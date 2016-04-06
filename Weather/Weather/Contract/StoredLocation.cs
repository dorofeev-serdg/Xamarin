using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Weather
{
	[DataContract]
	[JsonObject(MemberSerialization.OptIn)]
	public class StoredLocation
	{
		[DataMember]
		[JsonProperty(PropertyName = "locations")]
		public string[] locations { get; set; }
	}
}

