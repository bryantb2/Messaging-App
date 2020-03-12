using MessagingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingApp.Repositories
{
    public class FakeMessageRepo : IMessageRepo
    {
        private List<Message> messages = new List<Message>();

        public List<Message> MessageList { get { return messages; } }

        public async Task AddMsgToRepo(Message msg)
        {
            messages.Add(msg);
        }

        public async Task DeleteMsgFromRepo(int msgId)
        {
            var foundMsg = messages.Find(msg => msg.MessageID == msgId);
            messages.Remove(foundMsg);
        }

        public async Task AddReplytoMsg(Reply rply, int msgId)
        {
            messages.Find(msg => msg.MessageID == msgId)
                .AddToReplyHistory(rply);
        }

        public async Task<Reply> RemoveReplyFromMsg(int replyID, int msgId)
        {
            var foundRply = messages.Find(msg => msg.MessageID == msgId)
                .GetReplyHistory.Find(rply => rply.ReplyID == replyID);
            messages.Find(msg => msg.MessageID == msgId)
                .RemoveReplyHistory(replyID);
            return foundRply;
        }
        
        public async Task UpdateMsg(Message msg)
        {
            messages.Remove(messages.Find(foundMsg => msg.MessageID == foundMsg.MessageID));
            messages.Add(msg);
        }
    }
}
