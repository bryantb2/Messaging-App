﻿using MessagingApp.Data;
using MessagingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MessagingApp.Repositories
{
    public class RealUserRepo : IUserRepo
    {
        private ApplicationDbContext context;
        private UserManager<AppUser> userManager;
        public RealUserRepo(UserManager<AppUser> usrMgr, ApplicationDbContext c)
        {
            this.context = c;
            this.userManager = usrMgr;
        }

        public async Task<AppUser> GetUserDataAsync(ClaimsPrincipal User)
        {
            // get user async to extract id
            var user = await userManager.GetUserAsync(User);
            return this.context.Users
                .Include(usr => usr.GetMessageList)
                    .ThenInclude(msg => msg.GetReplyHistory)
                .Include(usr=> usr.GetReplyHistory)
                    .ToList().Find(usr => usr.Id == user.Id);
        }
    }
}
