using GameZone.Models;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Areas.Owner.ModelViewOwner
{
    public class RoomViewModel
    {

        public int Id { get; set; }

        [Required]
        public string RoomName { get; set; }

        [Required]
        public short NumberOfAllowedPeople { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public byte Unit { get; set; }

        public int GameStationId { get; set; }


        public IFormFile? PrimaryImageFile { get; set; }

    
        public string? PrimaryImage { get; set; }

        // List of uploaded additional images
        public List<IFormFile>? AdditionalImages { get; set; }

        // Display existing additional images (already stored in the system)
        public List<string>? ExistingAdditionalImages { get; set; } = new List<string>();

    }


}
    


