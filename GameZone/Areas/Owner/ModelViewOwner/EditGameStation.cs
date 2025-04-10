
using GameZone.Helpers;
using GameZone.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Areas.Owner.ModelViewOwner
{
    public class EditGameStation:GameStationVM
    {
        public int Id { get; set; }
        public string? CurrentCover { get; set; }
        [Display(Name = "Image")]
        [Required(ErrorMessage = "Please, Insert an Image!.")]
        
        [AllowedExtensions(FileSettings.AllowedExtensions)]
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
        public string? Image { get; set; }
    }
}
