using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingApp.Models;

namespace MessagingApp.ViewModels
{
    public class ForumViewModel
    {
        // for viewing chat and chat messages
        public ChatRoom SelectedChat { get; set; }

        // these are for posting up replies and messages
        public CreateMessageViewModel MsgViewModel { get; set; }

        public CreateReplyViewModel RplyViewModel { get; set; }
    }
}
