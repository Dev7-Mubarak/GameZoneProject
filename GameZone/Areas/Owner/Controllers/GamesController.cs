using Microsoft.AspNetCore.Mvc;

namespace GameZone.Areas.Owner.Controllers
{
    [Area("Owner")]
    public class GamesController : Controller
    {
        public GamesController()
        {
            
        }

        public IActionResult GetAll()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
