using System;
using System.Runtime.Serialization;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Weather
{
	public class LocationClient
	{
		private static readonly string LOCATION_URL ="http://ip-api.com/json/{0}";

		/// <summary>
		/// Gets the current location of user.
		/// </summary>
		/// <returns>The location.</returns>
		public static async Task<Location> GetLocation ()
		{
			var ip = await IpClient.GetMyIp ();

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(LOCATION_URL, ip.IP));

			using (HttpWebResponse response =  (HttpWebResponse)await request.GetResponseAsync ())
			{
				using (Stream resStream = response.GetResponseStream ()) 
				{
					using (StreamReader reader = new StreamReader (resStream)) 
					{
						string jsonResult = await reader.ReadToEndAsync ();

						var result = JsonConvert.DeserializeObject<Location> (jsonResult);
						return result;
					}
				}
			}
		}
	}
}

