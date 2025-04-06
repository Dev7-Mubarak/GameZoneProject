using GameZone.Areas.Admin.Services;
using GameZone.Areas.Admin.ViewModels;
using GameZone.Data;
using GameZone.Models;
using Microsoft.AspNetCore.Mvc;


namespace GameZone.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GamesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesService _devicesService;
        private readonly IGamesService _gamesService;
        private readonly AppDBContext _context;


        public GamesController(ICategoriesService categoriesService, IDevicesService devicesService, IGamesService gamesService, AppDBContext context)
        {
            _categoriesService = categoriesService;
            _devicesService = devicesService;
            _gamesService = gamesService;
            _context = context;
        }
        public IActionResult Index()
        {
            var games = _gamesService.GetAllGames();
            return View(games);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormVM viewModel = new()
            {
                Categories = _categoriesService.GetSelectList(),
                Devices = _devicesService.GetSelectList()

            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormVM model)
        {
            if (model.SelectedDevices == null || !model.SelectedDevices.Any())
            {
                ModelState.AddModelError("SelectedDevices", "Please select at least one device.");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();
                model.Devices = _devicesService.GetSelectList();
                return View(model);
            }

            await _gamesService.CreateGame(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gamesService.GetById(id);

            if (game is null)
            {
                return NotFound();
            }

            EditGameFormVM viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Descraption = game.Descraption,
                CategoryId = game.CategoryId,
                Developer = game.Developer,
                Release = game.Release,
                GameCover = game.Cover,
                SelectedDevices = game.Devices.Select(d => d.Id).ToList(),
                Categories = _categoriesService.GetSelectList(),
                Devices = _devicesService.GetSelectList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormVM model)
        {
            if (model.SelectedDevices == null || !model.SelectedDevices.Any())
            {
                ModelState.AddModelError("SelectedDevices", "Please select at least one device.");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();
                model.Devices = _devicesService.GetSelectList();
                return View(model);
            }

            var game = await _gamesService.EditGame(model);

            if (game is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var IsDeleted = _gamesService.Delete(id);

            if (IsDeleted)
                return Ok();
            else
                return BadRequest();
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

        public IActionResult CheckIfNameExists(GamesCategoriesVM model)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Name == model.Name);
            var isAllowed = category is null || category.Id.Equals(model.Id);

            return Json(isAllowed);
        }
    }
}
