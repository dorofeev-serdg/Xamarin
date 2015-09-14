using System;
using System.Runtime.Serialization;

namespace Weather
{
	[DataContract]
	public class ServerWeather
	{
		[DataMember]
		public string date { get; set; }
		[DataMember]
		public int maxtempC { get; set; }
		[DataMember]
		public int mintempC { get; set; }
		[DataMember]
		public HourlyWeather[] hourly { get; set; }

	}
}