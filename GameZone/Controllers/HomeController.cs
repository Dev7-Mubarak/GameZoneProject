using GameZone.Data;
using GameZone.Helpers;
using GameZone.Models;
using GameZone.Services;
using GameZone.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IGamesService _gamesService;
        private readonly IGameStationsService _gameStationsService;
        private readonly AppDBContext _context;
        public HomeController(UserManager<AppUser> userManager, AppDBContext context, IGamesService gamesService, IGameStationsService gameStationsService)
        {
            _userManager = userManager;
            _context = context;
            _gamesService = gamesService;
            _gameStationsService = gameStationsService;
        }

        public IActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {

                if (User.IsInRole(Role.Admin))
                    return RedirectToAction("Index", "Home", new { area = "Admin" });

                if (User.IsInRole(Role.Owner))
                    return RedirectToAction("Index", "Home", new { area = "Owner" });
            }

            var viewModel = new HomePageVM
            {
                PopularGames = _gamesService.GetPopularGames(),
                PopularGameStations = _gameStationsService.GetPopularGameStations()
            };
            return View(viewModel);
        }
        public IActionResult UserIndex()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserReservationLog()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            var reservations = await _context.Reservations
                .Where(r => r.UserId == user.Id)
                .Include(r => r.GameStation)
                .Include(r => r.Room)
                .Include(r => r.PaymentMethod)
                .OrderByDescending(r => r.Date)
                .ToListAsync();

            return View(reservations);
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
