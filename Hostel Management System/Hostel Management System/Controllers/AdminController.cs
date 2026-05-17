using BLL.DTOs;
using BLL.Services;

using Microsoft.AspNetCore.Mvc;

namespace Hostel_Management_System.Controllers
{
    public class AdminController : Controller
    {
        AdminService adminService;
        RoomService roomService;
        RoomAllocationService allocationService;
        public AdminController(AdminService adminService, RoomService roomService, RoomAllocationService allocationService )
        {
            this.adminService = adminService;
            this.roomService = roomService;
            this.allocationService = allocationService;
        }



        [HttpGet]
        public IActionResult Dashboard(string search)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "1")
            {
                return RedirectToAction("Login", "Account");
            }

            var userList = adminService.SearchUsers(search);
            ViewBag.Search = search;
            return View(userList);
        }

        [HttpGet]
        public IActionResult RoomRequest()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "1")
            {
                return RedirectToAction("Login", "Account");
            }

            var requests =
                allocationService.GetAllPendingRequests();

            return View(requests);
        }

        [HttpGet]
        public IActionResult ApproveRoom(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "1")
            {
                return RedirectToAction("Login", "Account");
            }

            var success = allocationService.ApproveRoom(id);

            if (success)
            {
                TempData["SuccessMsg"] = "Room successfully approved! Other pending requests for this resident have been cleared.";
            }
            else
            {
                TempData["ErrorMsg"] = "Failed to approve room. It may have already been processed.";
            }

            return RedirectToAction("RoomRequest");
        }

        [HttpGet]
        public IActionResult RejectRoom(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "1")
            {
                return RedirectToAction("Login", "Account");
            }

            var success = allocationService.RejectRoom(id);

            if (success)
            {
                TempData["SuccessMsg"] = "Room request rejected.";
            }
            else
            {
                TempData["ErrorMsg"] = "Failed to reject room.";
            }

            return RedirectToAction("RoomRequest");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "1")
            {
                return RedirectToAction("Login", "Account");
            }

            var res = adminService.Delete(id);

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "1")
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Account");
            }

            var user = adminService.GetById(id);

            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserDTO dto)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "1")
            {
                return RedirectToAction("Login", "Account");
            }
            ModelState.Remove("Password");
            ModelState.Remove("PasswordHash");
            ModelState.Remove("Role");


            if (ModelState.IsValid)
            {
                var res = adminService.Update(dto);

                if (res)
                {
                    return RedirectToAction("Dashboard");
                }
            }

            return View(dto);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "1")
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = HttpContext.Session.GetString("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int id = Convert.ToInt32(userId);

            var user = adminService.GetById(id);

            return View(user);
        }
        [HttpGet]
        public IActionResult Room(string search)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "1")
            {
                return RedirectToAction("Login", "Account");
            }

            var rooms = roomService.SearchRooms(search);
            ViewBag.Search = search;
            return View(rooms);
        }




    }
}
