using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Weather
{
	[DataContract]
	[JsonObject(MemberSerialization.OptIn)]
	public class Ip
	{
		[DataMember]
		[JsonProperty(PropertyName = "IP")]
		public string IP { get; set; }
	}
}

