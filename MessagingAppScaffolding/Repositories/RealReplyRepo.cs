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

        public async Task AddReplyToRepo(Reply reply)
        {
            this.context.Replies.Add(reply);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateRplyById(Reply rply)
        {
            this.context.Replies.Update(rply);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteRepFromRepo(int replyId)
        {
            var replies = this.context.Replies;
            foreach (Reply r in replies)
            {
                if (r.ReplyID == replyId)
                {
                    this.context.Replies.Remove(r);
                }
            }
            await this.context.SaveChangesAsync();
        }
    }
}
