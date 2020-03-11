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

        void AddReplyToRepo(Reply reply);

        void DeleteRepFromRepo(int replyId);

        void UpdateRplyById(Reply rply);
    }
}
