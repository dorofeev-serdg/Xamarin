using System;
using System.Runtime.Serialization;

namespace Weather
{
	[DataContract]
	 public class HourlyWeather
	{
		[DataMember]
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
		public int tempC { get; set; }
		[DataMember]
		public int pressure { get; set; }
		[DataMember]
		public WeatherIconUrl[] weatherIconUrl { get; set; }
		[DataMember]
		public int winddirDegree { get; set; }
		[DataMember]
		public int windspeedKmph { get; set; }
		[DataMember]
		public int humidity { get; set; }
	}
}