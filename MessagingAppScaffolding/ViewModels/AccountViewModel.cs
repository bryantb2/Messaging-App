using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingApp.ViewModels
{
    public class AccountViewModel
    {
        public int PostedMsgCount { get; set; }
        public int PostedRplyCount { get; set; }
        public int RepliesRecievedCount { get; set; }
        public DateTime FirstPostDate { get; set; }
        public DateTime RecentPostDate { get; set; }
        public DateTime DateJoined { get; set; }
        public string RegisteredEmail { get; set; }
        public string Username { get; set; }
    }
}
