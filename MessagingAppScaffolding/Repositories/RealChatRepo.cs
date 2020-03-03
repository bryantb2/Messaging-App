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

        // chatroom methods
        public void CreateChatRoom(ChatRoom chat)
        {
            var chats = this.context.ChatRooms;
            var alreadyExists = false;
            foreach(ChatRoom c in chats)
            {
                if (c.ChatName.ToLower() == chat.ChatName.ToLower())
                    alreadyExists = true;
            }
            if(!alreadyExists)
            {
                this.context.ChatRooms.Add(chat);
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

        // messaging methods
        public void AddMsgToChat(ChatRoom chat, Message msg)
        {
            // find chat and add message to it
            this.context.ChatRooms.Find(chat).AddMessage(msg);
            this.context.SaveChanges();
        }

        public Message RemoveMsgFromChat(ChatRoom chat, Message msg)
        {

        }
    }
}
