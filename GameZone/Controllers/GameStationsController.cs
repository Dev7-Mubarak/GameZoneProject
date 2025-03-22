using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
	public class GameStationsController : Controller
	{
        public GameStationsController()
        {
            
        }

        public IActionResult GetAll()
		{
			return View();
		}

		public IActionResult StationDetails()
		{
			return View();
		}

		public IActionResult StationReview()
		{
			return View();
		}

    }
}
