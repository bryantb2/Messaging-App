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

        Task AddReplytoMsg(Reply rply, int msgID);

        Task<Reply> RemoveReplyFromMsg(int replyID, int msgID);

        Task UpdateMsgById(Message msg);
    }
}
