using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class EventsController : Controller
    {
        public EventsController()
        {
            
        }
        public IActionResult GetAll()
        {
            return View();
        }

        public IActionResult EventDetails()
        {
            return View();
        }
    }
}
