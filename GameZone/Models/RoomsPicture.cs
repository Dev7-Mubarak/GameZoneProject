using System;
using System.Collections.Generic;

namespace GameZone.Models
{
    public partial class RoomsPicture
    {
        public int Id { get; set; }
        public string Image { get; set; } = null!;
        public int RoomId { get; set; }

        public virtual Room Room { get; set; } = null!;
    }
}
