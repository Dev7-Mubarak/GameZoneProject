using GameZone.Areas.Admin.ViewModels;
using GameZone.Constants;
using GameZone.Data;
using GameZone.Helpers;
using GameZone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace GameZone.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            _imagePath = $"{_webHostEnvironment.WebRootPath}{StationCoverSettings.stationFilePath}";
        }

        public IActionResult Index()
        {
            var stations = _context.GameStations
                                   .Include(s => s.User)
                                   .Include(s => s.Rooms)
                                   .Include(s => s.GameStationGames)

                                   .Select(gs => new GetAllStationsVM()
                                   {
                                       Id = gs.Id,
                                       Name = gs.Name,
                                       Image = gs.Image,
                                       Location = gs.Location,
                                       PhoneNumber1 = gs.PhoneNumber1,
                                       PhoneNumber2 = gs.PhoneNumber2,
                                       RoomsCount = gs.Rooms.Count,
                                       OwnerName = $"{gs.User.FisrtName} {gs.User.LastName}",
                                   });

            return View(stations);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var ownersUsers = await _userManager.GetUsersInRoleAsync(Role.Owner.ToString());

            var model = new CreateGameStationVM()
            {
                Users = ownersUsers.Select(u => new SelectListItem()
                {
                    Value = u.Id,
                    Text = $"{u.FisrtName} {u.LastName}"
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameStationVM model)
        {
            if (!ModelState.IsValid)
            {
                var ownersUsers = await _userManager.GetUsersInRoleAsync(Role.Owner.ToString());
                model.Users = ownersUsers.Select(u => new SelectListItem()
                {
                    Value = u.Id,
                    Text = $"{u.FisrtName} {u.LastName}"
                }).ToList();

                return View(model);
            }

            var cover = (await Utilities.SaveFileAsync(model.Cover, _imagePath));
            var coverName = cover.FileName;

            var gameStation = new GameStation()
            {
                Name = model.Name,
                Location = model.Location,
                PhoneNumber1 = model.PhoneNumber1,
                PhoneNumber2 = model.PhoneNumber2,
                MorningOpenTime = model.MorningOpenTime,
                MorningCloseTime = model.MorningCloseTime,
                EveningOpenTime = model.EveningOpenTime,
                EveningCloseTime = model.EveningCloseTime,
                Descraption = model.Description,
                UserId = model.UserId,
                Image = coverName,
            };

            _context.GameStations.Add(gameStation);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index), "GameStations");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int stationId)
        {
            var station = _context.GameStations.Find(stationId);
            if (station is null)
                return NotFound();

            var ownerUser = await _userManager.GetUsersInRoleAsync(Role.Owner.ToString());

            var model = new EditGameStationVM
            {
                Id = station.Id,
                Name = station.Name,
                Location = station.Location,
                PhoneNumber1 = station.PhoneNumber1,
                PhoneNumber2 = station.PhoneNumber2,
                MorningOpenTime = station.MorningOpenTime,
                MorningCloseTime = station.MorningCloseTime,
                EveningOpenTime = station.EveningOpenTime,
                EveningCloseTime = station.EveningCloseTime,
                Description = station.Descraption,
                UserId = station.UserId,
                Users = ownerUser.Select(u => new SelectListItem()
                {
                    Value = u.Id,
                    Text = $"{u.FisrtName} {u.LastName}"
                }).ToList(),
                CurrentCover = station.Image,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameStationVM model)
        {
            if (!ModelState.IsValid)
            {
                var ownersUsers = await _userManager.GetUsersInRoleAsync(Role.Owner.ToString());
                model.Users = ownersUsers.Select(u => new SelectListItem()
                {
                    Value = u.Id,
                    Text = $"{u.FisrtName} {u.LastName}"
                }).ToList();

                return View(model);
            }

            var station = _context.GameStations.Find(model.Id);
            if (station is null)
                return NotFound();

            bool hasNewCover = model.Cover is not null;
            var oldCover = station.Image;
            var NewCoverName = string.Empty;

            station.Name = model.Name;
            station.Location = model.Location;
            station.PhoneNumber1 = model.PhoneNumber1;
            station.PhoneNumber2 = model.PhoneNumber2;
            station.MorningOpenTime = model.MorningOpenTime;
            station.MorningCloseTime = model.MorningCloseTime;
            station.EveningOpenTime = model.EveningOpenTime;
            station.EveningCloseTime = model.EveningCloseTime;
            station.Descraption = model.Description;
            station.UserId = model.UserId;

            if (hasNewCover)
            {
                var cover = await Utilities.SaveFileAsync(model.Cover!, _imagePath);
                NewCoverName = cover.FileName;
                station.Image = NewCoverName;
            }

            _context.Update(station);

            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0) // means applied changes in database
            {
                if (hasNewCover)
                {
                    Utilities.DeleteFile(oldCover!, _imagePath);
                }
            }
            else
            {
                Utilities.DeleteFile(station.Image!, _imagePath);
            }

            return RedirectToAction(nameof(Index), "GameStations");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var station = _context.GameStations.Find(id);
            if (station is null)
                return NotFound();

            if (!string.IsNullOrEmpty(station.Image))
                Utilities.DeleteFile(station.Image, _imagePath);

            _context.GameStations.Remove(station);
            _context.SaveChanges();

            return Ok();
        }
    }
}
