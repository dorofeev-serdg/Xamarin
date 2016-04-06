using System;
using Weather;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Droid
{
	public class ConfigurationHelper
	{
		private static string DEFAULT_JSON_LOCATIONS = "{\"locations\":[\"Taganrog\",\"Voronezh\",\"Moscow\"]}";
		/// <summary>
		/// Reads the configuration from the file
		/// </summary>
		/// <returns>The configuration.</returns>
		public static async Task<Configuration[]> GetConfigurations(){
			Configuration[] configs = null;
			string configRootPath = ApplicationSettingsHelper.GetConfigRootPath ();
			string configFilePath = ApplicationSettingsHelper.GetConfigFilePath ();

			if (!System.IO.Directory.Exists (configRootPath)) {
				System.IO.Directory.CreateDirectory (configRootPath);
			}

			if (File.Exists (configFilePath)) {
				using (StreamReader reader = new StreamReader (configFilePath)) {
					string json = await reader.ReadToEndAsync ();

					if (json != null && json.Length > 0) {
						try
						{
							configs = JsonConvert.DeserializeObject<Configuration[]>(json);
						}
						catch{
							// TODO: process the exception of deserialization
							return null;
						}
					}
				}
			}
			return configs;
		}

		/// <summary>
		/// Adds the configuration to the saved file
		/// </summary>
		public static async Task AddCheckConfiguration(Configuration config)
		{
			var configList = new List<Configuration> ();
			var existedCofigurations = await GetConfigurations ();

			if (existedCofigurations != null) {
				configList.AddRange(existedCofigurations);
			}
			configList.Add (config);

			var json = JsonConvert.SerializeObject (configList);
			File.WriteAllText (ApplicationSettingsHelper.GetConfigFilePath (), json);
		}

		/// <summary>
		/// Gets the stored locations, i.e. array of cities
		/// </summary>
		/// <returns>The stored locations.</returns>
		public static async Task<string[]> GetStoredLocations()
		{
			StoredLocation location = null;
			string configRootPath = ApplicationSettingsHelper.GetConfigRootPath ();
			string locationsFilePath = ApplicationSettingsHelper.GetStoredLocationsFilePath ();
			string json = string.Empty;

			if (!System.IO.Directory.Exists (configRootPath)) {
				System.IO.Directory.CreateDirectory (configRootPath);
			}

			if (File.Exists (locationsFilePath)) {
				using (StreamReader reader = new StreamReader (locationsFilePath)) {
					json = await reader.ReadToEndAsync ();
				}
			} else {
				json = DEFAULT_JSON_LOCATIONS;
			}

			if (json != null && json.Length > 0) {
				try {
					location = JsonConvert.DeserializeObject<StoredLocation> (json);
				} catch {
					// TODO: process the exception of deserialization
					return null;
				}
			}

			return location != null ? location.locations : null;
		}
	}
}

