using Microsoft.AspNetCore.Mvc;

namespace GameZone.Areas.Owner.Controllers
{
    [Area("Owner")]
    public class HomeController : Controller
    { 
        public IActionResult Index()
        {
            return View();
        }
    }
}
