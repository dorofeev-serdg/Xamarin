using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Weather
{
	public class IpClient
	{
		public static readonly string IP_URL = @"http://api.ipify.org?format=json";

		/// <summary>
		/// Function to get external Ip of the client
		/// </summary>
		/// <returns>External Ip of the client.</returns>
		public static async Task<Ip> GetMyIp()
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(IP_URL);

			using (HttpWebResponse response =  (HttpWebResponse)(await request.GetResponseAsync ())) {
				using (Stream resStream = response.GetResponseStream ()) {
					using (StreamReader reader = new StreamReader (resStream)) {
						string jsonResult = await reader.ReadToEndAsync ();

						var result = JsonConvert.DeserializeObject<Ip> (jsonResult);
						return result;
					}
				}
			}
		}


	}
}

