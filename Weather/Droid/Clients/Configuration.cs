using System;

namespace Weather
{
	public class Configuration
	{
		public Location Location { get; set; }
		public WeatherData Weather { get; set; }

		/// <summary>
		/// Reads the configuration from the file
		/// </summary>
		/// <returns>The configuration.</returns>
		public static Configuration GetConfiguration(){
			Configuration config = new Configuration();

			return config;
		}

		/// <summary>
		/// Saves the configuration to the file.
		/// </summary>
		public void SaveConfiguration()
		{
			
		}

	}
}

