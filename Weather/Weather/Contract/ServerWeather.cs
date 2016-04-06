using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Weather
{
	[DataContract]
	[JsonObject(MemberSerialization.OptIn)]
	public class ServerWeather
	{
		[DataMember]
		[JsonProperty(PropertyName = "date")]
		public string date { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "maxtempC")]
		public int maxtempC { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "mintempC")]
		public int mintempC { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "hourly")]
		public HourlyWeather[] hourly { get; set; }

	}
}