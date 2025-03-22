using Microsoft.AspNetCore.Identity;

namespace GameZone.Models
{
    public class AppUser : IdentityUser
    {
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
