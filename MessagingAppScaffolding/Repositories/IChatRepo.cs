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

        Task CreateChatRoom(ChatRoom chat);

        Task DeleteChatRoom(int chatRoomID);

        Task AddMsgToChat(int chatID, Message msg);

        Task RemoveMsgFromChat(int chatID, int msgID);
    }
}
