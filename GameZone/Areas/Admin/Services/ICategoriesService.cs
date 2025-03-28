using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Areas.Admin.Services
{
    public interface ICategoriesService
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}
