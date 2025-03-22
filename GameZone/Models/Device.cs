using System;
using System.Collections.Generic;

namespace GameZone.Models
{
    public partial class Device
    {
        public Device()
        {
            Games = new HashSet<Game>();
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }
        public string Icon { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
