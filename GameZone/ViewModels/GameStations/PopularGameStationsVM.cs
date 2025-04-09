namespace GameZone.ViewModels.GameStations
{
    public class PopularGameStationsVM
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public float? Rating { get; set; }
        public string? Image { get; set; }

    }
}
