using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReverseGeoAPI.BAL.ReverseGeoService;

namespace ReverseGeoAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReverseGeoCodeController : ControllerBase
	{
		private IReverseGeoCodeService _context;

		public ReverseGeoCodeController(IReverseGeoCodeService reverseGeoCodeService)
        {
            _context = reverseGeoCodeService;
        }
		[HttpGet]
		[Route("GetData")]
		public IActionResult Get()
		{
			_context.ReverseGeoCode();	
			return Ok();
		}

    }
}
