using GameZone.Helpers;
using GameZone.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Areas.Admin.ViewModels
{
    public class EditGameStationVM : GameStationVM
    {
        public int Id { get; set; }


        public string? CurrentCover { get; set; }


        [Display(Name = "Image")]
        [AllowedExtensions(FileSettings.AllowedExtensions)]
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
