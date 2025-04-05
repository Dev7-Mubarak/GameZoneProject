using GameZone.Constants;
using GameZone.Custom_Validations;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Areas.Admin.ViewModels
{
    public class EditGameStationVM : GameStationVM
    {
        public int Id { get; set; }


        public string? CurrentCover { get; set; }


        [Display(Name = "Image")]
        //validate extensions and cover
        [AllowedExtensions(CoverSettings.allowedExtensions)]
        [MaxFileSize(CoverSettings.maxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
