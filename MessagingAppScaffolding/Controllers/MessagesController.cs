using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MessagingApp.Models;
using MessagingApp.Data;
using MessagingApp.Repositories;
using MessagingApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MessagingApp.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        IReplyRepo replyRepo;
        IMessageRepo messageRepo;
        IChatRepo chatRepo;

        private UserManager<AppUser> userManager;

        public MessagesController(UserManager<AppUser> usrMgr,
                IReplyRepo r, IMessageRepo m, IChatRepo c)
        {
            // repos
            this.replyRepo = r;
            this.messageRepo = m;
            this.chatRepo = c;
            // user managers
            this.userManager = usrMgr;
        }

        [HttpGet]
        public IActionResult Forum(int? chatRoomID = null)
        {
            // allows for user to select which chat room they want
            ViewBag.BackgroundStyle = "parallaxEffect";
            List<ChatRoom> chatRooms = chatRepo.ChatRoomList;
            ChatRoom selectChatRoom;
            if (chatRoomID != null)
                selectChatRoom = chatRooms.Find(chat => chat.ChatRoomID == chatRoomID);
            else
                selectChatRoom = chatRooms.Count == 0 ? null : chatRooms[0];
            ViewBag.ChatRoomList = chatRooms; // for dropdown
            return View(selectChatRoom);
        }

        // post message
        [HttpPost]
        public async Task<IActionResult> AddMessage(CreateMessageViewModel msg, int chatRoomID)
        {
            if(ModelState.IsValid)
            {
                var selectedChatRoom = chatRepo.ChatRoomList
                    .Find(chat => chat.ChatRoomID == chatRoomID);
                var user = await userManager.GetUserAsync(HttpContext.User);
                var newMsg = new Message()
                {
                    Topic = selectedChatRoom.ChatName,
                    MessageTitle = msg.Title,
                    MessageContent = msg.MessageBody,
                    UnixTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Poster = user.UserName
                };
                // add to msg, chat, and user repos
                messageRepo.AddMsgToRepo(newMsg);
                user.AddMessageToHistory(newMsg);
                chatRepo.AddMsgToChat(chatRoomID, newMsg);
                var result = await userManager.UpdateAsync(user);
            }
            else
                ModelState.AddModelError(nameof(CreateMessageViewModel.Title), "Invalid title or body");
            return Redirect("/Message/Forum/" + chatRoomID.ToString());
        }

        // post reply
        [HttpPost]
        public async Task<IActionResult> AddReply(CreateReplyViewModel rply, int msgId, int chatRoomID)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                var newRply = new Reply()
                {
                    ReplyContent = rply.MessageBody,
                    UnixTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Poster = user.UserName
                };
                // add to reply, msg, and user repos
                replyRepo.AddReplyToRepo(newRply);
                messageRepo.AddReplytoMsg(newRply, msgId);
                user.AddToReplyHistory(newRply);
                var result = await userManager.UpdateAsync(user);
            }
            else
                ModelState.AddModelError(nameof(CreateReplyViewModel.MessageBody), "Invalid reply body");
            return Redirect("/Message/Forum/" + chatRoomID.ToString());
        }
    }
}
