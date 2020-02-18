using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingApp.Models;
using MessagingApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MessagingApp.Repositories
{
    public class RealChatRepo : IChatRepo
    {
        private ApplicationDbContext context;

        // CONSTRUCTOR
        public RealChatRepo(ApplicationDbContext c) => this.context = c;

        public List<ChatRoom> ChatRoomList 
        { 
            get 
            {
                var chatRooms = this.context.ChatRooms
                    .Include(chat => chat.ChatMessages)
                        .ThenInclude(msg => msg.GetReplyHistory)
                    .ToList();
                return chatRooms;
            }
        }

        public void CreateChatRoom(String chatName)
        {
            var chats = this.context.ChatRooms;
            var alreadyExists = false;
            foreach(ChatRoom c in chats)
            {
                if (c.ChatName.ToLower() == chatName.ToLower())
                    alreadyExists = true;
            }
            if(!alreadyExists)
            {
                ChatRoom chat = new ChatRoom()
                {
                    ChatName = chatName,
                };
                var addedChat = this.context.ChatRooms.Add(chat);
                this.context.SaveChanges();
            }
        }

        public ChatRoom DeleteChatRoom(int chatRoomID)
        {
            var chats = this.context.ChatRooms;
            ChatRoom removedChat = null;
            foreach (ChatRoom c in chats)
            {
                if (c.ChatRoomID == chatRoomID)
                {
                    removedChat = c;
                    this.context.ChatRooms.Remove(c);
                    this.context.SaveChanges();
                    return removedChat;
                }
            }
            return removedChat;
        }
    }
}
