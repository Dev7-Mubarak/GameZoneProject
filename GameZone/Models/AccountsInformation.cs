using System;
using System.Collections.Generic;

namespace GameZone.Models
{
    public partial class AccountsInformation
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public int GameStationId { get; set; }
        public string AccountNumber { get; set; } = null!;

        public virtual GameStation GameStation { get; set; } = null!;
    }
}
