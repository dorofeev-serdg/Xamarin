using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Weather
{
	[DataContract]
	[JsonObject(MemberSerialization.OptIn)]
	public class Data
	{
		[DataMember]
		[JsonProperty(PropertyName = "current_condition")]
		public CurrentCondition[] current_condition { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "request")]
		public WeatherRequest[] request { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "weather")]
		public ServerWeather[] weather { get; set; }
	}
}