using System;
using System.Runtime.Serialization;

namespace Weather
{
	[DataContract]
	public class CurrentCondition
	{
		[DataMember]
		public int cloudcover { get; set; }
		[DataMember]
		public int FeelsLikeC { get; set; }
		[DataMember]
		public int pressure { get; set; }
		[DataMember]
		public int temp_C { get; set; }
		[DataMember]
		public int winddirDegree { get; set; }
		[DataMember]
		public int windspeedKmph { get; set; }
		[DataMember]
		public WeatherIconUrl[] weatherIconUrl { get; set; }
		[DataMember]
		public string observation_time{ get; set; }
	}
}

