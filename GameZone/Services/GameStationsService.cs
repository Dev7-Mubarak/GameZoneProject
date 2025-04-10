using GameZone.Data;
using GameZone.ViewModels.GameStations;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class GameStationsService : IGameStationsService
    {
        private readonly AppDBContext _context;

        public GameStationsService(AppDBContext context)
        {
            _context = context;
        }

        public IEnumerable<PopularGameStationsVM> GetPopularGameStations()
        {
            var stations = _context.GameStations
                .OrderByDescending(gs => gs.Rating)
                .Take(4)
                .Select(gs => new PopularGameStationsVM()
                {
                    Id = gs.Id,
                    Name = gs.Name,
                    Image = gs.Image,
                    Rating = gs.Rating,
                })
                .ToList();

            return stations;
        }
    }
}
