using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingApp.Models;

namespace MessagingApp.Models
{
    public class ChatRoom
    {
        // private fields
        private List<Message> messages = new List<Message>();

        // public properties
        public int ChatRoomID { get; set; }
        public String ChatName { get; set; }
        public List<Message> ChatMessages { get { return this.messages; } }

        public void AddMessage(Message msg) => messages.Add(msg);

        public void RemoveMessage(Message msg)
        {
            foreach(Message m in messages)
            {
                if(msg.MessageID == m.MessageID)
                {
                    messages.Remove(msg);
                }
            }
        }
    }
}
