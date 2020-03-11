using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingApp.Models;

namespace MessagingApp.ViewModels
{
    // model combines a create chat VM and a list of chat VMs that are used in the active table
    public class ManageChatsViewModel
    {
        public CreateChatViewModel Chat { get; set; }
        public List<ChatViewModel> ActiveChats { get; set; }
    }
}
