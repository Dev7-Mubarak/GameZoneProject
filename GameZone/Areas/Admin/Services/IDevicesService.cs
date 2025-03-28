using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Areas.Admin.Services
{
    public interface IDevicesService
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}
