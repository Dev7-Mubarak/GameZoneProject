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

        public HomeController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_GetOwnerStation());
        }

        public IActionResult ReservationLog()
        {
            var ownerStation = _GetOwnerStation();

            if (ownerStation == null)
            {
                return NotFound("Owner station not found.");
            }

            var reservations = _context.Reservations
                .Include(r => r.Room)
                .Include(r => r.PaymentMethod)
                .Include(r => r.User)
                .Where(r => r.GameStationId == ownerStation.Id)
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
        private GameStation? _GetOwnerStation()
        {
            var ownerId = _GetOwnerId();
            return _context.GameStations.FirstOrDefault(x => x.UserId == ownerId);
        }
        private string? _GetOwnerId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

    }
}
