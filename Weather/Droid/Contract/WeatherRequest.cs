using System;
using System.Runtime.Serialization;

namespace Weather
{
	[DataContract]
	public class WeatherRequest
	{
		[DataMember]
		public string query { get; set; }
		[DataMember]
		public string type { get; set; }
	}
}
