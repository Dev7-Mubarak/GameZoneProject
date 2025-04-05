using GameZone.Data;
using GameZone.Models;
using GameZone.ViewModels.Games;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Controllers
{
	public class GamesController : Controller
	{
		private readonly AppDBContext _context;

        public GamesController(AppDBContext context)
        {
            _context = context;
        }

		// All Categories with its Games
        public IActionResult GetAll()
		{
			var categories = _context.Categories
									 .Include(c => c.Games)
									 .OrderBy(c => c.Name)
									 .Where(c => c.Games.Any())
									 .Select(c => new GamesInCategoryVM()
									 {
										 Category = c,
										 Games = c.Games.OrderBy(g => g.Name)
														.Take(4)
														.ToList(), // Display Only 4 Games From The Category
										 totalGamesCount = c.Games.Count()
									 })
									 .ToList();

			return View(categories);
		}

        public IActionResult GetGamesBasedOnCategory(int categoryId, string searchTerm = "")
        {
			var allGamesInCategory = _context.Categories
											 .Include(c => c.Games)
											 .FirstOrDefault(c => c.Id == categoryId);

            if (allGamesInCategory is null)
                return NotFound();

            var filteredGames = allGamesInCategory.Games
												  .Where(g => g.Name.ToLower()
												  .Contains(searchTerm.ToLower()))
												  .ToList();

            var viewModel = new GamesInCategoryVM
            {
                Category = allGamesInCategory,
                Games = filteredGames,
				SearchTerm = searchTerm
            };

            return View(viewModel);
        }

        public IActionResult GameDetails(int gameId)
		{
            var game = _context.Games
							   .Include(g => g.Category)
							   .Include(g => g.GameStationGames)
							   .ThenInclude(gsg => gsg.GameStation)
							   .Include(g => g.Devices)
							   .FirstOrDefault(g => g.Id == gameId);

            if (game is null)
                return NotFound();

            var viewModel = new GameDetailsVM
            {
                Game = game,
                AvailableStations = game.GameStationGames.Select(gsg => gsg.GameStation).ToList(),
                SupportedDevices = game.Devices.ToList()
            };

            return View(viewModel);
		}
	}
}
