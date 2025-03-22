using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ILogger<HomeController> logger)
        {  
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserIndex()
        {
            return View();
        }

        public IActionResult UserReservationLog()
        {
            return View();
        }

        public IActionResult AccountDetails()
        {
            return View();
        }

        public IActionResult EditAccountDetails()
        {
            return View();
        }

        public IActionResult Team()
        {
            return View();
        }

        public IActionResult Vision()
        {
            return View();
        }

        public IActionResult TermsAndConditions()
        {
            return View();
        }

        public IActionResult PrivacyAndPolicy()
        {
            return View();
        }
    }
}
