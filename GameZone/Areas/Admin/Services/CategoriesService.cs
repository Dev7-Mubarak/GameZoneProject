using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Areas.Admin.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly AppDBContext _context;

        public CategoriesService(AppDBContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.Categories
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .OrderBy(c => c.Text)
                    .ToList();
        }
    }
}
