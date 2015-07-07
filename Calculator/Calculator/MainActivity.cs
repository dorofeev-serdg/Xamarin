using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Core;

namespace Calculator
{
	[Activity (Label = "Calculator", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		Calc calc;

		// Section of screen elements
		private Button number0;
		private Button number1;
		private Button number2;
		private Button number3;
		private Button number4;
		private Button number5;
		private Button number6;
		private Button number7;
		private Button number8;
		private Button number9;
		private Button dot;
		private Button actionPlus;
		private Button actionMinus;
		private Button actionMul;
		private Button actionDiv;
		private Button actionResult;
		private Button backspace;
		private Button clear;
		private TextView screen;

		protected override void OnCreate (Bundle bundle)
		{
			calc = new Calc ();
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			number0 = FindViewById<Button> (Resource.Id.Number0);
			number1 = FindViewById<Button> (Resource.Id.Number1);
			number2 = FindViewById<Button> (Resource.Id.Number2);
			number3 = FindViewById<Button> (Resource.Id.Number3);
			number4 = FindViewById<Button> (Resource.Id.Number4);
			number5 = FindViewById<Button> (Resource.Id.Number5);
			number6 = FindViewById<Button> (Resource.Id.Number6);
			number7 = FindViewById<Button> (Resource.Id.Number7);
			number8 = FindViewById<Button> (Resource.Id.Number8);
			number9 = FindViewById<Button> (Resource.Id.Number9);

			screen = FindViewById<TextView> (Resource.Id.digits);

			dot = FindViewById<Button> (Resource.Id.Dot);
			actionPlus = FindViewById<Button> (Resource.Id.ActionPlus);
			actionMinus = FindViewById<Button> (Resource.Id.ActionMinus);
			actionMul = FindViewById<Button> (Resource.Id.ActionMul);
			actionDiv = FindViewById<Button> (Resource.Id.ActionDiv);
			actionResult = FindViewById<Button> (Resource.Id.ActionResult);

			backspace = FindViewById<Button> (Resource.Id.ActionBackspace);
			clear = FindViewById<Button> (Resource.Id.ActionClear);

			number0.Click += (object sender, EventArgs e) => { NumberX_Click(0); };
			number1.Click += (object sender, EventArgs e) => { NumberX_Click(1); };
			number2.Click += (object sender, EventArgs e) => { NumberX_Click(2); };
			number3.Click += (object sender, EventArgs e) => { NumberX_Click(3); };
			number4.Click += (object sender, EventArgs e) => { NumberX_Click(4); };
			number5.Click += (object sender, EventArgs e) => { NumberX_Click(5); };
			number6.Click += (object sender, EventArgs e) => { NumberX_Click(6); };
			number7.Click += (object sender, EventArgs e) => { NumberX_Click(7); };
			number8.Click += (object sender, EventArgs e) => { NumberX_Click(8); };
			number9.Click += (object sender, EventArgs e) => { NumberX_Click(9); };

			actionPlus.Click   += (object sender, EventArgs e) => { ActionX_Click(Calc.Actions.Plus);};
			actionMinus.Click  += (object sender, EventArgs e) => { ActionX_Click(Calc.Actions.Minus);};
			actionMul.Click    += (object sender, EventArgs e) => { ActionX_Click(Calc.Actions.Mul);};
			actionDiv.Click    += (object sender, EventArgs e) => { ActionX_Click(Calc.Actions.Div);};
			actionResult.Click += (object sender, EventArgs e) => { ActionX_Click(Calc.Actions.Result);};

			clear.Click += (object sender, EventArgs e) => {
				PressClear ();
			};

			backspace.Click += (object sender, EventArgs e) => {
				PressBackspace ();
			};

			dot.Click += (object sender, EventArgs e) => {
				PressDot();
			};
		}

		private void NumberX_Click (int number)
		{
			calc.PressNumber(number);
			Console.WriteLine (String.Format("Press number {0}", number));
			screen.Text = calc.Argument.ToString("F" + calc.Dot.ToString());
		}

		private void ActionX_Click(Calc.Actions action)
		{
			calc.PressAction (action);
			Console.WriteLine (String.Format("Press action {0}", action));
			screen.Text = calc.Result.ToString ( "F" + calc.Dot.ToString());
		}

		private void PressDot()
		{
			calc.PressDot ();
			Console.WriteLine ("Press dot");
			screen.Text = calc.Argument.ToString ("F" + calc.Dot.ToString());
		}

		private void PressClear()
		{
			calc.PressClear ();
			Console.WriteLine ("Press clear");
			screen.Text = calc.Argument.ToString ("F" + calc.Dot.ToString());
		}

		private void PressBackspace()
		{
			calc.PressBackspace ();
			Console.WriteLine ("Press backspace");
			screen.Text = calc.Argument.ToString ("F" + calc.Dot.ToString());
		}
	}
}


