using GameZone.Areas.Owner.ModelViewOwner;
using GameZone.Data;
using GameZone.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameZone.Areas.Owner.Controllers
{
    [Area("Owner")]
    [Route("Owner/[controller]/[action]")]
    public class GamesController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GamesController(AppDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> GetAll()
        {
            var games = await _context.GameStationGames
                .Include(gs => gs.Game)
                    .ThenInclude(g => g.Category)  // Include Category through Game
                .Select(gs => gs.Game)
                .Distinct()
                .ToListAsync();

            return View(games);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var games = _context.Games.ToList();
            var model = new GameViewModel
            {
                GameList = games.Select(g => new SelectListItem
                {
                    Text = g.Name,
                    Value = g.Id.ToString()
                    
                }),
               
                
            };


            return View(model);
        }


        [HttpPost]
        public IActionResult Add(int gameId)
        {
            var stationId = _GetOwnerStation().Id;
            var game = _context.Games.Find(gameId);

            if (game == null)
            {
                return NotFound("Game not found");
            }

            var model = new GameStationGame
            {
                GameStationId = stationId,
                GameId = gameId
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int gameId))
            {
                return BadRequest("Invalid game ID.");
            }

            var game = _context.Games
                .Include(g => g.Category)
                .FirstOrDefault(g => g.Id == gameId);

            if (game == null)
            {
                return NotFound("Selected game not found.");
            }

            var model = new GameViewModel
            {
                SelectedGame = game.Name,
                Category = game.Category?.Name ?? "Unknown",
                Description = game.Descraption,
                CoverImagePath = game.Cover
            };

            return Json(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GameViewModel model)
        {
            
            var gameStationId = _GetOwnerStation()?.Id;
            if (gameStationId == null)
            {
                ModelState.AddModelError("", "Game station not found");
                return View(model);
            }

           
            if (model.Id <= 0) 
            {
                ModelState.AddModelError("Id", "Please select a valid game");
                return View(model);
            }

           
            var game = _context.Games.Find(model.Id);
            if (game == null)
            {
                ModelState.AddModelError("", "Selected game not found");
                return View(model);
            }

            
            var existingAssignment = _context.GameStationGames
                .FirstOrDefault(g => g.GameId == model.Id && g.GameStationId == gameStationId);

            if (existingAssignment != null)
            {
                ModelState.AddModelError("", "This game is already assigned to your station");
                return View(model);
            }

           
            var gameStationGame = new GameStationGame
            {
                GameId = model.Id,
                GameStationId = gameStationId.Value,
               
            };

            _context.GameStationGames.Add(gameStationGame);
            _context.SaveChanges();

            return RedirectToAction("GetAll");
        }
        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var gameStationId = await _context.Users
                .Where(u => u.Id == _GetOwnerId())
              
                 .FirstOrDefaultAsync();

                if (gameStationId is null)
                    return BadRequest(new { message = "User doesn't manage any game station" });

                // 2. Find the junction table entry
                var gameStationGame = await _context.GameStationGames
                    .FirstOrDefaultAsync(gsg => gsg.GameId == id);
                      
                      
                   

                if (gameStationGame == null)
                    return NotFound(new { message = "Game not found in this station" });

                // 3. Remove the relationship
                _context.GameStationGames.Remove(gameStationGame);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Game removed from station successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
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
