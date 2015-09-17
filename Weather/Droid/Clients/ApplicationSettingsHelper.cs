using System;

namespace Droid
{
	public static class ApplicationSettingsHelper
	{
		public static readonly string API_KEY = "ae14af6e1c2d83d16659aea4c71d9";
		public static readonly string WEATHER_IMAGE_ROOT = @"/WeatherImages/";
		public static readonly string CONFIG_ROOT = @"/Config/";
		public static readonly string CONFIG_FILE_NAME = @"Config.txt";
		public static readonly int    TIME_CACHE_INTERVAL = 1;	// number of hours to store data

		/// <summary>
		/// Returns the folder path to the specific folder of Weather application
		/// </summary>
		/// <returns>The app root path.</returns>
		public static string GetAppRootPath()
		{
			string rootPath = Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData);
			rootPath = rootPath.Substring (0, rootPath.LastIndexOfAny (new char[]{ '/', '\\' }) - 1);
			return rootPath;
		}

		/// <summary>
		/// Gets the weather images dir.
		/// </summary>
		/// <returns>The weather images dir.</returns>
		public static string GetWeatherImagesDir()
		{
			return GetAppRootPath() + WEATHER_IMAGE_ROOT;
		}

		/// <summary>
		/// Gets the configuration root directory path.
		/// </summary>
		/// <returns>The configuration root directory path.</returns>
		public static string GetConfigRootPath()
		{
			return GetAppRootPath() + CONFIG_ROOT;
		}

		/// <summary>
		/// Gets the configuration file full path.
		/// </summary>
		/// <returns>The configuration file full path.</returns>
		public static string GetConfigFilePath()
		{
			return GetConfigRootPath() + CONFIG_FILE_NAME;
		}
	}
}

