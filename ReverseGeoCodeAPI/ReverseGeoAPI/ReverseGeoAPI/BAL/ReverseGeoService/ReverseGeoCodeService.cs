using Newtonsoft.Json;
using ReverseGeoAPI.DAL.ReverseGeoRepository;
using ReverseGeoAPI.Model;
using ReverseGeoAPI.SqlHelper;
using System.Data;

namespace ReverseGeoAPI.BAL.ReverseGeoService
{
	public class ReverseGeoCodeService : IReverseGeoCodeService
	{
		private IReverseGeoCodeRepository _context;
		string connection;
        public ReverseGeoCodeService(IReverseGeoCodeRepository reverseGeoCodeRepository)
        {
			_context = reverseGeoCodeRepository;
			connection = $"Data Source=169.38.100.195;Initial Catalog=TheArcherLive;Persist Security Info=True;User ID=CYADMDW;Password=pMZDFSHs35vB;MultipleActiveResultSets=True"; 
        }
        public void ReverseGeoCode()
		{
			DataTable dataTable = _context.GetReverseGeoCode();

			foreach (DataRow row in dataTable.Rows) 
			{
				try
				{
				 var data=LocationData.GetGeoLocationJsonData(row[23].ToString());
					var mydata = new ReverseGeoCodeModel()
					{
						ActivityId = Convert.ToInt32(row[0]),
						State = data.results[0].state,
						SubDistrict = data.results[0].subDistrict,
						Pincode = data.results[0].pincode,
						District = data.results[0].district,
						GeoLocationSummery = $"{data.results[0].state},{data.results[0].district},{data.results[0].subDistrict},{data.results[0].city},{data.results[0].village},{data.results[0].area},{data.results[0].street}"

					};

					_context.UpdateReverseGeoCode(mydata);


				}
				catch(Exception ex) 
				{ 

				}
			}
			
		}


	}
}
