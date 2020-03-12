using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingApp.Models;

namespace MessagingApp.ViewModels
{
    public class ManageAdminsViewModel
    {
        public List<AppUser> userList { get; set; }
        public List<AppUser> adminList { get; set; }
    }
}
