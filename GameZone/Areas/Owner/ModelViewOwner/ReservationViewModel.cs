namespace GameZone.Areas.Owner.ModelViewOwner
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartHour { get; set; }
        public int NumberOfHours { get; set; }
        public float TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
    }
}
