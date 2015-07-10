using System;

namespace Core
{
	public class Calc
	{
		private double  result;		// variable to contain the result of calculation
		private double  argument;	// variable to contain the currently entered argument
		private int     dotFlag;	// variable to contain a dot-flag
		private Actions action;     // variable to containt current action

		public double Result
		{
			get { return result; }
		}

		public double Argument
		{
			get { return argument; }
		}

		public int Dot 
		{
			get { return dotFlag; }
		}

		public Calc ()
		{
			result = 0.0;
			argument = 0.0;
			dotFlag = -1;			// -1 means than no decimal part is used
			action = Actions.Unknown;
		}

		public enum Actions
		{
			Unknown,

			Plus,
			Minus,
			Mul,
			Div
		}

		public void PressDot()
		{
			dotFlag = dotFlag < 0 ? 0 : dotFlag;
		}

		public void PressNumber(int number)
		{
			dotFlag = dotFlag < 0 ? -1 : dotFlag + 1; 
			argument = dotFlag < 0 ? argument * 10 + number : argument + (double)number / Math.Pow(10, dotFlag);
		}

		public void PressClear()
		{
			argument = 0.0;
			result = 0.0;
			dotFlag = -1;
			action = Actions.Unknown;
		}

		public void PressBackspace()
		{
			dotFlag = dotFlag < 0 ? -1 : dotFlag - 1; 
			argument = dotFlag < 0 ? Math.Floor (argument / 10) : RoundDown(argument, dotFlag);
		}

		public void PressAction(Actions act)
		{
			
			switch (action) {
				case Actions.Div:
					result = result / argument;
					break;
				case Actions.Plus:
					result = result + argument;
					break;
				case Actions.Mul:
					result = result * argument;
					break;
				case Actions.Minus:
					result = result - argument;
					break;
				case Actions.Unknown:
					result = argument;
						break;
			}

			argument = 0.0;
			action = act;
			dotFlag = -1;
		}

		public void PressResult()
		{
			PressAction (action);
		}

		#region Round function

		private enum RoundingDirection { Up, Down }
		private delegate double RoundingFunction(double value);

		private static double RoundDown(double value, int precision)
		{
			return Round(value, precision, RoundingDirection.Down);
		}

		private static double Round(double value, int precision, 
			RoundingDirection roundingDirection)
		{
			RoundingFunction roundingFunction;
			if (roundingDirection == RoundingDirection.Up)
				roundingFunction = Math.Ceiling;
			else
				roundingFunction = Math.Floor;
				value *= Math.Pow(10, precision);
				value = roundingFunction(value);
				return value * Math.Pow(10, -1 * precision);
		}

		#endregion
	}
}

