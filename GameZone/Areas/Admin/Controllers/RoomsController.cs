using GameZone.Areas.Admin.ViewModels;
using GameZone.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomsController : Controller
    {
        private readonly AppDBContext _context;

        public RoomsController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult AllRooms()
        {
            var allRooms = new StationsRoomsVM()
            {
                AllStationsRooms = _context.Rooms
                                   .Include(r => r.GameStation)
                                   .ToList(),
            };

            return View(allRooms);
        }

        public IActionResult RoomsInStation()
        {
            return View();
        }
    }
}
