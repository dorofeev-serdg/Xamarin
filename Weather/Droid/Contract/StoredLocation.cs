using System;
using System.Runtime.Serialization;

namespace Weather
{
	[DataContract]
	public class StoredLocation
	{
		[DataMember]
		public string[] locations { get; set; }
	}
}

