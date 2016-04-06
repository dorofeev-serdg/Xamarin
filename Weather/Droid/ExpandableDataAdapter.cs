using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Droid
{
	public class ExpandableDataAdapter : BaseExpandableListAdapter 
	{
		protected List<Weather.HourlyWeather> HourlyWeather { get; set; }
		protected List<Weather.ServerWeather> DailyWeather { get; set; }
		protected Weather.WeatherData WeatherData { get; set; }

		public ExpandableDataAdapter( Weather.WeatherData newWeather) : base()
		{

			WeatherData = newWeather;

			var dailyWeather = new List<Weather.ServerWeather> ();
			foreach (var daily in newWeather.data.weather) {
				dailyWeather.Add (daily);
			}
			DailyWeather = dailyWeather;

			var hourlyWeather = new List<Weather.HourlyWeather> ();
			foreach (var daily in DailyWeather) {
				foreach (var hourly in daily.hourly) {
					hourlyWeather.Add (hourly);
				}
			}

			HourlyWeather = hourlyWeather;
		}

		public override View GetGroupView (int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
		{
			View header = convertView;
			if (header == null) {
				using (var inflater = Application.Context.GetSystemService (Context.LayoutInflaterService) as LayoutInflater) {
					header = inflater.Inflate (Resource.Layout.ListGroup, null);
				}
			}
				
			DateTime date = new DateTime ();
			if (DateTime.TryParse (DailyWeather [groupPosition].date, out date)) {
				header.FindViewById<TextView> (Resource.Id.DataHeader).Text = String.Format ("{0} - {1} - {2}", 
					date.DayOfWeek.ToString ().Substring(0, 3), date.Day.ToString("D2"), date.ToString ("MMMMM").Substring(0,3));
			} else {
				header.FindViewById<TextView> (Resource.Id.DataHeader).Text =  DailyWeather[groupPosition].date;
			}

			return header;
		}

		public override View GetChildView (int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
		{
			View row = convertView;
			if (row == null) {
				using (var inflater = Application.Context.GetSystemService (Context.LayoutInflaterService) as LayoutInflater) {
					row = inflater.Inflate (Resource.Layout.DataListItem, null);
				}
			}
				
			row.FindViewById<TextView> (Resource.Id.Time).Text = DailyWeather [groupPosition].hourly [childPosition].Time;
			row.FindViewById<TextView> (Resource.Id.Temp).Text = string.Format("{0}{1}", DailyWeather [groupPosition].hourly [childPosition].tempC.ToString (),  Weather.EnumHelper.GetDescription (Weather.Gradus.Graduses.Celsius));
			row.FindViewById<TextView> (Resource.Id.WindSpeed).Text = string.Format( "{0} m/s", Math.Round(DailyWeather [groupPosition].hourly [childPosition].windspeedKmph/3.6).ToString("F0"));
			row.FindViewById<TextView> (Resource.Id.Humidity).Text = string.Format ("{0}%", DailyWeather [groupPosition].hourly [childPosition].humidity);
			row.FindViewById<ImageView> (Resource.Id.WeatherImage).SetImageBitmap (GetImageBitmapFromUrl(DailyWeather [groupPosition].hourly [childPosition].weatherIconUrl [0].value));

			var rowView = row.FindViewById<TextView> (Resource.Id.Time);
			Bitmap windBitmap = BitmapFactory.DecodeResource (Application.Context.Resources, Resource.Drawable.arrowLeft);
			var rotatedBitmap = RotateBitmap (windBitmap, (float)DailyWeather [groupPosition].hourly [childPosition].winddirDegree - 90); // 90 is used as initial arraw is turned right

			// Let's make the arraw squared by size of corner equal to height of element row
			using (var bitmapScalled = Bitmap.CreateScaledBitmap (rotatedBitmap, rowView.LineHeight, rowView.LineHeight, true)) {
				row.FindViewById<ImageView> (Resource.Id.WindDirection).SetImageBitmap (bitmapScalled);
			}

			windBitmap.Recycle ();
			rotatedBitmap.Recycle ();
			return row;
		}

		public override int GetChildrenCount (int groupPosition)
		{
			return DailyWeather [groupPosition].hourly.Length;
		}

		public override int GroupCount {
			get {
				return DailyWeather.Count;
			}
		}

		public static Bitmap RotateBitmap(Bitmap source, float angle)
		{
			Matrix matrix = new Matrix ();
			matrix.SetRotate (angle);
			return Bitmap.CreateBitmap (source, 0, 0, (int)source.GetBitmapInfo ().Width, (int)source.GetBitmapInfo ().Height, matrix, true);
		}

		#region implemented abstract members of BaseExpandableListAdapter

		public override Java.Lang.Object GetChild (int groupPosition, int childPosition)
		{
			throw new NotImplementedException ();
		}

		public override long GetChildId (int groupPosition, int childPosition)
		{
			return childPosition;
		}

		public override Java.Lang.Object GetGroup (int groupPosition)
		{
			throw new NotImplementedException ();
		}

		public override long GetGroupId (int groupPosition)
		{
			return groupPosition;
		}

		public override bool IsChildSelectable (int groupPosition, int childPosition)
		{
			return true;
		}

		public override bool HasStableIds {
			get {
				return true;
			}
		}

		#endregion
		/// <summary>
		/// Gets the image bitmap from URL.
		/// </summary>
		/// <returns>The image bitmap from URL.</returns>
		/// <param name="url">URL.</param>
		private Bitmap GetImageBitmapFromUrl(string url)
		{
			Bitmap imageBitmap = null;
			string fileName = url.Substring (url.LastIndexOfAny (new char[] {	'/', '\\' }) + 1);
			imageBitmap = Weather.WeatherClient.GetWeatherImage (fileName);

			if (imageBitmap == null) {
				using (var webClient = new WebClient ()) {
					var imageBytes = webClient.DownloadData (url);
					if (imageBytes != null && imageBytes.Length > 0) {
						imageBitmap = BitmapFactory.DecodeByteArray (imageBytes, 0, imageBytes.Length);
					}
				}
			}
			return imageBitmap;
		}
	}
}

