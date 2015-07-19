using CoreGraphics;
using Foundation;
using ObjCRuntime;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UIKit;


namespace iOS
{
	[Register("CalcLandView")]
	public partial class CalcLandView: UIView
	{
		public CalcLandView(IntPtr p) : base(p)
		{
		}

		public CalcLandView()
		{
			var arr = NSBundle.MainBundle.LoadNib ("CalcLandView", this, null);
			var v = Runtime.GetNSObject(arr.ValueAt(0)) as UIView;
			v.Frame = new CGRect (0, 0, Frame.Width, Frame.Height);
			AddSubview (v);

		}
	}
}

