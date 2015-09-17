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
	}
}

