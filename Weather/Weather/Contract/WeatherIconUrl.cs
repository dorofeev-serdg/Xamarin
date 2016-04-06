using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Weather
{
	[DataContract]
	[JsonObject(MemberSerialization.OptIn)]
	public class WeatherIconUrl
	{
		[DataMember]
		[JsonProperty(PropertyName = "value")]
		public string value { get; set; }
	}
}
