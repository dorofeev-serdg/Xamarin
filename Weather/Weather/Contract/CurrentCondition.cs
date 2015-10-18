using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Weather
{
	[DataContract]
	[JsonObject(MemberSerialization.OptIn)]
	public class CurrentCondition
	{
		[DataMember]
		[JsonProperty(PropertyName = "cloudcover")]
		public int cloudcover { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "FeelsLikeC")]
		public int FeelsLikeC { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "pressure")]
		public int pressure { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "temp_C")]
		public int temp_C { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "winddirDegree")]
		public int winddirDegree { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "windspeedKmph")]
		public int windspeedKmph { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "weatherIconUrl")]
		public WeatherIconUrl[] weatherIconUrl { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "observation_time")]
		public string observation_time{ get; set; }
	}
}

