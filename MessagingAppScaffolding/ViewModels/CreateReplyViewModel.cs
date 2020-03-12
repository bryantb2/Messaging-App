using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingApp.ViewModels
{
    public class CreateReplyViewModel
    {
        [Required]
        [UIHint("Reply Body")]
        public String MessageBody { get; set; }
    }
}
