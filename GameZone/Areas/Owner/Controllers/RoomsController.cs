using GameZone.Areas.Owner.ModelViewOwner;
using GameZone.Data;
using GameZone.Helpers;
using GameZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameZone.Areas.Owner.Controllers
{
    [Area("Owner")]
    public class RoomsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagePath;

        public RoomsController(AppDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.RoomsFilePath}";
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<IActionResult> GetAll()
        {
            var currentUserId = GetUserId();
            var rooms = await _context.Rooms
                .Where(r => r.GameStation.UserId == currentUserId)
                .Select(r => new RoomViewModel
                {
                    Id = r.Id,
                    RoomName = r.Name,
                    Price = r.Price,
                    NumberOfAllowedPeople = r.NumberOfAllowedPeople,
                    PrimaryImage = r.PrimaryImage,
                    Unit = r.Unit,
                    GameStationId = (int)r.GameStationId,
                    ExistingAdditionalImages = r.RoomsPictures.Select(p => p.Image).ToList()
                })
                .ToListAsync();

            return View(rooms);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var currentUserId = GetUserId();
            var room = await _context.Rooms
                                     .Include(r => r.RoomsPictures)
                                     .FirstOrDefaultAsync(m => m.Id == id && m.GameStation.UserId == currentUserId);

            if (room == null)
                return NotFound();

            return View(room);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var currentStationId = _GetOwnerStation()?.Id;

            if (currentStationId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RoomViewModel
            {
                GameStationId = currentStationId.Value
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.GameStationId <= 0)
            {
                ModelState.AddModelError("", "Invalid Game Station");
                return View(model);
            }

            var gameStation = await _context.GameStations
                .FirstOrDefaultAsync(gs => gs.Id == model.GameStationId);

            if (gameStation == null)
            {
                ModelState.AddModelError("GameStationId", "Selected game station doesn't exist");
                return View(model);
            }


            var room = new Room
            {
                Name = model.RoomName,
                Price = model.Price,
                NumberOfAllowedPeople = model.NumberOfAllowedPeople,
                GameStationId = model.GameStationId,
                Unit = model.Unit,
            };


            if (model.PrimaryImageFile != null)
            {
                var result = await Utilities.SaveFileAsync(model.PrimaryImageFile, _imagePath);
                if (result.Succeeded)
                {
                    room.PrimaryImage = result.FileName;
                }
            }


            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return RedirectToAction("AddMoreImages1", "Rooms", new
            {
                area = "Owner",
                roomId = room.Id
            });
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var currentUserId = GetUserId();
            var room = await _context.Rooms
                .Include(r => r.RoomsPictures)
                .Include(r => r.GameStation)
                .FirstOrDefaultAsync(r => r.Id == id && r.GameStation.UserId == currentUserId);

            if (room == null)
            {
                return NotFound();
            }

            var model = new RoomViewModel
            {
                Id = room.Id,
                RoomName = room.Name,
                Price = room.Price,
                NumberOfAllowedPeople = room.NumberOfAllowedPeople,
                GameStationId = room.GameStationId,
                Unit = room.Unit,
                PrimaryImage = room.PrimaryImage,
                ExistingAdditionalImages = room.RoomsPictures.Select(p => p.Image).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid) // Fixed the condition check
            {
                return View(model);
            }

            var currentUserId = GetUserId();
            var room = await _context.Rooms
                .Include(r => r.RoomsPictures)
                .Include(r => r.GameStation)
                .FirstOrDefaultAsync(r => r.Id == id && r.GameStation.UserId == currentUserId);

            if (room == null)
                return NotFound();

            // Validate GameStation exists
            var gameStation = await _context.GameStations.FindAsync(model.GameStationId);
            if (gameStation == null)
            {
                ModelState.AddModelError("GameStationId", "Selected game station doesn't exist");
                return View(model);
            }

            // Update room properties
            room.Name = model.RoomName;
            room.Price = model.Price;
            room.NumberOfAllowedPeople = model.NumberOfAllowedPeople;
            room.GameStationId = model.GameStationId;
            room.Unit = model.Unit;

            // Handle image upload
            if (model.PrimaryImageFile != null)
            {
                var result = await Utilities.SaveFileAsync(model.PrimaryImageFile, _imagePath);
                if (result.Succeeded)
                {
                    // Delete old image if exists
                    if (!string.IsNullOrEmpty(room.PrimaryImage))
                    {
                        var oldImagePath = Path.Combine(_imagePath, room.PrimaryImage);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    room.PrimaryImage = result.FileName;
                }
            }


            _context.Update(room);
            await _context.SaveChangesAsync();


            return RedirectToAction("AddMoreImages1", new { roomId = room.Id });

        }



        [HttpGet]
        public async Task<IActionResult> AddMoreImages1(int roomId)
        {
            var currentUserId = GetUserId();
            var room = await _context.Rooms
                .Include(r => r.RoomsPictures)
                .Include(r => r.GameStation)
                .FirstOrDefaultAsync(r => r.Id == roomId && r.GameStation.UserId == currentUserId);

            if (room == null)
                return NotFound();

            var model = new AddMoreImagesViewModel
            {
                RoomId = room.Id,
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMoreImages(AddMoreImagesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var currentUserId = GetUserId();
                var room = await _context.Rooms
                                         .Include(r => r.RoomsPictures)
                                         .FirstOrDefaultAsync(r => r.Id == model.RoomId && r.GameStation.UserId == currentUserId);
                if (room == null)
                    return NotFound();

                if (model.Files != null && model.Files.Any())
                {
                    foreach (var file in model.Files)
                    {
                        if (file != null)
                        {
                            var result = await Utilities.SaveFileAsync(file, _imagePath);

                            if (result.Succeeded)
                            {
                                room.RoomsPictures.Add(new RoomsPicture { Image = result.FileName });
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(GetAll));
            }
            return View(model);
        }





        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUserId = GetUserId();
            var room = await _context.Rooms
                                     .Include(r => r.RoomsPictures)
                                     .FirstOrDefaultAsync(r => r.Id == id && r.GameStation.UserId == currentUserId);

            if (room == null)
            {
                return NotFound(new { message = "Room not found" });
            }


            var reservations = await _context.Reservations.Where(r => r.RoomId == id).ToListAsync();
            _context.Reservations.RemoveRange(reservations);


            if (!string.IsNullOrEmpty(room.PrimaryImage))
            {
                DeleteImage(room.PrimaryImage);
            }
            foreach (var image in room.RoomsPictures)
            {
                DeleteImage(image.Image);
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Room deleted successfully" });
        }



        private void DeleteImage(string imagePath)
        {
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath.TrimStart('/'));
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
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
