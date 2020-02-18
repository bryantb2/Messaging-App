using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingApp.Models;

namespace MessagingApp.Repositories
{
    public interface IMessageRepo
    {
        List<Message> MessageList { get; }

        void AddMsgToRepo(Message msg);

        Message DeleteMsgFromRepo(int msgId);
    }
}
