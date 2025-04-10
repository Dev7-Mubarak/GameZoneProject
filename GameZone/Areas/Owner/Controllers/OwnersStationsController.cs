using GameZone.Areas.Admin.ViewModels;
using GameZone.Areas.Owner.ModelViewOwner;
using GameZone.Data;
using GameZone.Helpers;
using GameZone.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameZone.Areas.Owner.Controllers
{
    [Area("Owner")]
    public class OwnersStationsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly string _imagePath;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OwnersStationsController(AppDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.StationFilePath}";
        }

        public IActionResult Index()
        {
            return View(_GetOwnerStation());
        }

        public IActionResult Edit(int id)
        {
            var station = _context.GameStations.Find(id);
            if (station == null)
            {
                return NotFound();
            }

            var model = new EditGameStation
            {
                Id = station.Id,
                Name = station.Name,
                Description = station.Descraption,
                Location = station.Location,
                PhoneNumber1 = station.PhoneNumber1,
                PhoneNumber2 = station.PhoneNumber2,
                CurrentCover = station.Image
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameStation model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var station = await _context.GameStations.FindAsync(model.Id);
            if (station == null)
            {
                return NotFound();
            }

            station.Name = model.Name;
            station.Descraption = model.Description;
            station.PhoneNumber1 = model.PhoneNumber1;
            station.PhoneNumber2 = model.PhoneNumber2;
            station.Location = model.Location;

            bool hasNewCover = model.Cover != null && model.Cover.Length > 0;
            string oldCover = station.Image;

            if (hasNewCover)
            {
                var result = await Utilities.SaveFileAsync(model.Cover, _imagePath);

                if (result.Succeeded)
                {
                    station.Image = result.FileName;
                }

                
            }

            _context.Update(station);
            var effectedRows = await _context.SaveChangesAsync();

            if (effectedRows > 0 && hasNewCover && !string.IsNullOrEmpty(oldCover))
            {
                var oldCoverPath = Path.Combine(_imagePath, oldCover);
                if (System.IO.File.Exists(oldCoverPath))
                {
                    System.IO.File.Delete(oldCoverPath);
                }
            }

            return RedirectToAction(nameof(Index), "Home");
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
