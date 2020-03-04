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

        void AddMsgToChat(int chatID, Message msg);

        Message RemoveMsgFromChat(int chatID, int msgID);
    }
}
