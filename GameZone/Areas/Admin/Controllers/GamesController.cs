﻿using Microsoft.AspNetCore.Mvc;

namespace GameZone.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GamesController : Controller
    {
        public GamesController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GamesInStation()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
