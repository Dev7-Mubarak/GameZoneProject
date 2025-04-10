using GameZone.Models;

namespace GameZone.Areas.Owner.ModelViewOwner
{
    public class StationDashboardViewModel
    {
        public GameStation Station { get; set; }
        public int PendingCount { get; set; }
        public int CompletedCount { get; set; }
        public int DeniedCount { get; set; }
    }
}
