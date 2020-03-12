using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MessagingApp.Data;
using MessagingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MessagingApp.Repositories
{
    public class RealMessageRepo : IMessageRepo
    {
        private ApplicationDbContext context;

        public RealMessageRepo(ApplicationDbContext c) => this.context = c;

        public List<Message> MessageList 
        {
            get
            {
                var messages = this.context.Messages
                    .Include(msg => msg.GetReplyHistory)
                    .ToList();
                return messages;
            }
        }

        public async Task AddMsgToRepo(Message msg)
        {
            this.context.Messages.Add(msg);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateMsg(Message msg)
        {
            this.context.Messages.Update(msg);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteMsgFromRepo(int msgId)
        {
            var messages = this.context.Messages;
            foreach (Message m in messages)
            {
                if (m.MessageID == msgId)
                {
                    this.context.Messages.Remove(m);
                }
            }
            await this.context.SaveChangesAsync();
        }

        // reply methods
        public async Task AddReplytoMsg(Reply rply, int msgID)
        {
            var message = this.context.Messages.ToList()
                .Find(msg => msg.MessageID == msgID);
            message.AddToReplyHistory(rply);
            this.context.Messages.Update(message);
            await this.context.SaveChangesAsync();
        }

        public async Task<Reply> RemoveReplyFromMsg(int replyID, int msgID)
        {
            // find message
            var selectedMsg = this.context.Messages.ToList()
                .Find(msg => msg.MessageID == msgID);

            // find reply
            var foundReply = selectedMsg.GetReplyHistory
                .Find(rply => rply.ReplyID == replyID);

            // remove message and update context
            selectedMsg.RemoveReplyHistory(replyID);
            this.context.Messages.Update(selectedMsg);
            await this.context.SaveChangesAsync();

            return foundReply;
        }
    }
}
