using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingApp.Models;

namespace MessagingApp.Repositories
{
    public interface IReplyRepo
    {
        List<Reply> ReplyList { get; }

        Task AddReplyToRepo(Reply reply);

        Task DeleteRepFromRepo(int replyId);

        Task UpdateRplyById(Reply rply);
    }
}
