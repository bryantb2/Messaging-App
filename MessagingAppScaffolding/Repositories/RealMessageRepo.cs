﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MessagingApp.Data;
using MessagingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MessagingApp.Repositories
{
    public class RealMessageRepo
    {
        private ApplicationDbContext context;

        public RealMessageRepo(ApplicationDbContext c) => this.context = c;

        public List<Message> MessageList 
        {
            get
            {
                var messages = this.context.Messages
                    .Include(msg => msg.GetReplyHistory)
                    .ToList();
                return messages;
            }
        }

        public void AddMsgToRepo(Message msg)
        {
            this.context.Messages.Add(msg);
            this.context.SaveChanges();
        }

        public Message DeleteMsgFromRepo(int msgId)
        {
            var messages = this.context.Messages;
            Message removedMsg = null;
            foreach (Message m in messages)
            {
                if (m.MessageID == msgId)
                {
                    removedMsg = m;
                    this.context.Messages.Remove(m);
                    this.context.SaveChanges();
                    return removedMsg;
                }
            }
            return removedMsg;
        }
    }
}
