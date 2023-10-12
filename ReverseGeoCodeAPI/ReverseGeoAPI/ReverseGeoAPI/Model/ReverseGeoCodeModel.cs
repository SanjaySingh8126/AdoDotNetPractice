namespace ReverseGeoAPI.Model
{
	public class ReverseGeoCodeModel
	{

        public string GeoLocationSummery { get; set; }
		public string SubDistrict { get; set; }
		public string District { get; set; }
		public string State { get; set; }
		public string Pincode { get; set; }
		public int ActivityId { get; set; }	


	}

	public class MapMyIndiaresponse
	{
		public int? responseCode { get; set; }

		public string version { get; set; }

		public List<Result> results { get; set; }
	}

	public class Result
	{
		public string houseNumber { get; set; }
		public string houseName { get; set; }
		public string poi { get; set; }
		public string poi_dist { get; set; }
		public string street { get; set; }
		public string street_dist { get; set; }
		public string subSubLocality { get; set; }
		public string subLocality { get; set; }
		public string locality { get; set; }
		public string village { get; set; }
		public string district { get; set; }
		public string subDistrict { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string pincode { get; set; }
		public string lat { get; set; }
		public string lng { get; set; }
		public string formatted_address { get; set; }
		public string area { get; set; }            //Added by Mahima
	}

	public class MapMyIndiaRequest
	{
		public string Latitude { get; set; }
		public string Longitude { get; set; }
	}
}
