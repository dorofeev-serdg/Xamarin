using System;
using System.Runtime.Serialization;

namespace Weather
{
	[DataContract]
	public class Location
	{
		[DataMember]
		public string city { get; set; }
		[DataMember]
		public string country { get; set; }
		[DataMember]
		public double lat { get; set; }
		[DataMember]
		public double lon{ get; set; }
		[DataMember]
		public string status { get; set; }
		[DataMember]
		public string timezone { get; set; }
	}
}

