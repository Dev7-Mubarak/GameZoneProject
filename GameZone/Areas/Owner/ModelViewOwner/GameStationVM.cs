using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Areas.Owner.ModelViewOwner
{
    public class GameStationVM
    {
        [Required(ErrorMessage = "Station Name is required!")]
        [MaxLength(60, ErrorMessage = "Maximum length is 60 characters.")]
        public string Name { get; set; } = null!;


    


        [Required(ErrorMessage = "Phone Number(1) is required!")]
        [MaxLength(15, ErrorMessage = "Maximum length is 15 characters.")]
        [Display(Name = "Phone Number(1)")]
        public string PhoneNumber1 { get; set; } = null!;


        [Required(ErrorMessage = "Phone Number(2) is required!")]
        [MaxLength(15, ErrorMessage = "Maximum length is 15 characters.")]
        [Display(Name = "Phone Number(2)")]
        public string PhoneNumber2 { get; set; } = null!;


        [Required(ErrorMessage = "Please, Enter A Location")]
        [Display(Name = "Location")]
        public string Location { get; set; }



        [Required(ErrorMessage = "Description is required!")]
        [Display(Name = "Description")]
        public string Description { get; set; } = null!;


       
    }
}

