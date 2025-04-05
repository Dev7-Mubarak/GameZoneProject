using GameZone.Models;

namespace GameZone.ViewModels.Games
{
    public class GameDetailsVM
    {
        public Game Game { get; set; }
        public List<GameStation> AvailableStations { get; set; }
        public List<Device> SupportedDevices { get; set; }
        public bool IsAvailableInStations => AvailableStations.Any();
    }
}
