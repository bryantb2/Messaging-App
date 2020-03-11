﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingApp.Models;

namespace MessagingApp.ViewModels
{
    public class ManageChatsViewModel
    {
        public CreateChatViewModel chat { get; set; }
        public List<ChatRoom> activeChats { get; set; }
    }
}
