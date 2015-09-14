using System;
using System.Runtime.Serialization;

namespace Weather
{
	[DataContract]
	public class Data
	{
		[DataMember]
		public CurrentCondition[] current_condition { get; set; }
		[DataMember]
		public WeatherRequest[] request { get; set; }
		[DataMember]
		public ServerWeather[] weather { get; set; }
	}
}