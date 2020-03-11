using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MessagingApp.Models;
using MessagingApp.Repositories;
using Microsoft.AspNetCore.Identity;
using MessagingApp.ViewModels;
using MessagingApp.Data;
using MessagingApp.APIModels;

namespace MessagingApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private ApplicationDbContext context;
        IUserRepo userRepo;
        public AccountController(UserManager<AppUser> usrMgr, 
            ApplicationDbContext c, IUserRepo u)
        {
            userManager = usrMgr;
            context = c;
            userRepo = u;
        }
        public async Task<IActionResult> Index()
        {
            // get user
            var user = await userRepo.GetUserDataAsync(HttpContext.User);

            // set messaging stats and info
            var accountVM = new AccountViewModel();
            accountVM.PostedMsgCount = GetPostedMsgCount(user.GetMessageList);
            accountVM.PostedRplyCount = GetPostedRplyCount(user.GetReplyHistory);
            accountVM.RepliesRecievedCount = GetRepliesToMsgCount(user.GetMessageList);
            accountVM.FirstPostDate = GetPostDate(user.GetMessageList);
            accountVM.RecentPostDate = GetPostDate(user.GetMessageList, "OLDEST");
            accountVM.DateJoined = user.GetDateJoined;
            accountVM.RegisteredEmail = user.Email;
            accountVM.Username = user.UserName;

            ViewBag.BackgroundStyle = "pageContainer5";
            return View(accountVM);
        }

        /*[HttpPut]
        public async Task<IActionResult> EditMessage(int id, [FromBody] MessageEditAPIModel msgModel)
        {
            if (ModelState.IsValid)
            {
                // get user (this ensures a bad user cannot edit another person's msg)
                var user = await userRepo.GetUserDataAsync(HttpContext.User);
                // find msg
                var foundRply = user.GetReplyHistory.Find(rply => rply.ReplyID == id);
                if (foundRply == null)
                {
                    ModelState.AddModelError(nameof(MessageEditAPIModel.MsgBody), "Invalid title or body");
                    return View("ManageMessages", msgModel);
                }
                // update msg
                foundRply.ReplyContent = msgModel.MsgBody;
                replyRepo.UpdateRplyById(foundRply);
                // return msg
                return View(foundRply);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> EditReply(int id, [FromBody] MessageEditAPIModel msgModel)
        {

        }*/

        public IActionResult ManageChats()
        {
            // TODO: MAKE THIS ROLE BASED
            return View();
        }

        public IActionResult ManageAdmins()
        {
            return View();
        }

        public async Task<IActionResult> ManageMessages()
        {
            // get user
            var user = await userRepo.GetUserDataAsync(HttpContext.User);

            // set message and reply info
            var manageMsgVM = new ManageChatsViewModel();
            manageMsgVM.MessageList = user.GetMessageList;
            manageMsgVM.ReplyList = user.GetReplyHistory;

            ViewBag.BackgroundStyle = "pageContainer6";
            return View(manageMsgVM);
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
                    rplyCounter += 1;
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