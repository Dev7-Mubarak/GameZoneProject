﻿using GameZone.Areas.Admin.ViewModels;
using GameZone.Data;
using GameZone.Helpers;
using GameZone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Admin)]
    public class CategoriesController : Controller
    {
        private readonly AppDBContext _context;

        public CategoriesController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult AllGameCategories()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(GamesCategoriesVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var category = new Category() { Name = model.Name };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(AllGameCategories));
        }

        [HttpGet]
        public IActionResult EditCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if (category is null)
                return NotFound();

            var gamecategory = new GamesCategoriesVM { Id = categoryId, Name = category.Name };

            return View(gamecategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(GamesCategoriesVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var category = _context.Categories.Find(model.Id);
            if (category is null)
                return NotFound();

            category.Name = model.Name;

            _context.SaveChanges();

            return RedirectToAction(nameof(AllGameCategories));
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category is null)
                return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Ok();
        }

        public IActionResult CheckIfNameExists(GamesCategoriesVM model)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Name == model.Name);
            var isAllowed = category is null || category.Id.Equals(model.Id);

            return Json(isAllowed);
        }
    }
}