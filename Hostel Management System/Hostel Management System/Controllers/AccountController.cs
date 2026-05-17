using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_Management_System.Controllers
{
    public class AccountController : Controller
    {
        UserService userService;
        public AccountController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserDTO U)
        {

            if (userService.IsNameExist(U.FullName))
            {
                ModelState.AddModelError("FullName", "Name already exists. Please choose a different name.");
            }
            if (userService.IsEmailExist(U.Email))
            {
                ModelState.AddModelError("Email", "Mail already used. Please try another.");
            }
            if (ModelState.IsValid)
            {
              
                var res= userService.Create(U);
                if(res==true)
                {
                    return View("Login");
                }

            }
            return View("Register", U);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginDTO data)
        {
            if (ModelState.IsValid)
            {
                var user = userService.Login(data);

                if (user != null)
                {
                    HttpContext.Session.SetString("UserId", user.UserId.ToString());

                    HttpContext.Session.SetString("UserName", user.FullName);

                    HttpContext.Session.SetString("UserEmail", user.Email);

                    HttpContext.Session.SetString("UserRole", user.Role);

                    //Redirect based on role
                    if (user.Role == "1")
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }

                    else if (user.Role == "2")
                    {
                        return RedirectToAction("Dashboard", "Resident");
                    }
                }

                ModelState.AddModelError("",
                    "Invalid email or password");
            }

            return View(data);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Back()
        {
 
            return View("Login");

        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
