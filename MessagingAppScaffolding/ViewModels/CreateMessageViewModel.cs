using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingApp.ViewModels
{
    public class CreateMessageViewModel
    {
        [Required]
        [UIHint("Message Title")]
        public String Title { get; set; }

        [Required]
        [UIHint("Message Body")]
        public String MessageBody { get; set; }
    }
}
