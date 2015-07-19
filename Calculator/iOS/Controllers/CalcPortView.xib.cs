using CoreGraphics;
using Foundation;
using ObjCRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace iOS
{
	[Register("CalcPortView")]
	public partial class CalcPortView :UIView
	{
		public CalcPortView(IntPtr p) : base (p)
		{
		}

		public CalcPortView()
		{
			var arr = NSBundle.MainBundle.LoadNib ("CalcPortView", this, null);
			var v = Runtime.GetNSObject(arr.ValueAt(0)) as UIView;
			v.Frame = new CGRect (0, 0, Frame.Width, Frame.Height);
			AddSubview (v);

		}
	}
}

