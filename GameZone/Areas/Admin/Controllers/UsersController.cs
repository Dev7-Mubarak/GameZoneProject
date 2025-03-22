using Microsoft.AspNetCore.Mvc;

namespace GameZone.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        public UsersController()
        {
            
        }

        public IActionResult Index()
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
