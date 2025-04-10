using GameZone.Models;

namespace GameZone.ViewModels.Games
{
    public class PopularGamesVM
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string CategoryName { get; set; }

        public string Cover { get; set; }

    }
}
