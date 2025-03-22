using Microsoft.AspNetCore.Mvc;

namespace GameZone.Areas.Owner.Controllers
{
    [Area("Owner")]
    public class RoomsController : Controller
    {
        public RoomsController()
        {
            
        }

        public IActionResult GetAll()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
