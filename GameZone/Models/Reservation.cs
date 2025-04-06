namespace GameZone.Models
{
    public partial class Reservation
    {
        public int Id { get; set; }
        public string? ReservationName { get; set; }
        public string UserId { get; set; } = null!;
        public int RoomId { get; set; }
        public int GameStationId { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime EndHour { get; set; }
        public short NumberOfHours { get; set; }
        public ReservationStatus Satuts { get; set; }
        public float TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public int? PaymentMethodId { get; set; }
        public PaymentType? PaymentType { get; set; }
        public string? DepositImage { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
        public virtual AppUser User { get; set; } = null!;
        public virtual GameStation GameStation { get; set; } = null!;
    }
}
