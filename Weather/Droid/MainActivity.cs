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
	public class MainActivity : Activity //, ILocationListener
	{
		//private LocationManager LocManager;
		private Weather.Location CurrentLocation = new Weather.Location();
		//private Android.Locations.Location AndroidCurrentLocation;
		//private string LocationProvider;
		async protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			//InitializeLocationManager();

			var currentWeather = await GetWeather ();
			if (currentWeather != null) {
				var listView = FindViewById<ExpandableListView> (Resource.Id.myExpandableListview);
				listView.SetAdapter (new ExpandableDataAdapter (this, currentWeather));
			}
		}

		/// <summary>
		/// Gets the location based on android location service
		/// </summary>
	/*	private async Task<Weather.Location> GetHardwareLocation()
		{

			if(LocManager.IsProviderEnabled(LocationProvider))
			{
				LocManager.RequestLocationUpdates (LocationProvider, 2000, 1, this);
			} 

			var location = new Weather.Location ();
			if (AndroidCurrentLocation == null) {

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

				CurrentLocation = location;

			} else {
				location.lat = AndroidCurrentLocation.Latitude;
				location.lon = AndroidCurrentLocation.Longitude;
			}

			return location;

		}*/

		/// <summary>
		/// Gets the weather.
		/// </summary>
		/// <returns>The weather.</returns>
		private async Task<Weather.WeatherData> GetWeather()
		{
			var location = new Weather.Location ();

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

			try
			{
				return await WeatherClient.GetWeather (location);
			}
			catch (WebException e) {
				// TODO: procecc the exception
				string reason = e.Message;
				return null;
			}
			catch (Exception e) {
				// TODO: procecc the exception
				string reason = e.Message;
				return null;
			}
		}

	/*	protected override void OnPause ()
		{
			base.OnPause ();
			LocManager.RemoveUpdates (this);
		}

		public void OnLocationChanged (Android.Locations.Location location)
		{
			CurrentLocation.lat = location.Latitude; 
			CurrentLocation.lon = location.Longitude;

			AndroidCurrentLocation = location;
		}

		public void OnProviderDisabled (string provider){}

		public void OnProviderEnabled (string provider){}

		public void OnStatusChanged (string provider, Availability status, Bundle extras){}

		void InitializeLocationManager()
		{
			LocManager = (LocationManager)GetSystemService(LocationService);
			Criteria criteriaForLocationService = new Criteria
			{
				Accuracy = Accuracy.Coarse
			};
			IList<string> acceptableLocationProviders = LocManager.GetProviders(criteriaForLocationService, true);

			if (acceptableLocationProviders.Count > 0)
			{
				LocationProvider = acceptableLocationProviders[0];
			}
			else
			{
				LocationProvider = String.Empty;
			}
		}*/

	}
}


