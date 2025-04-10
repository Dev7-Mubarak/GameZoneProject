using System.ComponentModel.DataAnnotations;

namespace GameZone.Areas.Owner.ModelViewOwner
{
    public class AddMoreImagesViewModel
    {
        public int RoomId { get; set; }

        // For additional images upload
        [Display(Name = "Additional Images")]
        public List<IFormFile> Files { get; set; }

        // Optional: Existing additional images to preview
        public List<string> ExistingAdditionalImages { get; set; }
    }
}
