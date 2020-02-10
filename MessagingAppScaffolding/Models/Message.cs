using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CommunityWebsite.Models
{
    public class Message
    {
        //CLASS FIELDS (instance variables)
        private string messageContent;
        private string topic;
        private Int32 unixTimeStamp;
        private int messageID;
        private string userNameSignature; //name of user who inputted the message
        private string messageTitle;
        private List<Reply> replies = new List<Reply>();

        //PROPERTIES    
        public string Topic
        {
            get { return this.topic; }
            set { this.topic = value; }
        }
        
        public int MessageID
        {
            get { return this.messageID; }
            set { this.messageID = value; }
        }

        public List<Reply> GetReplyHistory
        {
            get { return this.replies; }
        }

        public void AddToReplyHistory(Reply reply)
        {
            this.replies.Add(reply);
        }

        public void RemoveReplyHistory(int replyID)
        {
            foreach(Reply r in replies)
            {
                if (r.ReplyID == replyID)
                    replies.Remove(r);
            }
        }

        public Int32 UnixTimeStamp
        {
            get { return unixTimeStamp; }
            set { this.unixTimeStamp = value; }
        }

        public string MessageContent
        {
            get { return this.messageContent; }
            set { this.messageContent = value; }
        }

        public DateTime GetTimePosted
        {
            //HUMAN READABLE VERSION OF THE TIME
            //credit goes to: https://stackoverflow.com/questions/249760/how-can-i-convert-a-unix-timestamp-to-datetime-and-vice-versa
            get
            {
                DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                long unixTimeStampInTicks = (long)(this.unixTimeStamp * TimeSpan.TicksPerSecond);
                return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
            }
        }

        public string MessageTitle
        {
            get { return this.messageTitle; }
            set { this.messageTitle = value; }
        }

        public string UserNameSignature
        {
            get { return this.userNameSignature; }
            set { this.userNameSignature = value; }
        }

    }
}
