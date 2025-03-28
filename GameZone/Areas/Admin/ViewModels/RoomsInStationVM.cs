using GameZone.Models;

namespace GameZone.Areas.Admin.ViewModels
{
    public class RoomsInStationVM
    {
        public string StationName { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
