using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Weather;
using Android.Graphics;
using System.Net;

namespace Droid
{
	public class DataAdapter : BaseAdapter<Weather.HourlyWeather>{
		readonly Activity context;

		public DataAdapter(Activity newContext, List<Weather.HourlyWeather> newData) : base ()
		{
			context = newContext;
			WeatherList = newData;
		}

		public List<Weather.HourlyWeather> WeatherList { get; set; }

		public override int Count {
			get {
				return WeatherList.Count;
			}
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View newView = convertView; // re-use an existing view, if one is available
			if (newView == null) // otherwise create a new one
				newView = context.LayoutInflater.Inflate(Resource.Layout.DataListItem, null);
			newView.FindViewById<TextView> (Resource.Id.Time).Text = WeatherList [position].Time;
			newView.FindViewById<TextView> (Resource.Id.Temp).Text = WeatherList [position].tempC.ToString();
			newView.FindViewById<TextView> (Resource.Id.WindSpeed).Text = WeatherList [position].windspeedKmph.ToString();
			return newView;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override HourlyWeather this[int index] {
			get {
				return WeatherList  [index];
			}
		}

	}
}

