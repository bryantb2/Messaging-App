using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingApp.ViewModels
{
    public class AdminViewModel
    {
        // view model for manage chats page
        public String UserID { get; set; }
        public String Username { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime RecentActivity { get; set; }
        public int NumberOfPosts { get; set; }
    }
}
