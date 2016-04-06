using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Weather
{
	[DataContract]
	[JsonObject(MemberSerialization.OptIn)]
	public class Location
	{
		[DataMember]
		[JsonProperty(PropertyName = "city")]
		public string city { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "country")]
		public string country { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "lat")]
		public double lat { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "lon")]
		public double lon{ get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "status")]
		public string status { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "timezone")]
		public string timezone { get; set; }
	}
}