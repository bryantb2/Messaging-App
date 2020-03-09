using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MessagingApp.Models;
using MessagingApp.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MessagingApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        public AccountController(UserManager<AppUser> usrMgr)
        {
            userManager = usrMgr;
        }
        public async Task<IActionResult> Index()
        {
            // get user
            var user = await userManager.GetUserAsync(HttpContext.User);

            // get messaging stats
            var postedMsgCount = GetPostedMsgCount(user.GetMessageList);
            var postedRplyCount = GetPostedRplyCount(user.GetReplyHistory);
            var repliesRecievedCount = GetRepliesToMsgCount(user.GetMessageList);
            var firstPostDate = GetPostDate(user.GetMessageList);
            var mostRecentPost = GetPostDate(user.GetMessageList, "OLDEST");
            var dateJoined = user.GetDateJoined;

            // 
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

        // message statistical logic
        private int GetPostedMsgCount(List<Message> msgList)
        {
            int msgCounter = 0;
            foreach (Message m in msgList)
                msgCounter += 1;
            return msgCounter;
        }

        private int GetPostedRplyCount(List<Reply> rplyList)
        {
            int rplyCounter = 0;
            foreach (Reply rply in rplyList)
                rplyCounter += 1;
            return rplyCounter;
        }

        private int GetRepliesToMsgCount(List<Message> msgList)
        {
            // will count the overall number of message replies recieved by a user
            int rplyCounter = 0;
            foreach (Message m in msgList)
                foreach (Reply rply in m.GetReplyHistory)
                    rplyCounter += rplyCounter;
            return rplyCounter;
        }

        private DateTime GetPostDate(List<Message> msgList, string operation = "RECENT")
        {
            // will return most recent by default
            Message selectedMsg = msgList[0];
            foreach(Message m in msgList)
            {
                if (operation.ToUpper() == "RECENT")
                {
                    // compare and set recent
                    if (m.UnixTimeStamp > selectedMsg.UnixTimeStamp)
                        selectedMsg = m;
                }
                else
                {
                    // compare and set oldest
                    if (m.UnixTimeStamp < selectedMsg.UnixTimeStamp)
                        selectedMsg = m;
                }
            }
            return selectedMsg.GetTimePosted;
        }
    }
}