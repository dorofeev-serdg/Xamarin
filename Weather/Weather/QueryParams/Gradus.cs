using System;
using System.ComponentModel;

namespace Weather
{
	public static class Gradus
	{
		/// <summary>
		/// Enum to specify possible graduses
		/// </summary>
		public enum Graduses
		{
			Unknown,

			[Description("°C")]
			Celsius,
			[Description("°F")]
			Farenheit
		}
	}
}

