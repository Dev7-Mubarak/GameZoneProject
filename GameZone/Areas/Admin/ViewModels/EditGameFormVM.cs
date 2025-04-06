﻿using GameZone.Attributes;
using GameZone.Settings;

namespace GameZone.Areas.Admin.ViewModels
{
    public class EditGameFormVM : GameFormVM
    {
        public int Id { get; set; }

        public string? GameCover { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtensions)]
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = null!;

    }
}
