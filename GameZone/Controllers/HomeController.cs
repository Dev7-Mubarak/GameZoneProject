using GameZone.Data;
using GameZone.Helpers;
using GameZone.Models;
using GameZone.Services;
using GameZone.ViewModels;
using GameZone.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IGamesService _gamesService;
        private readonly IGameStationsService _gameStationsService;
        private readonly AppDBContext _context;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        public HomeController(UserManager<AppUser> userManager, AppDBContext context, IGamesService gamesService, IGameStationsService gameStationsService, IPasswordHasher<AppUser> passwordHasher)
        {
            _userManager = userManager;
            _context = context;
            _gamesService = gamesService;
            _gameStationsService = gameStationsService;
            _passwordHasher = passwordHasher;
        }

        public IActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {

                if (User.IsInRole(Role.Admin))
                    return RedirectToAction("Index", "GameStations", new { area = "Admin" });

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

        [HttpGet]
        public async Task<IActionResult> AccountDetails()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var CurrentUser = await _userManager.FindByIdAsync(userId);

            if (CurrentUser == null)
            {
                return NotFound();
            }

            var user = new EditCurentUserVM
            {
                Id = CurrentUser.Id,
                Email = CurrentUser.Email,
                FirstName = CurrentUser.FisrtName,
                LastName = CurrentUser.LastName,
            };
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AccountDetails(EditCurentUserVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }


            user.Email = model.Email;
            user.FisrtName = model.FirstName;
            user.LastName = model.LastName;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
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
