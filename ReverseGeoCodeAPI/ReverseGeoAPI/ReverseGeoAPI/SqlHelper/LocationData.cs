using Newtonsoft.Json;
using ReverseGeoAPI.Model;
using System.Net.Http.Headers;

namespace ReverseGeoAPI.SqlHelper
{
	public static class LocationData
	{
		public static MapMyIndiaresponse GetGeoLocationJsonData(string GeoLangLat)
		{
			MapMyIndiaresponse geoLocation = null;

			if (GeoLangLat != null)
			{
				string[] str = GeoLangLat.Split(',');
				if (!string.IsNullOrEmpty(str[0]))
				{
					string requestUri = string.Empty;

					if ((Convert.ToDecimal(str[0]) > (decimal)0.0) && (Convert.ToDecimal(str[1]) > (decimal)0.0))
					{
						if (Convert.ToDecimal(str[0]) > Convert.ToDecimal(str[1]))
							requestUri = string.Format("http://apis.mapmyindia.com/advancedmaps/v1/eauvdqw24yprmw9qjqdtdo6t4jt4n921/rev_geocode?lng={0}&lat={1}", str[0], str[1]);
						else
							requestUri = string.Format("http://apis.mapmyindia.com/advancedmaps/v1/eauvdqw24yprmw9qjqdtdo6t4jt4n921/rev_geocode?lng={0}&lat={1}", str[1], str[0]);

						//string requestUri = string.Format("http://apis.mapmyindia.com/advancedmaps/v1/eauvdqw24yprmw9qjqdtdo6t4jt4n921/rev_geocode?lat={0}&lng={1}", str[0], str[1]);
						//Console.WriteLine(requestUri);
						using (var client = new HttpClient())
						{
							try
							{
								client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

								HttpResponseMessage response = client.GetAsync(requestUri).Result;

								if (response.IsSuccessStatusCode)
								{
									geoLocation = JsonConvert.DeserializeObject<MapMyIndiaresponse>(response.Content.ReadAsStringAsync().Result);
									if (geoLocation != null)
										return geoLocation;
								}
							}
							catch (Exception ex)
							{
								return geoLocation;
							}
						}
					}
				}
			}
			return null;
		}
	}
}
