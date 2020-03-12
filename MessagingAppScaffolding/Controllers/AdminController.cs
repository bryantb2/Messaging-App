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
    [Authorize(Roles = "admin, owner")]
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

        [Authorize(Roles = "owner")]
        public async Task<IActionResult> Index()
        {
            // this route will take users to manage admins page
            var appUserList = userRepo.GetAllUsersAndData();
            var adminList = await GenerateUserRoleList(appUserList, "admin");
            var peasantList = await GenerateUserRoleList(appUserList, "standard");

            // build view model
            var manageAdminVM = new ManageAdminsViewModel()
            {
                AdminList = GenerateAdminViewModels(adminList),
                UserList = GenerateAdminViewModels(peasantList)
            };

            ViewBag.BackgroundStyle = "pageContainer8";
            return View(manageAdminVM);
        }

        public async Task<IActionResult> AddAdmin(int userId)
        {
            // TODO return redirect to index after adding admin
            throw new NotImplementedException();
        }

        public async Task<IActionResult> RemoveAdmin(int userId)
        {
            // TODO return redirect to index after removing admin
            throw new NotImplementedException();
        }

        public async Task<IActionResult> ManageChats(ManageChatsViewModel chatModel = null)
        {
            // add properties to manage chats model
            var chatManageVM = new ManageChatsViewModel();
            chatManageVM.ActiveChats = GenerateChatViewModels(chatRepo.ChatRoomList);

            ViewBag.BackgroundStyle = "pageContainer7";
            return View(chatManageVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddChatRoom(ManageChatsViewModel chatModel)
        {
            // TODO
            if(ModelState.IsValid)
            {
                // create chat room
                var newChat = new ChatRoom()
                {
                    ChatName = chatModel.Chat.ChatName,
                    UnixTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                };
                // add to repo
                await chatRepo.CreateChatRoom(newChat);
            }
            else
            {
                ModelState.AddModelError(nameof(ManageChatsViewModel.Chat.ChatName), "Invalid title");
                return RedirectToAction("ManageChats", new
                {
                    chatModel = chatModel
                });
            }
            return RedirectToAction("ManageChats");
        }

        [HttpGet] // jank way of ensuring compat. with anchor tag calls
        public async Task<IActionResult> DeleteChatRoom(int chatRoomID)
        {
            var foundChat = chatRepo.ChatRoomList.Find(chat => chat.ChatRoomID == chatRoomID);
            if(foundChat == null)
                return RedirectToAction("ManageChats");
            await chatRepo.DeleteChatRoom(chatRoomID);
            return RedirectToAction("ManageChats");
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
                chatVM.RecentActivity = 
                    chat.ChatMessages.Count == 0 ?
                    chat.GetTimeCreated : GetPostDate(chat.ChatMessages, "RECENT");
                chatVM.NumberOfPosts = chat.ChatMessages.Count;

                // add to list
                chatVmList.Add(chatVM);
            }
            return chatVmList;
        }

        private List<AdminViewModel> GenerateAdminViewModels(List<AppUser> userList)
        {
            // list method is responsible for parsing user data into view models for the manage admins page
            List<AdminViewModel> adminVMList = new List<AdminViewModel>();
            for (var i = 0; i < userList.Count; i++)
            {
                var currentUser = userList[i];
                var adminVM = new AdminViewModel()
                {
                    UserID = currentUser.Id,
                    Username = currentUser.UserName,
                    DateJoined = currentUser.GetDateJoined,
                    RecentActivity = GetPostActivityDate(currentUser.GetMessageList, "RECENT"),
                    NumberOfPosts = currentUser.GetMessageList.Count
                };
                adminVMList.Add(adminVM);
            }
            return adminVMList;
        }

        private async Task<List<AppUser>> GenerateUserRoleList(List<AppUser> userList, string role)
        {
            List<AppUser> hasRoleList = new List<AppUser>();
            for(var i = 0; i < userList.Count; i++)
            {
                var currentUser = userList[i];
                if(await userManager.IsInRoleAsync(currentUser, role))
                {
                    hasRoleList.Add(currentUser);
                }
            }
            return hasRoleList;
        }

        private DateTime GetPostActivityDate(List<Message> msgList, string operation = "RECENT")
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
    }
}