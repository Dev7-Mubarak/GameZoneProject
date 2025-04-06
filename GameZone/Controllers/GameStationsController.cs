using GameZone.Data;
using GameZone.Helpers;
using GameZone.Models;
using GameZone.Settings;
using GameZone.ViewModels.GameStations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Controllers
{
    public class GameStationsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagePath;


        public GameStationsController(AppDBContext context, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.DepositsFilePath}";
        }

        public IActionResult GetAll()
        {
            var stations = _context.GameStations;

            return View(stations);
        }
        public IActionResult StationDetails(int id)
        {
            var station = _context.GameStations
                .Include(gs => gs.GameStationGames)
                    .ThenInclude(gsg => gsg.Game)
                .Include(gs => gs.Rooms)
                .Include(gs => gs.Ratings)
                .FirstOrDefault(x => x.Id == id);

            if (station == null)
            {
                return NotFound();
            }

            var viewModel = new GameStationDetailsVM
            {
                GameStation = new GameStationVM
                {
                    Id = station.Id,
                    Name = station.Name,
                    Descraption = station.Descraption,
                    Location = station.Location,
                    Rating = station.Rating,
                    MorningOpenTime = station.MorningOpenTime,
                    MorningCloseTime = station.MorningCloseTime,
                    EveningOpenTime = station.EveningOpenTime,
                    EveningCloseTime = station.EveningCloseTime,
                    PhoneNumber1 = station.PhoneNumber1,
                    PhoneNumber2 = station.PhoneNumber2,
                    Image = station.Image,
                    Games = station.GameStationGames?
                        .Select(gsg => gsg.Game)
                        .ToList() ?? new List<Game>(),
                    Rooms = station.Rooms?.ToList() ?? new List<Room>(),
                    User = station.User,
                },
                Reservation = new ReservationVM
                {
                    GameStationId = station.Id
                }
            };

            return View(viewModel);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateReservation(GameStationDetailsVM model)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            var room = _context.Rooms.Find(model.Reservation.RoomId);
            if (room == null)
            {
                ModelState.AddModelError("", "Invalid room selection");
                return RedirectToAction("StationDetails", new { id = model.Reservation.GameStationId });
            }

            var reservation = new Reservation
            {
                UserId = user.Id,
                ReservationName = model.Reservation.ReservationName,
                GameStationId = model.Reservation.GameStationId,
                RoomId = room.Id,
                Date = model.Reservation.ReservationDate,
                StartHour = model.Reservation.StartHour,
                NumberOfHours = (short)model.Reservation.DurationHours,
                PaymentType = model.Reservation.PaymentType,
                Satuts = ReservationStatus.Pending,
                TotalPrice = room.Price * model.Reservation.DurationHours,
            };

            if (model.Reservation.PaymentType == PaymentType.Deposit)
            {
                var result = await Utilities.SaveFileAsync(model.Reservation.PaymentReceipt, _imagePath);
                if (result.Succeeded)
                {
                    reservation.DepositImage = result.FileName;
                }
            }

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction("UserReservationLog", "Home");
        }
    }
}
