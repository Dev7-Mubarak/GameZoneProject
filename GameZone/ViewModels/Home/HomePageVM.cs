using GameZone.ViewModels.Games;
using GameZone.ViewModels.GameStations;

namespace GameZone.ViewModels.Home
{
    public class HomePageVM
    {
        public IEnumerable<PopularGamesVM> PopularGames;
        public IEnumerable<PopularGameStationsVM> PopularGameStations;
    }
}
