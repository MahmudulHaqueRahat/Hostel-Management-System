using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_Management_System.Controllers
{
    public class RoomController : Controller
    {
        RoomService roomService;
        public RoomController(RoomService roomService)
        {
            this.roomService = roomService;
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "1")
            {
                return RedirectToAction("Login", "Account");
            }

            var res = roomService.Delete(id);

            return RedirectToAction("Room", "Admin");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "1")
            {
                return RedirectToAction("Login", "Account");
            }

            var room = roomService.GetById(id);

            return View(room);
        }

        [HttpPost]
        public IActionResult Edit(RoomDTO d)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "1")
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                roomService.Update(d);

                return RedirectToAction("Room", "Admin");
            }

            return View(d);
        }


        [HttpGet]
        public IActionResult Back()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "1")
            {
                return RedirectToAction("Login", "Account");
            }
            return View("Room","Admin");

        }
    }
}
