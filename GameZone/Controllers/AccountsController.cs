using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class AccountsController : Controller
    {
        public AccountsController()
        {
            
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
    }
}
