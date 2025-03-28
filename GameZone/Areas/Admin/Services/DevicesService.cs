using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Areas.Admin.Services
{
    public class DevicesService : IDevicesService
    {
        private readonly AppDBContext _context;

        public DevicesService(AppDBContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.Devices
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .OrderBy(d => d.Text)
                    .ToList();
        }
    }
}
