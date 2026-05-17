using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_Management_System.Controllers
{
    public class ResidentController : Controller
    {
        AdminService adminService;
        ResidentService residentService;
        RoomService roomService;
        BillService billService;
        RoomAllocationService allocationService;
        NotificationService notificationService;
        public ResidentController(ResidentService residentService, RoomService roomService, RoomAllocationService allocationService, AdminService adminService, BillService billService, NotificationService notificationService )
        {
            this.residentService = residentService;
            this.roomService = roomService;
            this.allocationService = allocationService;
            this.adminService = adminService;
            this.billService = billService;
            this.notificationService = notificationService;
        }


        [HttpGet]
        public IActionResult Dashboard()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "2")
            {
                return RedirectToAction("Login", "Account");
            }

            int userId =
                Convert.ToInt32(
                    HttpContext.Session.GetString("UserId"));
            

            ViewBag.UnreadCount =
                notificationService.GetUnreadCount(userId);

            var resident =
                residentService.GetByUserId(userId);

            if (resident == null)
            {
                return RedirectToAction("Profile");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "2")
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Profile(ResidentDTO dto)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "2")
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                dto.UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

                residentService.Create(dto);

                return RedirectToAction("Dashboard");
            }

            return View(dto);
        }
        //For Room Booking

        [HttpGet]
        public IActionResult BookRoom()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "2")
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            var resident = residentService.GetByUserId(userId);

            bool hasApprovedRoom = allocationService.HasApprovedRoom(resident.ResidentId);

            if (hasApprovedRoom)
            {
                var approvedRoom =
                    allocationService.GetApprovedRoom(
                        resident.ResidentId);

                ViewBag.ApprovedRoom = approvedRoom;

                return View(new List<RoomDTO>());
            }

            var pendingRoomIds = allocationService.GetPendingRoomIds(resident.ResidentId);

            var rooms = roomService.Get()
               .Where(r =>
                   r.Status == "Available" &&
                   !pendingRoomIds.Contains(r.RoomId))
               .ToList();

            return View(rooms);
        }

        [HttpGet]
        public IActionResult Book(int roomId)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            var resident = residentService.GetByUserId(userId);
            allocationService.BookRoom(resident.ResidentId, roomId);

            return RedirectToAction("BookRoom");
        }



        [HttpGet]
        public IActionResult CancelBooking(int roomId)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            var resident = residentService.GetByUserId(userId);

            allocationService.CancelBooking(resident.ResidentId, roomId);

            return RedirectToAction("PendingRooms");
        }

        [HttpGet]
        public IActionResult PendingRooms()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "2")
            {
                return RedirectToAction("Login", "Account");
            }

            int userId =
                Convert.ToInt32(
                    HttpContext.Session.GetString("UserId"));

            var resident =
                residentService.GetByUserId(userId);

            var pendingRooms =
                allocationService.GetPendingRooms(
                    resident.ResidentId);

            return View(pendingRooms);
        }

        [HttpGet]
        public IActionResult ProfileDetails()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "2")
            {
                return RedirectToAction("Login", "Account");
            }

            int userId =
                Convert.ToInt32(
                    HttpContext.Session.GetString("UserId"));

            var user = adminService.GetById(userId);

            return View(user);
        }
        [HttpGet]
        public ActionResult Bills()
        {
            int userId = Convert.ToInt32(
            HttpContext.Session.GetString("UserId"));
            var resident = residentService
            .GetByUserId(userId);
            if (resident == null)
            {
                return RedirectToAction("Dashboard");
            }
            var bills = billService
            .GetByResident(resident.ResidentId);
            return View(bills);

        }

        [HttpGet]
        public IActionResult Notifications()
        {
            int userId = Convert.ToInt32(
                HttpContext.Session.GetString("UserId"));

            notificationService.MarkAllAsRead(userId);

            var notifications =
                notificationService.GetByUser(userId);

            return View(notifications);
        }
    }
   }
