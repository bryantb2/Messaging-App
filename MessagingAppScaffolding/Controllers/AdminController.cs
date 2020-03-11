using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MessagingApp.ViewModels;

namespace MessagingApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageChats()
        {
            ViewBag.BackgroundStyle = "pageContainer7";
            return View();
        }

        [HttpPost]
        public IActionResult AddChatRoom(ManageChatsViewModel chatModel)
        {
            // TODO
            if(ModelState.IsValid)
            {

            }
            return View();
        }

        [HttpDelete]
        public IActionResult DeleteChatRoom(int chatId)
        {
            // TODO
            return View(); 
        }

        // view model stat calc methods
    }
}