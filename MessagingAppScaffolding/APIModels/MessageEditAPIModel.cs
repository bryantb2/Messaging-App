using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingApp.APIModels
{
    public class MessageEditAPIModel
    {
        [Required]
        public string MsgTitle { get; set; }

        [Required]
        public string MsgBody { get; set; }
    }
}
