using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MessagingApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.BackgroundStyle = "pageContainer5";
            return View();
        }

        public IActionResult ManageChats()
        {
            return View();
        }

        public IActionResult ManageMessages()
        {
            ViewBag.BackgroundStyle = "pageContainer6";
            return View();
        }

        public IActionResult Stats()
        {
            return View();
        }
    }
}