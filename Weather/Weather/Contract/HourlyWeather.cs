using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Weather
{
	[DataContract]
	[JsonObject(MemberSerialization.OptIn)]
	public class HourlyWeather
	{
		[DataMember]
		[JsonProperty(PropertyName = "time")]
		public int time { get; set;	}
		public string Time 
		{ 
			get 
			{ 
				string t = time.ToString ("0000"); 
				return string.Format ("{0}:{1}", t.Substring (0, 2), t.Substring (2, 2));
			} 
		}
		[DataMember]
		[JsonProperty(PropertyName = "tempC")]
		public int tempC { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "pressure")]
		public int pressure { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "weatherIconUrl")]
		public WeatherIconUrl[] weatherIconUrl { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "winddirDegree")]
		public int winddirDegree { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "windspeedKmph")]
		public int windspeedKmph { get; set; }
		[DataMember]
		[JsonProperty(PropertyName = "humidity")]
		public int humidity { get; set; }
	}
}