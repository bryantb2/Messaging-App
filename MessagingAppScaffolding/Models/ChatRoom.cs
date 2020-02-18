using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingApp.Models;

namespace MessagingApp.Models
{
    public class ChatRoom
    {
        public int ChatRoomID { get; set; }
        public String ChatName { get; set; }
        public List<Message> ChatMessages { get; set; }

        public void AddMessage(Message msg) => ChatMessages.Add(msg);

        public void RemoveMessage(Message msg)
        {
            foreach(Message m in ChatMessages)
            {
                if(msg.MessageID == m.MessageID)
                {
                    ChatMessages.Remove(msg);
                }
            }
        }
    }
}
