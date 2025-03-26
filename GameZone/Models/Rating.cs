namespace GameZone.Models
{
    public partial class Rating
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int GameStationId { get; set; }
        public double UserRating { get; set; }
        public string? UserComment { get; set; }
        public DateTime RatingDate { get; set; }

        public virtual GameStation GameStation { get; set; } = null!;
        public virtual AppUser User { get; set; } = null!;
    }
}
