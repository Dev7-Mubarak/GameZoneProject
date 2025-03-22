using System;
using System.Collections.Generic;

namespace GameZone.Models
{
    public partial class Game
    {
        public Game()
        {
            GameStationGames = new HashSet<GameStationGame>();
            Devices = new HashSet<Device>();
        }

        public int Id { get; set; }
        public string Descraption { get; set; } = null!;
        public string Cover { get; set; } = null!;
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Developer { get; set; }
        public string? Release { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<GameStationGame> GameStationGames { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
    }
}
