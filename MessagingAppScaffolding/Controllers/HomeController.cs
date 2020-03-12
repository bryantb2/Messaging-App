using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MessagingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MessagingApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private UserManager<AppUser> userManager;
        public HomeController(
                UserManager<AppUser> usrMgr)
        {
            userManager = usrMgr;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.BackgroundStyle = "pageContainer";
            var user = await userManager.GetUserAsync(HttpContext.User);
            var userExist = true;
            if (user == null)
                userExist = false;
            ViewBag.VisitorStatus = userExist;
            return View(user);
        }

        public IActionResult About()
        {
            ViewBag.BackgroundStyle = "pageContainer4";
            return View();
        }
    }
}
