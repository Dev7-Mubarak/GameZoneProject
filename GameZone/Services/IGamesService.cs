using GameZone.Models;
using GameZone.ViewModels.Games;

namespace GameZone.Services
{
    public interface IGamesService
    {
        IEnumerable<PopularGamesVM> GetPopularGames();
    }
}
