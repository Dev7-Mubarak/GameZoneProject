namespace GameZone.Models
{
    public partial class Room
    {
        public Room()
        {
            Reservations = new HashSet<Reservation>();
            RoomsPictures = new HashSet<RoomsPicture>();
            Devices = new HashSet<Device>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public short NumberOfAllowedPeople { get; set; }
        public int GameStationId { get; set; }
        public string? PrimaryImage { get; set; }
        public byte Unit { get; set; }

        public virtual GameStation GameStation { get; set; } = null!;
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<RoomsPicture> RoomsPictures { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
    }
}
