
using System;

using Foundation;
using UIKit;
using Core;

namespace iOS
{
	public partial class CalcViewController : UIViewController
	{
		Calc calc;

		public CalcViewController () : base ("CalcViewController", null)
		{
			calc = new Calc ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.

			Number0.TouchUpInside += (object sender, EventArgs e) => { NumberX_Click(0); };
			Number1.TouchUpInside += (object sender, EventArgs e) => { NumberX_Click(1); };
			Number2.TouchUpInside += (object sender, EventArgs e) => { NumberX_Click(2); };
			Number3.TouchUpInside += (object sender, EventArgs e) => { NumberX_Click(3); };
			Number4.TouchUpInside += (object sender, EventArgs e) => { NumberX_Click(4); };
			Number5.TouchUpInside += (object sender, EventArgs e) => { NumberX_Click(5); };
			Number6.TouchUpInside += (object sender, EventArgs e) => { NumberX_Click(6); };
			Number7.TouchUpInside += (object sender, EventArgs e) => { NumberX_Click(7); };
			Number8.TouchUpInside += (object sender, EventArgs e) => { NumberX_Click(8); };
			Number9.TouchUpInside += (object sender, EventArgs e) => { NumberX_Click(9); };


			ActionPlus.TouchUpInside   += (object sender, EventArgs e) => { ActionX_Click(Calc.Actions.Plus);};
			ActionMinus.TouchUpInside  += (object sender, EventArgs e) => { ActionX_Click(Calc.Actions.Minus);};
			ActionMul.TouchUpInside    += (object sender, EventArgs e) => { ActionX_Click(Calc.Actions.Mul);};
			ActionDiv.TouchUpInside    += (object sender, EventArgs e) => { ActionX_Click(Calc.Actions.Div);};
			ActionResult.TouchUpInside += (object sender, EventArgs e) => { Result_Click();};

			ActionClear.TouchUpInside += (object sender, EventArgs e) => {
				PressClear ();
			};

			ActionBackspace.TouchUpInside += (object sender, EventArgs e) => {
				PressBackspace ();
			};

			Dot.TouchUpInside += (object sender, EventArgs e) => {
				PressDot();
			};
		}

		private void NumberX_Click (int number)
		{
			calc.PressNumber(number);
			Console.WriteLine (String.Format("Press number {0}", number));
			Digits.Text = calc.Argument.ToString("F" + ( calc.Dot < 0 ? "0" : calc.Dot.ToString()));
		}

		private void ActionX_Click(Calc.Actions action)
		{
			try
			{
				calc.PressAction (action);
			}
			catch {
				Digits.Text = "Error!";
				calc.PressClear ();
				return;
			}
			Console.WriteLine (String.Format("Press action {0}", action));
			Digits.Text = calc.Result.ToString ();
		}

		private void Result_Click()
		{
			calc.PressResult ();
			Console.WriteLine ("Press result");
			try
			{
				Digits.Text = calc.Result.ToString ();
			}
			catch {
				Digits.Text = "Error!";
				calc.PressClear ();
			}
		}

		private void PressDot()
		{
			calc.PressDot ();
			Console.WriteLine ("Press dot");
		}

		private void PressClear()
		{
			calc.PressClear ();
			Console.WriteLine ("Press clear");
			Digits.Text = calc.Argument.ToString ();
		}

		private void PressBackspace()
		{
			calc.PressBackspace ();
			Console.WriteLine ("Press backspace");
			Digits.Text = calc.Argument.ToString ("F" + ( calc.Dot < 0 ? "0" : calc.Dot.ToString()));
		}
	}
}

