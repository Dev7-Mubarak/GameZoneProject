using System.ComponentModel.DataAnnotations;

namespace GameZone.ViewModels
{
    public class RatingVM
    {
        [Required]
        public int GameStationId { get; set; }

        [Required(ErrorMessage = "Please select a rating")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int UserRating { get; set; }

        [Required(ErrorMessage = "Please enter a comment")]
        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters")]
        public string UserComment { get; set; }
    }
}
