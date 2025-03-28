using GameZone.Areas.Admin.ViewModels;
using GameZone.Data;
using GameZone.Models;
using GameZone.Settings;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameZone.Areas.Admin.Services
{
    public class GamesService : IGamesService
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _gameImagesPath;

        public GamesService(AppDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _gameImagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.GamesImagesPath}";
        }

        public IEnumerable<Game> GetAllGames()
        {
            var games = _context.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ToList();
            return games;
        }

        public Game? GetById(int id)
        {
            var games = _context.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ToList()
                .SingleOrDefault(g => g.Id == id);
            return games;
        }



        public async Task CreateGame(CreateGameFormVM model)
        {
            var coverName = await SaveCover(model.Cover);

            var devices = _context.Devices.Where(d => model.SelectedDevices.Contains(d.Id)).ToList();

            Game game = new()
            {
                Name = model.Name,
                Descraption = model.Descraption,
                Developer = model.Developer,
                Release = model.Release,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Devices = devices
            };

            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public async Task<Game?> EditGame(EditGameFormVM model)
        {
            var game = _context.Games
                .Include(g => g.Devices)
                .SingleOrDefault(g => g.Id == model.Id);

            if (game is null)
            {
                return null;
            }

            var devices = _context.Devices.Where(d => model.SelectedDevices.Contains(d.Id)).ToList();

            var HasNewCover = model.Cover is not null;
            var currentCover = game.Cover;

            game.Name = model.Name;
            game.Descraption = model.Descraption;
            game.Developer = model.Developer;
            game.Release = model.Release;
            game.CategoryId = model.CategoryId;
            game.Devices = devices;

            if (HasNewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }

            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                if (HasNewCover)
                {
                    var oldCover = Path.Combine(_gameImagesPath, currentCover);
                    File.Delete(oldCover);
                }

                return game;
            }
            else
            {

                var NewCover = Path.Combine(_gameImagesPath, game.Cover);
                File.Delete(NewCover);

                return null;
            }
        }

        public bool Delete(int id)
        {
            var IsDeleted = false;

            var game = _context.Games.Find(id);

            if (game is null)
                return IsDeleted;

            _context.Games.Remove(game);
            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                IsDeleted = true;

                var cover = Path.Combine(_gameImagesPath, game.Cover);
                File.Delete(cover);
            }

            return IsDeleted;
        }

        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_gameImagesPath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;
        }        
    } 
}
