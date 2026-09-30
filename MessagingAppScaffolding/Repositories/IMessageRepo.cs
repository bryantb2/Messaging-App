using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingApp.Models;

namespace MessagingApp.Repositories
{
    public interface IMessageRepo
    {
        List<Message> MessageList { get; }

        Task AddMsgToRepo(Message msg);

        Task DeleteMsgFromRepo(int msgId);

        Task AddReplytoMsg(Reply rply, int msgId);

        Task<Reply> RemoveReplyFromMsg(int replyID, int msgId);

        Task UpdateMsg(Message msg);
    }
}
