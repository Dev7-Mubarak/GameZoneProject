using GameZone.Attributes;
using GameZone.Helpers;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Areas.Admin.ViewModels
{
    public class CreateGameStationVM : GameStationVM
    {
        [Display(Name = "Image")]
        [Required(ErrorMessage = "Please, Insert an Image!.")]

        //validate extensions and cover
        [AllowedExtensions(FileSettings.AllowedExtensions)]
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}
