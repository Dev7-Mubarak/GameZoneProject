using GameZone.Models;

namespace GameZone.Areas.Admin.ViewModels
{
    public class GamesInStationVM
    {
        public string StationName { get; set; }
        public List<Game> Games { get; set; }
    }
}
