using System;

namespace Core
{
	public class Calc
	{
		private double result;		// variable to contain the result of calculation
		private double argument;	// variable to contain the entered argument
		private uint    dotFlag;	// variable to contain a dot-flag
		private Actions action;     // variable to containt current action

		public double Result
		{
			get { return result; }
		}

		public double Argument
		{
			get { return argument; }
		}

		public uint Dot 
		{
			get { return dotFlag; }
		}

		public Calc ()
		{
			result = 0.0;
			argument = 0.0;
			dotFlag = 0;
		}

		public enum Actions
		{
			Unknown,

			Result,
			Plus,
			Minus,
			Mul,
			Div
		}

		public void PressDot()
		{
			dotFlag = dotFlag == 0 ? 1 : dotFlag;
		}

		public void PressNumber(int number)
		{
			argument = dotFlag == 0 ? argument * 10 + number : argument + (double)number / (10 ^ dotFlag);
			dotFlag = dotFlag == 0 ? 0 : dotFlag + 1; 
		}

		public void PressClear()
		{
			argument = 0.0;
			result = 0.0;
			dotFlag = 0;
			action = Actions.Unknown;
		}

		public void PressBackspace()
		{
			argument = dotFlag == 0 ? Math.Floor (argument / 10) : Math.Round (argument, -((int)dotFlag + 1));
			dotFlag = dotFlag == 0 ? 0 : dotFlag - 1; 
		}

		public void PressAction(Actions act)
		{
			if (act != Actions.Result) {
				action = act;
			} else {
				try
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
					}
				}
				catch (Exception e)
				{
					// TODO: implement logging
					result = 0.0;
					argument = 0.0;
					dotFlag = 0;
				}
			}

			argument = 0.0;
			dotFlag = 0;
		}
	}
}

