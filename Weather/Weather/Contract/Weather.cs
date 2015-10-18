using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Weather
{
	[DataContract]
	[JsonObject(MemberSerialization.OptIn)]
	public class WeatherData
	{
		[DataMember]
		[JsonProperty(PropertyName = "data")]
		public Data data { get; set; }
	}
}

