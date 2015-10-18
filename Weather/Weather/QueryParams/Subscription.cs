using System;
using System.ComponentModel;

namespace Weather
{
	public class Subscription
	{
		/// <summary>
		/// Enum to specify possible subscriptions
		/// </summary>
		public enum Subscript
		{
			Unknown,

			[Description("free")]
			free,
			[Description("premium")]
			premium
		}
	}
}

