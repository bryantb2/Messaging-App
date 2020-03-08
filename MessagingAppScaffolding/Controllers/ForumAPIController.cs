using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingApp.Models;
using MessagingApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MessagingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ForumAPIController : Controller
    {
        IReplyRepo replyRepo;
        IMessageRepo messageRepo;
        IChatRepo chatRepo;

        private UserManager<AppUser> userManager;

        public ForumAPIController(UserManager<AppUser> usrMgr,
                IReplyRepo r, IMessageRepo m, IChatRepo c)
        {
            // repos
            this.replyRepo = r;
            this.messageRepo = m;
            this.chatRepo = c;
            // user managers
            this.userManager = usrMgr;
        }

        // API ROUTES
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRplyById(int id)
        {
            var foundRply = replyRepo.ReplyList.Find(rply => rply.ReplyID == id);
            if (foundRply != null)
                return Ok(foundRply);
            else
                return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMsgById(int id)
        {
            var foundMsg = messageRepo.MessageList.Find(msg => msg.MessageID == id);
            if (foundMsg != null)
                return Ok(foundMsg);
            else
                return NotFound();
        }
    }
}