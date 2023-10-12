using ReverseGeoAPI.Model;
using ReverseGeoAPI.SqlHelper;
using System.Data;


namespace ReverseGeoAPI.DAL.ReverseGeoRepository
{
	public class ReverseGeoCodeRepository : IReverseGeoCodeRepository
	{
		private readonly string _conn= "Data Source=169.38.100.195;Initial Catalog=TheArcherLive;Persist Security Info=True;User ID=CYADMDW;Password=pMZDFSHs35vB;MultipleActiveResultSets=True";

	
      

		public DataTable GetReverseGeoCode()
		{
			string query = "SELECT * FROM tblActivityDetails_bkpGeoCode Where State IS NULL OR State=''  ";
			return Myhelper.ExecuteQueryGetDataTable(_conn, query);

		}

		public void UpdateReverseGeoCode(ReverseGeoCodeModel reverseGeoCodeModel)
		{
			string query = $"UPDATE tblActivityDetails_bkpGeoCode SET GeoLocationSummary='{reverseGeoCodeModel.GeoLocationSummery}', SubDistrict='{reverseGeoCodeModel.SubDistrict}', District='{reverseGeoCodeModel.District}', State='{reverseGeoCodeModel.State}', Pincode='{reverseGeoCodeModel.Pincode}' WHERE ActivityId={reverseGeoCodeModel.ActivityId}";
			Myhelper.ExecuteQueryGetDataTable(_conn, query);
				

		}
	}
}
