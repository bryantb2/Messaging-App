using MessagingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingApp.Repositories
{
    public class FakeChatRepo : IChatRepo
    {
        private List<ChatRoom> chats = new List<ChatRoom>();

        public List<ChatRoom> ChatRoomList { get { return chats; } }

        public async Task CreateChatRoom(ChatRoom chat)
        {
            chats.Add(chat);
        }

        public async Task DeleteChatRoom(int chatRoomID)
        {
            var foundChat = chats.Find(chat => chat.ChatRoomID == chatRoomID);
            chats.Remove(foundChat);
        }

        public async Task AddMsgToChat(int chatRoomID, Message msg)
        {
            chats.Find(chat => chat.ChatRoomID == chatRoomID).AddMessage(msg);
        }

        public async Task RemoveMsgFromChat(int chatRoomID, int msgID)
        {
            var chatMessages = chats.Find(chat => chat.ChatRoomID == chatRoomID).ChatMessages;
            chatMessages.Remove(chatMessages.Find(msg => msg.MessageID == msgID));
        }
    }
}
