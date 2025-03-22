using System;
using System.Collections.Generic;

namespace GameZone.Models
{
    public partial class GameStationGame
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int GameStationId { get; set; }

        public virtual Game Game { get; set; } = null!;
        public virtual GameStation GameStation { get; set; } = null!;
    }
}
