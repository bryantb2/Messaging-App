using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingApp.Models;

namespace MessagingApp.Repositories
{
    public interface IChatRepo
    {
        List<ChatRoom> ChatRoomList { get; }

        void CreateChatRoom(ChatRoom chat);

        ChatRoom DeleteChatRoom(int chatRoomID);

        void AddMsgToChat(ChatRoom chat, Message msg);

        Message RemoveMsgFromChat(ChatRoom chat, Message msg);
    }
}
