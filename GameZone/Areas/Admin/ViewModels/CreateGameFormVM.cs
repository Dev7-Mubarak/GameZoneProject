using GameZone.Helpers;
using GameZone.Helpers.Attributes;

namespace GameZone.Areas.Admin.ViewModels
{
    public class CreateGameFormVM : GameFormVM
    {

        [AllowedExtensions(FileSettings.AllowedExtensions)]
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;

    }
}
