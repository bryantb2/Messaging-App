using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MessagingApp.Models
{
    public class AppUser : IdentityUser  
    {
        //CLASS FIELDS
        private List<Message> messageHistory = new List<Message>();
        private List<Reply> replyHistory = new List<Reply>();

        //PROPERTIES
        public Int64 DateJoined { get; set; }

        public List<Reply> GetReplyHistory
        {
            get { return this.replyHistory; }
        }
        
        public List<Message> GetMessageList
        {
            get { return this.messageHistory; }
        }

        //METHODS
        public void AddToReplyHistory(Reply reply)
        {
            this.replyHistory.Add(reply);
        }

        public void RemoveReplyFromHistory(int repyID)
        {
            foreach (Reply reply in this.replyHistory)
            {
                if (reply.ReplyID == repyID)
                {
                    this.replyHistory.Remove(reply);
                }
            }
        }

        public void AddMessageToHistory(Message message)
        {
            this.messageHistory.Add(message);
        }

        public void RemoveMessageFromHistory(int messageID)
        {
            foreach (Message message in this.messageHistory)
            {
                if (message.MessageID == messageID)
                {
                    this.messageHistory.Remove(message);
                }
            }
        }

        public DateTime GetDateJoined
        {
            get
            {
                //https://stackoverflow.com/questions/249760/how-can-i-convert-a-unix-timestamp-to-datetime-and-vice-versa
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(DateJoined);
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
