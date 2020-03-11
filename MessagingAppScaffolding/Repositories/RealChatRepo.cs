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
        public async Task CreateChatRoom(ChatRoom chat)
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
                await this.context.SaveChangesAsync();
            }
        }

        public async Task DeleteChatRoom(int chatRoomID)
        {
            var chats = this.context.ChatRooms;
            foreach (ChatRoom c in chats)
            {
                if (c.ChatRoomID == chatRoomID)
                {
                    this.context.ChatRooms.Remove(c);
                }
            }
            await this.context.SaveChangesAsync();
        }

        // messaging methods
        public async Task AddMsgToChat(int chatID, Message msg)
        {
            // find chat
            var chats = this.context.ChatRooms.ToList();
            var selectedChatRoom = chats.Find(chat => chat.ChatRoomID == chatID);

            // add message and then update chat
            selectedChatRoom.AddMessage(msg);
            this.context.ChatRooms.Update(selectedChatRoom);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveMsgFromChat(int chatID, int msgID)
        {
            Message foundMessage = null;
            // find chat
            var chats = this.context.ChatRooms.ToList();
            var selectedChatRoom = chats.Find(chat => chat.ChatRoomID == chatID);
            
            // find message
            foundMessage = selectedChatRoom.ChatMessages
                .Find(msg => msg.MessageID == msgID);
            
            // remove message and then update chat
            selectedChatRoom.RemoveMessage(foundMessage);
            this.context.ChatRooms.Update(selectedChatRoom);
            await this.context.SaveChangesAsync();

           // return foundMessage;
        }
    }
}
