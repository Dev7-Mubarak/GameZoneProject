using GameZone.Areas.Admin.ViewModels;
using GameZone.Data;
using GameZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GamesController : Controller
    {
        private readonly AppDBContext _context;

        public GamesController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GamesInStation(int stationId)
        {
            var station = await _context.GameStations
                                           .Include(gs => gs.GameStationGames)
                                           .ThenInclude(gsg => gsg.Game)
                                           .ThenInclude(g => g.Category)
                                           .FirstOrDefaultAsync(gs => gs.Id == stationId);

            if (station is null)
            {
                return NotFound();
            }

            var gamesInStation = new GamesInStationVM
            {
                StationName = station.Name,
                Games = station.GameStationGames
                               .Select(gsg => gsg.Game)
                               .ToList()
            };

            return View(gamesInStation);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult AllGameCategories()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(GamesCategoriesVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var category = new Category() { Name = model.Name };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(AllGameCategories));
        }

        [HttpGet]
        public IActionResult EditCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if (category is null)
                return NotFound();

            var gamecategory = new GamesCategoriesVM { Id = categoryId, Name = category.Name };

            return View(gamecategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(GamesCategoriesVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var category = _context.Categories.Find(model.Id);
            if (category is null)
                return NotFound();

            category.Name = model.Name;

            _context.SaveChanges();

            return RedirectToAction(nameof(AllGameCategories));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category is null)
                return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Ok();
        }
    }
}
