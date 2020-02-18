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

namespace MessagingApp.Controllers
{
    public class MessagesController : Controller
    {
        IReplyRepo replyRepo;
        IMessageRepo messageRepo;
        IChatRepo chatRepo;
        public MessagesController(IReplyRepo r, IMessageRepo m, IChatRepo c)
        {
            this.replyRepo = r;
            this.messageRepo = m;
            this.chatRepo = c;
        }

        public ViewResult Forum(int? chatRoomID = null)
        {
            // allows for user to select which chat room they want
            ViewBag.BackgroundStyle = "parallaxEffect";
            List<ChatRoom> chatRooms = chatRepo.ChatRoomList;
            ChatRoom selectChatRoom;
            if (chatRoomID != null)
                selectChatRoom = chatRooms.Find(chat => chat.ChatRoomID == chatRoomID);
            else
                selectChatRoom = chatRooms.Count == 0 ? null : chatRooms[0];
            return View(selectChatRoom);
        }

        // post message


        // post reply
    }
}
