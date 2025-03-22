using Microsoft.AspNetCore.Mvc;

namespace GameZone.Areas.Owner.Controllers
{
    [Area("Owner")]
    public class OwnersStationsController : Controller
    {
        public OwnersStationsController()
        {
            
        }

        public IActionResult OwnerStation()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult ReservationLog()
        {
            return View();
        }

        public IActionResult AcceptedReservation()
        {
            return View();
        }

        public IActionResult RejectedReservation()
        {
            return View();
        }
    }
}
