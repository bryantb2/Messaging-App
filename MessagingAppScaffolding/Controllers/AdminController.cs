using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MessagingApp.ViewModels;
using MessagingApp.Models;
using MessagingApp.Repositories;
using MessagingApp.Data;
using Microsoft.AspNetCore.Identity;

namespace MessagingApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private ApplicationDbContext context;
        IUserRepo userRepo;
        IChatRepo chatRepo;
        IMessageRepo messageRepo;
        public AdminController(UserManager<AppUser> usrMgr,
            ApplicationDbContext c, IUserRepo u, IChatRepo chat, IMessageRepo m)
        {
            userManager = usrMgr;
            context = c;
            userRepo = u;
            chatRepo = chat;
            messageRepo = m;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageChats()
        {
            // add properties to manage chats model
            var chatManageVM = new ManageChatsViewModel();
            chatManageVM.ActiveChats = GenerateChatViewModels(chatRepo.ChatRoomList);

            ViewBag.BackgroundStyle = "pageContainer7";
            return View(chatManageVM);
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
        public IActionResult DeleteChatRoom(int chatRoomID)
        {
            // TODO
            return View(); 
        }

        // view model stat calc methods
        private DateTime GetPostDate(List<Message> msgList, string operation = "RECENT")
        {
            // will return most recent by default
            Message selectedMsg = msgList[0];
            foreach (Message m in msgList)
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

        private List<ChatViewModel> GenerateChatViewModels(List<ChatRoom> chatRoomList)
        {
            // converts chatroom models to chatroom view models
            List<ChatViewModel> chatVmList = new List<ChatViewModel>();
            foreach(ChatRoom chat in chatRoomList)
            {
                var chatVM = new ChatViewModel();
                chatVM.ChatID = chat.ChatRoomID;
                chatVM.Name = chat.ChatName;
                chatVM.DateCreated = chat.GetTimeCreated;
                chatVM.RecentActivity = GetPostDate(chat.ChatMessages, "RECENT");
                chatVM.NumberOfPosts = chat.ChatMessages.Count;

                // add to list
                chatVmList.Add(chatVM);
            }
            return chatVmList;
        }
    }


}