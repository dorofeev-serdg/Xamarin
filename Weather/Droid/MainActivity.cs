using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Weather;
using System.Net;
using System.Threading.Tasks;
using Android.Locations;
using System.Collections.Generic;
using System.Linq;

namespace Droid
{
	[Activity (Label = "Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private Weather.Location CurrentLocation = new Weather.Location();
		private List<Weather.WeatherData> CurrentWeather = new List<Weather.WeatherData>();

		async protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Read locations and get weather for every location
			var storedLocations = await ConfigurationHelper.GetStoredLocations ();
			if (storedLocations != null && storedLocations.Length > 0 && (CurrentWeather == null || CurrentWeather.Count == 0)) {
				foreach (var loc in storedLocations) {
					var localWeather = await WeatherClient.GetWeather (new Weather.Location{ city = loc });
					if (localWeather != null)
						CurrentWeather.Add (localWeather);
				}
			} else {
				// TODO: procecc the error
			}

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			FragmentTransaction transaction = FragmentManager.BeginTransaction ();
			SlidingTabsFragment fragment = new SlidingTabsFragment (CurrentWeather);
			transaction.Replace (Resource.Id.sample_content_fragment, fragment);
			transaction.Commit ();
					}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.actionbar_main, menu);
			return base.OnCreateOptionsMenu (menu);
		}

		/// <summary>
		/// Gets the weather.
		/// </summary>
		/// <returns>The weather.</returns>
		private async Task<Weather.WeatherData> GetWeather()
		{
			var location = new Weather.Location ();
			WeatherData weather = new WeatherData ();
			//bool updateWeather = true;

			// Get current location
			try {
				location = await LocationClient.GetLocation ();
			} catch (WebException e) {
				// TODO: procecc the exception
				string reason = e.Message;
				return null;
			} catch (Exception e) {
				// TODO: procecc the exception
				string reason = e.Message;
				return null;
			}

			var storedLocations = await ConfigurationHelper.GetStoredLocations ();
			foreach(var loc in storedLocations)
			{
				var localWeather = await WeatherClient.GetWeather (new Weather.Location{ city = loc});
				if (localWeather != null)
					CurrentWeather.Add (localWeather);
			}

/*			// Check if there's saved data
			var configurations = await ConfigurationHelper.GetConfigurations ();
			if( configurations != null &&
				configurations.Length > 0) {
				DateTime observationTime = new DateTime ();
				if (DateTime.TryParse (configurations [0].Weather.data.current_condition [0].observation_time, out observationTime)) {

					if (observationTime.AddHours (ApplicationSettingsHelper.TIME_CACHE_INTERVAL) > DateTime.Now) {
						updateWeather = false;
						weather = configurations [0].Weather;
					}
				}
			}

			// Update data from service if required
			if (updateWeather) {
				try {
					weather = await WeatherClient.GetWeather (location);
				} catch (WebException e) {
					// TODO: procecc the exception
					string reason = e.Message;
					return null;
				} catch (Exception e) {
					// TODO: procecc the exception
					string reason = e.Message;
					return null;
				}
			}
			await ConfigurationHelper.AddCheckConfiguration (new Configuration (){ Location = location, Weather = weather });
			*/
			return weather;
		}
	}
}