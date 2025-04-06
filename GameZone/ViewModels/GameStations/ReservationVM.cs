using GameZone.Models;
using System.ComponentModel.DataAnnotations;

namespace GameZone.ViewModels.GameStations
{
    public class ReservationVM
    {
        [Required]
        public string ReservationName { get; set; }
        [Required]
        public int GameStationId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime StartHour { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "minimum 1 hour")]
        public int DurationHours { get; set; }

        [Required]
        public PaymentType PaymentType { get; set; }

        public IFormFile? PaymentReceipt { get; set; }
    }
}
