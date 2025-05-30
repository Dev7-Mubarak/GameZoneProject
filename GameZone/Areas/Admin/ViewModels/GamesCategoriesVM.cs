﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Areas.Admin.ViewModels
{
    public class GamesCategoriesVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter category name!.")]
        [MaxLength(15, ErrorMessage = "Maximum length of characters is 15")]
        [Display(Name = "Category Name")]
        [Remote("CheckIfNameExists", null, AdditionalFields = "Id", ErrorMessage = "Category with this name is already exists!.")]
        public string Name { get; set; } = null!;
    }
}
