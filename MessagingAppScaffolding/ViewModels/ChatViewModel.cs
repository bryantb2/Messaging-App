using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingApp.ViewModels
{
    public class ChatViewModel
    {
        // view model for manage chats page
        public int ChatID { get; set; }
        public String Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime RecentActivity { get; set; }
        public int NumberOfPosts { get; set; }
    }
}
