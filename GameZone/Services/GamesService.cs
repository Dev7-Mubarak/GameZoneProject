using GameZone.Data;
using GameZone.Models;
using GameZone.ViewModels.Games;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class GamesService : IGamesService
    {
        private readonly AppDBContext _context;

        public GamesService(AppDBContext context)
        {
            _context = context;
        }

        public IEnumerable<PopularGamesVM> GetPopularGames()
        {
            var popularGames = _context.Games
                .Include(g => g.Category)
                .OrderByDescending(g => g.Release)
                .Take(4)
                .Select(g => new PopularGamesVM
                {
                    Id = g.Id,
                    Name = g.Name,
                    Cover = g.Cover,
                    CategoryName = g.Category.Name 
                })
                .ToList();

            return popularGames;
        }
    }
}
