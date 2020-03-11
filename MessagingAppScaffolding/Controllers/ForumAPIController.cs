using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingApp.APIModels;
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
        IUserRepo userRepo;

        private UserManager<AppUser> userManager;

        public ForumAPIController(UserManager<AppUser> usrMgr,
                IReplyRepo r, IMessageRepo m, IChatRepo c, IUserRepo u)
        {
            // repos
            this.replyRepo = r;
            this.messageRepo = m;
            this.chatRepo = c;
            // user managers
            this.userManager = usrMgr;
            this.userRepo = u;
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
        public async Task<IActionResult> GetMsgByRplyId(int id)
        {
            var foundRply = replyRepo.ReplyList.Find(rply => rply.ReplyID == id);
            if (foundRply == null)
                return NotFound();
            var msgList = messageRepo.MessageList;
            foreach(Message m in msgList)
            {
                foreach (Reply r in m.GetReplyHistory)
                    if (r.ReplyID == id)
                        return Ok(m); // return parent if reply is found
            }
            return BadRequest();
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChatRoomByMsgId(int id)
        {
            // find message
            var foundMsg = messageRepo.MessageList.Find(msg => msg.MessageID == id);
            if (foundMsg == null)
                return NotFound();
            var chatList = chatRepo.ChatRoomList;
            foreach(ChatRoom chat in chatList)
            {
                var isInChat = chat.ChatMessages.Contains(foundMsg) == true ? true : false;
                if (isInChat)
                    return Ok(chat);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetReplyHistory()
        {
            // get user from context
            var user = await userRepo.GetUserDataAsync(HttpContext.User);
            if (user == null)
                return NotFound();
            return Ok(user.GetReplyHistory);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessageHistory()
        {
            // get user from context
            var user = await userRepo.GetUserDataAsync(HttpContext.User);
            if (user == null)
                return NotFound();
            return Ok(user.GetMessageList);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMsgById(int id, [FromBody] MessageEditAPIModel msgModel)
        {
            if(ModelState.IsValid)
            {
                // get user (this ensures a bad user cannot edit another person's msg)
                var user = await userRepo.GetUserDataAsync(HttpContext.User);
                // find msg
                var foundMsg = user.GetMessageList.Find(msg => msg.MessageID == id);
                if (foundMsg == null)
                    return NotFound();
                // update msg
                foundMsg.MessageTitle = msgModel.MsgTitle;
                foundMsg.MessageContent = msgModel.MsgBody;
                await messageRepo.UpdateMsgById(foundMsg);
                // return msg
                return Ok(foundMsg);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditRplyById(int id, [FromBody] MessageEditAPIModel msgModel)
        {
            if (ModelState.IsValid)
            {
                // get user (this ensures a bad user cannot edit another person's msg)
                var user = await userRepo.GetUserDataAsync(HttpContext.User);
                // find msg
                var foundRply = user.GetReplyHistory.Find(rply => rply.ReplyID == id);
                if (foundRply == null)
                    return NotFound();
                // update msg
                foundRply.ReplyContent = msgModel.MsgBody;
                await replyRepo.UpdateRplyById(foundRply);
                // return msg
                return Ok(foundRply);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMsgById(int id)
        {
            // get user (this ensures a bad user cannot edit another person's msg)
            var user = await userRepo.GetUserDataAsync(HttpContext.User);
            // find msg
            var foundMsg = user.GetMessageList.Find(msg => msg.MessageID == id);
            if (foundMsg == null)
                return NotFound();
            // delete msg
            await messageRepo.DeleteMsgFromRepo(id);
            // return msg
            //return Ok(foundMsg);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRplyById(int id)
        {
            // get user (this ensures a bad user cannot edit another person's rply)
            var user = await userRepo.GetUserDataAsync(HttpContext.User);
            // find msg
            var foundRply = user.GetReplyHistory.Find(rply => rply.ReplyID == id);
            if (foundRply == null)
                return NotFound();
            // delete msg
            await replyRepo.DeleteRepFromRepo(id);
            // return success
            //return Ok(foundRply);
            return NoContent();
        }
    }
}