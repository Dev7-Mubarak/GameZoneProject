using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
	public class GamesController : Controller
	{
        public GamesController()
        {
            
        }
        public IActionResult GetAll()
		{
			return View();
		}

		public IActionResult GameDetails()
		{
			return View();
		}

		public IActionResult GetGamesBasedOnCategory()
		{
			return View();
		}
	}
}
