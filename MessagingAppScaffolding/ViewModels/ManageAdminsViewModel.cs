using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingApp.Models;

namespace MessagingApp.ViewModels
{
    public class ManageAdminsViewModel
    {
        public List<AdminViewModel> UserList { get; set; }
        public List<AdminViewModel> AdminList { get; set; }
    }
}
