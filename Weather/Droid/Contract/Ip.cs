using System;
using System.Runtime.Serialization;

namespace Weather
{
	[DataContract]
	public class Ip
	{
		[DataMember]
		public string IP { get; set; }
	}
}

