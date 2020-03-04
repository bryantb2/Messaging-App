using System;
using System.Collections.Generic;

namespace MessagingApp.Models
{
    public class Message
    {
        private List<Reply> replies = new List<Reply>();

        //PROPERTIES    
        public string Topic { get; set; }

        public int MessageID { get; set; }

        public String Poster { get; set; }

        public string MessageTitle { get; set; }

        public string MessageContent { get; set; }

        public List<Reply> GetReplyHistory => this.replies;

        public void AddToReplyHistory(Reply reply) => this.replies.Add(reply);

        public void RemoveReplyHistory(int replyID)
        {
            foreach (Reply r in replies)
            {
                if (r.ReplyID == replyID)
                    replies.Remove(r);
            }
        }

        public Int64 UnixTimeStamp { get; set; }

        public DateTime GetTimePosted
        {
            get
            {
                //https://stackoverflow.com/questions/249760/how-can-i-convert-a-unix-timestamp-to-datetime-and-vice-versa
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(UnixTimeStamp);
                DateTime date = dateTimeOffset.UtcDateTime;
                return date.ToLocalTime();
                // adsf
                /*DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                long unixTimeStampInTicks = (long)(this.UnixTimeStamp * TimeSpan.TicksPerSecond);
                return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);*/
            }
        }
    }
}
