using ReverseGeoAPI.Model;
using System.Data;

namespace ReverseGeoAPI.DAL.ReverseGeoRepository
{
	public interface IReverseGeoCodeRepository
	{
		DataTable GetReverseGeoCode();
	    void  UpdateReverseGeoCode(ReverseGeoCodeModel source);	
	}
}
