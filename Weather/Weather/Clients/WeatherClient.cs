﻿using System;
using System.Runtime.Serialization;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Android.Graphics;
using Droid;
using System.Threading;
using System.Collections.Generic;


namespace Weather
{
	public class WeatherClient
	{
		public static readonly string WEATHER_URL = "http://api.worldweatheronline.com/{0}/v2/weather.ashx?q={1}&format=json&num_of_days={2}&key={3}";
		public static readonly int NUMBER_OF_DAYS = 5; 
		public static readonly int HOURLY_WEATHER_COUNT = 8;

		/// <summary>
		/// Performs web request and gets the weather from the service.
		/// </summary>
		/// <returns>The weather.</returns>
		/// <param name="location">Location to get the weather</param>
		public static async Task<WeatherData> GetWeather(Location location)
		{
			WeatherData result = null;
			string weatherUrl = string.Format (WEATHER_URL, EnumHelper.GetDescription (Subscription.Subscript.free), location.city, NUMBER_OF_DAYS.ToString (), ApplicationSettingsHelper.API_KEY);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(weatherUrl);

			using(WebResponse response = await request.GetResponseAsync())
			{
				using (Stream resStream = response.GetResponseStream ()) 
				{
					using (StreamReader reader = new StreamReader (resStream)) 
					{
						string jsonResult = await reader.ReadToEndAsync ();
						result = JsonConvert.DeserializeObject<WeatherData> (jsonResult);
					}
				}
			}
			 
			int  length1 = result.data.weather.Length;
			int  length2 = result.data.weather[0].hourly.Length;

			// TODO: investigate ho w to run this tasks in Parallel
			for( int i = 0; i < length1; i++){
				for(int j = 0; j < length2; j++) {
					//taskArray[i*length2 + j]  = Task.Factory.StartNew(async () => { await GetWeatherImageFile (result.data.weather[i].hourly[j].weatherIconUrl[0].value); });
					await GetWeatherImageFile (result.data.weather[i].hourly[j].weatherIconUrl[0].value);
				}
			}
			return result;
		}

/*		/// <summary>
		/// Gets the weather for all the locations
		/// </summary>
		/// <returns>The weather array</returns>
		/// <param name="locations">Locations.</param>
		public static async Task<WeatherData[]> GetWeather(string[] inputLocations)
		{
			// Such a structure of parallel tasks is implemented to avoid losing context on parallel task execution
			WeatherData[] weatherArray = new WeatherData[inputLocations.Length] ;
			Location[] locations = new Location [inputLocations.Length];

			// Declare list of tasks to wait them in future
			Func<Task>[] taskList = new Func<Task>[inputLocations.Length];

			// Declare and populate array of semaphors 
			ManualResetEvent[] resetEvent = new ManualResetEvent [inputLocations.Length];

			for(int i = 0; i < inputLocations.Length; i++)
			{
				resetEvent[i] = new ManualResetEvent(false);
			}

			// Define tasks to update the weather and add each of them to the list
			for (int i = 0; i < inputLocations.Length; i++)
			{
				Func<Task> task = async() => {
					try {
						weatherArray[i] = await GetWeather (new Location (){ city = inputLocations[i] });
					}
					catch(Exception e)
					{
						Console.WriteLine(e.Message);
						resetEvent [i].Set ();
					} finally {
						resetEvent [i].Set ();
					}
				};
				taskList [i] = task;
			}

			// Run each task 
			for (int i = 0; i < inputLocations.Length; i++)
			{
				ThreadPool.QueueUserWorkItem (x => taskList[i]());
			}

			// Wait all the semaphores are set
			WaitHandle.WaitAll (resetEvent, -1, false);

			return weatherArray;
		} 

		/// <summary>
		/// Gets the weather for all the locations
		/// </summary>
		/// <returns>The weather array</returns>
		/// <param name="locations">Locations.</param>
		public static async Task<WeatherData[]> GetWeather(string[] inputLocations)
		{
			// Such a structure of parallel tasks is implemented to avoid losing context on parallel task execution
			WeatherData[] weatherArray = new WeatherData[inputLocations.Length] ;
			Location[] locations = new Location [inputLocations.Length];

			// Declare list of tasks to wait them in future
			Task[] taskList = new Task<WeatherData>[inputLocations.Length];

			// Define tasks to update the weather and add each of them to the list
			for (int i = 0; i < inputLocations.Length; i++)
			{
				taskList [i] = Task.Factory.StartNew ( async(loc) => {
					await GetWeather (new Location (){ city = (string)loc });
				}, inputLocations [i]);
			}

			// Wait all the tasks
			Task.WaitAll(taskList); 

			//for (int i = 0; i < inputLocations.Length; i++) {
			//	weatherArray [i] = taskList [i].Result;
			//}

			return weatherArray;
		}
		*/



		/// <summary>
		/// Downloads the required weather image.
		/// </summary>
		/// <returns>The weather image file.</returns>
		/// <param name="uri">URI.</param>
		public static async Task GetWeatherImageFile(string uri)
		{
			string fileName = uri.Substring (uri.LastIndexOfAny (new char[] {	'/', '\\'}) + 1);
			string rootPath = ApplicationSettingsHelper.GetAppRootPath();
			string fileDirRoot = ApplicationSettingsHelper.GetWeatherImagesDir ();
			string totalFileName = System.IO.Path.Combine( fileDirRoot, fileName);

			if (!System.IO.Directory.Exists (fileDirRoot)) {
				System.IO.Directory.CreateDirectory (fileDirRoot);
			}

			if (!File.Exists (totalFileName)) {
				using (var webClient = new WebClient())
				{
					Uri fileUri = new Uri (uri);
					var webData = await webClient.DownloadDataTaskAsync(fileUri);

					if (webData != null && webData.Length > 0) {
						var filePath = System.IO.Path.Combine(fileDirRoot, fileName);
						System.IO.File.WriteAllBytes (filePath, webData);
					}
				}
			}
		}

		/// <summary>
		/// Gets the weather image.
		/// </summary>
		/// <returns>The weather image.</returns>
		/// <param name="fileName">File name.</param>
		public static Bitmap GetWeatherImage(string fileName)
		{
			string imagesRoot = ApplicationSettingsHelper.GetWeatherImagesDir ();
			string imagePath = System.IO.Path.Combine (imagesRoot, fileName);

			if (File.Exists (imagePath)) {

				var bitmap =  BitmapFactory.DecodeFile(imagePath);
				return bitmap;

			} else {
				return null;
			}
		}
	}
}

