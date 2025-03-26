using GameZone.Data;
using GameZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameZone.Areas.Owner.Controllers
{
    [Area("Owner")]
    public class HomeController : Controller
    {
        private readonly AppDBContext _context;
        private static GameStation? _ownerStation;
        private string? _ownerId;

        public HomeController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            _ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _ownerStation = _context.GameStations.FirstOrDefault(x => x.UserId == _ownerId);

            return View(_ownerStation);
        }

        public IActionResult ReservationLog()
        {
            var reservations = _context.Reservations
                .Include(r => r.Room)
                .Include(r => r.PaymentMethod)
                .Include(r => r.User)
                .Where(r => r.GameStationId == _ownerStation.Id)
                .ToList();


            return View(reservations);
        }


        public IActionResult Edit()
        {
            return View();
        }


        public IActionResult AcceptedReservation()
        {
            return View();
        }

        public IActionResult RejectedReservation()
        {
            return View();
        }

    }
}
