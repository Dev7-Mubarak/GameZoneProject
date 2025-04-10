using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameZone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

public class GameViewModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Game Name")]
    public string SelectedGame { get; set; }

    [Required]
    [Display(Name = "Category")]
    public string Category { get; set; }

    [Required]
    [Display(Name = "Description")]
    public string Description { get; set; }

    [Display(Name = "Cover Image Path")]
    public string CoverImagePath { get; set; }

    [Required]
    [Display(Name = "Game Station ID")]
    public int GameStationId { get; set; }

    // Populated from the database for the drop‑down list.
    public IEnumerable<SelectListItem> GameList { get; set; }


}

