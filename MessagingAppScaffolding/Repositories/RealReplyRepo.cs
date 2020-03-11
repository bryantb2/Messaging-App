using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingApp.Data;
using MessagingApp.Models;

namespace MessagingApp.Repositories
{
    public class RealReplyRepo : IReplyRepo
    {
        private ApplicationDbContext context;
        public RealReplyRepo(ApplicationDbContext c) => this.context = c;

        public List<Reply> ReplyList
        {
            get
            {
                var replies = this.context.Replies.ToList();
                return replies;
            }
        }

        public void AddReplyToRepo(Reply reply)
        {
            this.context.Replies.Add(reply);
            this.context.SaveChanges();
        }

        public void UpdateRplyById(Reply rply)
        {
            this.context.Replies.Update(rply);
            this.context.SaveChanges();
        }

        public Reply DeleteRepFromRepo(int replyId)
        {
            var replies = this.context.Replies;
            Reply removedReply = null;
            foreach (Reply r in replies)
            {
                if (r.ReplyID == replyId)
                {
                    removedReply = r;
                    this.context.Replies.Remove(r);
                    this.context.SaveChanges();
                    return removedReply;
                }
            }
            return removedReply;
        }
    }
}
