using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MessagingApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace MessagingApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            ViewBag.BackgroundStyle = "pageContainer";
            return View();
        }

        public ViewResult Contact()
        {
            ViewBag.BackgroundStyle = "pageContainer3";
            return View();
        }

        public ViewResult About()
        {
            ViewBag.BackgroundStyle = "pageContainer4";
            return View();
        }
    }
}
