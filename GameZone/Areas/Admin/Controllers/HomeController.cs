using GameZone.Data;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AppDBContext _context;

        public HomeController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.GameStations);
        }
    }
}
