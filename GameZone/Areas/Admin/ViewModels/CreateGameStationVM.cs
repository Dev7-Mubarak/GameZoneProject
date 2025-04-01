using GameZone.Constants;
using GameZone.Custom_Validations;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Areas.Admin.ViewModels
{
    public class CreateGameStationVM : GameStationVM
    {
        [Display(Name = "Image")]
        [Required(ErrorMessage = "Please, Insert an Image!.")]
        //validate extensions and cover
        [AllowedExtensions(CoverSettings.allowedExtensions)]
        [MaxFileSize(CoverSettings.maxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}
