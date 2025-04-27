using GameZone.Areas.Admin.ViewModels;
using GameZone.Data;
using GameZone.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Admin)]
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

        public async Task<IActionResult> RoomsInStation(int stationId)
        {
            var station = await _context.GameStations
                                                 .Include(s => s.Rooms)
                                                 .FirstOrDefaultAsync(s => s.Id == stationId);

            if (station is null)
            {
                return NotFound();
            }

            var roomsInStation = new RoomsInStationVM
            {
                StationName = station.Name,
                Rooms = station.Rooms.ToList()
            };

            return View(roomsInStation);
        }
    }
}
