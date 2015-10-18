using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Weather
{
	[DataContract]
	[JsonObject(MemberSerialization.OptIn)]
	public class WeatherRequest
	{
		[DataMember]
		[JsonProperty(PropertyName = "query")]
		public string query { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "type")]
		public string type { get; set; }
	}
}
