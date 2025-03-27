using GameZone.Constants;
using GameZone.Custom_Validations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Areas.Admin.ViewModels
{
    public class GameStationVM
    {
        
        [Required(ErrorMessage = "Station Name is required!")]
        [MaxLength(60, ErrorMessage = "Maximum length is 60 characters.")]
        public string Name { get; set; } = null!;


        [Required(ErrorMessage = "Station Address is required!")]
        [MaxLength(100, ErrorMessage = "Maximum length is 100 characters.")]
        [Display(Name = "Full Address")]
        public string Location { get; set; } = null!;


        [Required(ErrorMessage = "Phone Number(1) is required!")]
        [MaxLength(15, ErrorMessage = "Maximum length is 15 characters.")]
        [Display(Name = "Phone Number(1)")]
        public string PhoneNumber1 { get; set; } = null!;


        [Required(ErrorMessage = "Phone Number(2) is required!")]
        [MaxLength(15, ErrorMessage = "Maximum length is 15 characters.")]
        [Display(Name = "Phone Number(2)")]
        public string PhoneNumber2 { get; set; } = null!;


        [Required(ErrorMessage = "Please, Enter a time!.")]
        [Display(Name = "Morning Open Time")]
        public TimeSpan MorningOpenTime { get; set; }


        [Required(ErrorMessage = "Please, Enter a time!.")]
        [Display(Name = "Morning Close Time")]
        public TimeSpan MorningCloseTime { get; set; }


        [Required(ErrorMessage = "Please, Enter a time!.")]
        [Display(Name = "Evening Open Time")]
        public TimeSpan EveningOpenTime { get; set; }


        [Required(ErrorMessage = "Please, Enter a time!.")]
        [Display(Name = "Evening Close Time")]
        public TimeSpan EveningCloseTime { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        [Display(Name = "Description")]
        public string Description { get; set; } = null!;


        [Required(ErrorMessage = "Owner is required!")]
        [Display(Name = "Owner")]
        public string UserId { get; set; } = null!;

        public IEnumerable<SelectListItem> Users { get; set; } = new HashSet<SelectListItem>();
    }
}
