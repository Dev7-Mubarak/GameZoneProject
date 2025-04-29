using System.ComponentModel.DataAnnotations;

namespace GameZone.ViewModels
{
    public class EditCurentUserVM
    {
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
