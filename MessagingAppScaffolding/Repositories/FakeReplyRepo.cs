using MessagingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingApp.Repositories
{
    public class FakeReplyRepo : IReplyRepo
    {
        private List<Reply> replies = new List<Reply>();

        public List<Reply> ReplyList { get { return replies; } }

        public async Task AddReplyToRepo(Reply reply)
        {
            replies.Add(reply);
        }

        public async Task DeleteRepFromRepo(int replyId)
        {
            var foundReply = replies.Find(rply => rply.ReplyID == replyId);
            replies.Remove(foundReply);
        }

        public async Task UpdateRplyById(Reply rplyArg)
        {
            var foundReply = replies.Find(rply => rply.ReplyID == rplyArg.ReplyID);
            replies.Remove(foundReply);
            replies.Add(rplyArg);
        }
    }
}
