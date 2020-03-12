using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingApp.Models;

namespace MessagingApp.ViewModels
{
    public class ManageMessageViewModel
    {
        public List<Message> MessageList { get; set; }
        public List<Reply> ReplyList { get; set; }
    }
}
