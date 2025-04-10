using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Areas.Owner.ModelViewOwner
{
    public class GetAllOwnersGames
    {
        public int Id { get; set; }
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Cover Image Path")]
        public string CoverImagePath { get; set; }
        [Display(Name = "Game Name")]
        public string Name { get; set; }
    }
}
