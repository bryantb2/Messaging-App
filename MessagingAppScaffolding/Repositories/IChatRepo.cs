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

        void CreateChatRoom(String chatName);

        ChatRoom DeleteChatRoom(int chatRoomID);
    }
}
