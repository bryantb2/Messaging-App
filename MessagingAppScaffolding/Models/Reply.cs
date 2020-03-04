using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MessagingApp.Models
{
    public class Reply
    {
        //PROPERTIES
        public int ReplyID { get; set; }

        public String ReplyContent { get; set; }

        public String Poster { get; set; }

        public Int64 UnixTimeStamp { get; set; }

        public DateTime GetTimePosted
        {
            //credit goes to: https://stackoverflow.com/questions/249760/how-can-i-convert-a-unix-timestamp-to-datetime-and-vice-versa
            get
            {
                /*DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                long unixTimeStampInTicks = (long)(this.UnixTimeStamp * TimeSpan.TicksPerSecond);
                return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);*/
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(UnixTimeStamp);
                DateTime date = dateTimeOffset.UtcDateTime;
                return date.ToLocalTime();
            }
        }
    }

    
}
