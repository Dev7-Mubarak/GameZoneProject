using GameZone.Areas.Owner.ModelViewOwner;
using GameZone.Data;
using GameZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameZone.Areas.Owner.Controllers
{
    [Area("Owner")]
    [Route("Owner/Reservations/[action]")]
    public class HomeController : Controller
    {
        private readonly AppDBContext _context;

        public HomeController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var station = _GetOwnerStation();
            if (station == null)
            {
                return NotFound("Game station not found.");
            }

            var viewModel = new StationDashboardViewModel
            {
                Station = station,
                PendingCount = await GetReservationCount(station.Id, ReservationStatus.Pending),
                CompletedCount = await GetReservationCount(station.Id, ReservationStatus.Completed),
                DeniedCount = await GetReservationCount(station.Id, ReservationStatus.Denied)
            };

            return View(viewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            var station = await _context.GameStations
                .Include(gs => gs.Reservations)
                .FirstOrDefaultAsync(gs => gs.Id == id);

            if (station == null)
            {
                return NotFound();
            }

            var viewModel = new StationDashboardViewModel
            {
                Station = station,
                PendingCount = await GetReservationCount(id, ReservationStatus.Pending),
                CompletedCount = await GetReservationCount(id, ReservationStatus.Completed),
                DeniedCount = await GetReservationCount(id, ReservationStatus.Denied)
            };

            return View(viewModel);
        }

        private async Task<int> GetReservationCount(int stationId, ReservationStatus status)
        {
            return await _context.Reservations
                .AsNoTracking()
                .CountAsync(r => r.GameStationId == stationId && r.Satuts == status);
        }

        public IActionResult ReservationLog()
        {
            var reservations = _context.Reservations
                .Include(r => r.Room)
                .Include(r => r.PaymentMethod)
                .Select(r => new ReservationViewModel
                {
                    Id = r.Id,
                    RoomName = r.Room.Name,
                    Date = r.Date,
                    StartHour = r.StartHour,
                    NumberOfHours = r.NumberOfHours,
                    TotalPrice = r.TotalPrice,
                    PaymentMethod = r.PaymentMethod.Name,
                    Status = r.Satuts.ToString(),
                    Image = r.DepositImage,
                    GameStationId = r.GameStationId,
                })
                .Where(x => x.GameStationId == _GetOwnerStation().Id)
                .ToList();

            return View(reservations);
        }
        [HttpPost]
        public IActionResult UpdateReservationStatus([FromBody] ReservationStatusUpdateModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid data. Please check the request format.");
            }

            var reservation = _context.Reservations.Find(model.Id);
            if (reservation == null)
            {
                return NotFound("Reservation not found.");
            }

            try
            {

                if (Enum.IsDefined(typeof(ReservationStatus), model.Status))
                {
                    reservation.Satuts = model.Status;
                }
                else
                {
                    return BadRequest("Invalid status value.");
                }

                _context.SaveChanges();
                return Ok("Reservation status updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
