using GameZone.Models;

namespace GameZone.ViewModels.GameStations
{
    public class GameStationVM
    {
        public int Id { get; set; }
        public string Descraption { get; set; } = null!;
        public string Location { get; set; } = null!;
        public float? Rating { get; set; }
        public TimeSpan MorningOpenTime { get; set; }
        public TimeSpan MorningCloseTime { get; set; }
        public TimeSpan EveningOpenTime { get; set; }
        public TimeSpan EveningCloseTime { get; set; }
        public string UserId { get; set; } = null!;
        public string? Image { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; } = null!;

        public List<Game> Games = new List<Game>();
        public List<Room> Rooms = new List<Room>();
        public AppUser User { get; set; }
    }
}
