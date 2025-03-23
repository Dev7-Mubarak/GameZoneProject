using Microsoft.AspNetCore.Mvc;

namespace GameZone.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomsController : Controller
    {
        public RoomsController()
        {
            
        }
        public IActionResult AllRooms()
        {
            return View();
        }

        public IActionResult RoomsInStation()
        {
            return View();
        }

        //public IActionResult AllRoomsCategories()
        //{
        //    return View();
        //}

        //// For Create Category
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// For Edit Category
        //public IActionResult Edit()
        //{
        //    return View();
        //}

        //// For Delete Category
        //public IActionResult Delete()
        //{
        //    return View();
        //}
    }
}
