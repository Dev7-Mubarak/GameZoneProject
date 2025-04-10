using GameZone.Models;

namespace GameZone.Areas.Owner.ModelViewOwner
{
    public class ReservationStatusUpdateModel
    {
        public int Id { get; set; }  // Reservation ID
        public ReservationStatus Status { get; set; }  // New status (e.g., "confirmed" or "denied")
    }
}
