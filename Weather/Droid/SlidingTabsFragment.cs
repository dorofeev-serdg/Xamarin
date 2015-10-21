
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;

namespace Droid
{
	public class SlidingTabsFragment : Fragment
	{
		private SlidingTabScrollView mSlidingTabScrollView;
		private ViewPager mViewPager;
		private List<Weather.WeatherData> CurrentWeather = new List<Weather.WeatherData> ();

		public SlidingTabsFragment( List<Weather.WeatherData> weatherDataList): base()
		{
			CurrentWeather = weatherDataList;
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate (Resource.Layout.fragment_sample, container, false);
		}

		public override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			mSlidingTabScrollView = View.FindViewById<SlidingTabScrollView> (Resource.Id.sliding_tabs);
			mViewPager = view.FindViewById<ViewPager> (Resource.Id.viewpager);
			mViewPager.Adapter = new SamplePagerAdapter (CurrentWeather);

			mSlidingTabScrollView.ViewPager = mViewPager;
		}


		public class SamplePagerAdapter : PagerAdapter
		{
			private List<Weather.WeatherData> CurrentWeather = new List<Weather.WeatherData> ();
			List<string> items = new List<string>();

			/// <summary>
			/// Populates tab strips at the screen top with names of cities
			/// </summary>
			/// <param name="currentWeather">List of weather in cities</param>
			public SamplePagerAdapter(List<Weather.WeatherData> currentWeather) : base()
			{
				CurrentWeather = currentWeather;

				foreach( var weather in CurrentWeather)	{
					string tab = weather.data.request[0].query.Contains(",") ? weather.data.request[0].query.Substring( 0 ,  weather.data.request[0].query.IndexOf(",")) : weather.data.request[0].query;
					items.Add(tab);
				}
			}

			/// <summary>
			/// Override the number of tabs
			/// </summary>
			/// <value>The count.</value>
			public override int Count
			{
				get{ return items.Count; }
			}

			public override bool IsViewFromObject(View view, Java.Lang.Object obj)
			{
				return view == obj;
			}


			public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
			{
				View view = LayoutInflater.From (container.Context).Inflate (Resource.Layout.pager_item, container, false);
				container.AddView (view);

				var listView = view.FindViewById<ExpandableListView> (Resource.Id.myExpandableListview);
				listView.SetAdapter (new ExpandableDataAdapter (CurrentWeather[position]));

				return view;
			}

			public string GetHeaderTitle(int position)
			{
				return items [position];
			}


			public override void DestroyItem (ViewGroup container, int position, Java.Lang.Object obj)
			{
				container.RemoveView ((View)obj);
			}
		}
	}
}