using GameZone.Models;
using GameZone.ViewModels.GameStations;

namespace GameZone.Services
{
    public interface IGameStationsService
    {
        public IEnumerable<PopularGameStationsVM> GetPopularGameStations();
    }
}
