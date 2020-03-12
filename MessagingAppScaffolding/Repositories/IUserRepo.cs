using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MessagingApp.Models;

namespace MessagingApp.Repositories
{
    public interface IUserRepo
    {
        Task<AppUser> GetUserDataAsync(ClaimsPrincipal User);

        List<AppUser> GetAllUsersAndData();
    }
}
