using System;
using System.Runtime.Serialization;

namespace Weather
{
	[DataContract]
	public class WeatherData
	{
		[DataMember]
		public Data data { get; set; }
	}
}

