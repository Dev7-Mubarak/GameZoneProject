using GameZone.Models;

namespace GameZone.ViewModels.Games
{
    public class GamesInCategoryVM
    {
        public Category Category { get; set; }
        public List<Game> Games { get; set; }
        public int totalGamesCount { get; set; }

        public string SearchTerm { get; set; }  // for searching a game
    }
}
