namespace GameZone.Models
{
    public partial class GameStation
    {
        public GameStation()
        {
            AccountsInformations = new HashSet<AccountsInformation>();
            GameStationGames = new HashSet<GameStationGame>();
            Ratings = new HashSet<Rating>();
            Rooms = new HashSet<Room>();
        }

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

        public virtual AppUser User { get; set; } = null!;
        public virtual ICollection<AccountsInformation> AccountsInformations { get; set; }
        public virtual ICollection<GameStationGame> GameStationGames { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}
