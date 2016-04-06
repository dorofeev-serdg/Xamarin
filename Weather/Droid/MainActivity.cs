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
				ShowNotification ("Error", "No presaved location found");
				// TODO: show view to enter new location
			}

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			if (bundle != null) {
				
			}

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
		/// Shows the alert.
		/// </summary>
		/// <param name="header">Header text.</param>
		/// <param name="message">Message text.</param>
		private void ShowNotification(string header, string message)
		{
			var builder = new AlertDialog.Builder (this);
			builder.SetCancelable (true);
			//builder.SetPositiveButton ("Ok", (senderArert, EventArgs) => {});

			var alert = builder.Create ();
			alert.SetTitle(header);
			alert.SetMessage(message);
			alert.Show();			
		}

		/// <summary>
		/// Gets the weather.
		/// </summary>
		/// <returns>The weather.</returns>
		private async Task<Weather.WeatherData> GetWeather()
		{
			var location = new Weather.Location ();
			WeatherData weather = new WeatherData ();

			// Get current location
			try {
				location = await LocationClient.GetLocation ();
			} catch (WebException e) {
				ShowNotification ("Error", "Error defining current location");
				// TODO: show view to enter current location
			} catch (Exception e) {
				ShowNotification ("Error", "Application will be closed.");
				Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
			}

			var storedLocations = await ConfigurationHelper.GetStoredLocations ();
			foreach(var loc in storedLocations)
			{
				var localWeather = await WeatherClient.GetWeather (new Weather.Location{ city = loc});
				if (localWeather != null)
					CurrentWeather.Add (localWeather);
			}

			return weather;
		}
	}
}