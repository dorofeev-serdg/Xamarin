using System;
using System.Runtime.Serialization;

namespace Weather
{
	[DataContract]
	public class WeatherIconUrl
	{
		[DataMember]
		public string value { get; set; }
	}
}
