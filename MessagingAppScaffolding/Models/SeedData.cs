using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingApp.Data;
using MessagingApp.Models;
using MessagingApp.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingApp.Models
{
    public static class SeedData
    {
        private static async Task<IdentityResult> AddUserAsync(UserManager<AppUser> userManager, AppUser user)
        {
            return await userManager.CreateAsync(user);
        }

        public static async void SeedDB(ApplicationDbContext c, IServiceProvider prov) //UserManager<AppUser> userManager)
        {
            //ApplicationDbContext context = c;
            ApplicationDbContext context = prov.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();

            if(!context.ChatRooms.Any())
            {
                UserManager<AppUser> userManager = null;
                RoleManager<IdentityRole> roleManager = null;
                userManager = prov.GetRequiredService<UserManager<AppUser>>();
                roleManager = prov.GetRequiredService<RoleManager<IdentityRole>>();
                // repos
                RealChatRepo chatRepo = new RealChatRepo(context);
                RealMessageRepo msgRepo = new RealMessageRepo(context);
                RealReplyRepo rplyRepo = new RealReplyRepo(context);

                // app user creation
                var user1 = new AppUser
                {
                    UserName = "NoobSlayer",
                    NormalizedUserName = "NOOBSLAYER",
                    Email = "abc123@gmail.com",
                    NormalizedEmail = "ABC123@GMAIL.COM",
                    DateJoined = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                };

                var user2 = new AppUser
                {
                    UserName = "Kalashnikov",
                    NormalizedUserName = "KALASHNIKOV",
                    Email = "ak47@yahoo.com",
                    NormalizedEmail = "AK47@YAHOO.COM",
                    DateJoined = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                };

                var user3 = new AppUser
                {
                    UserName = "ItalianCowboy",
                    NormalizedUserName = "ITALIANCOWBODY",
                    Email = "cowboy@gmail.com",
                    NormalizedEmail = "COWBOY@GMAIL.COM",
                    DateJoined = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                };

                // creating chat repos
                var chat1 = new ChatRoom()
                {
                    ChatName = "General",
                    UnixTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                };

                var chat2 = new ChatRoom()
                {
                    ChatName = "Photography",
                    UnixTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                };

                var chat3 = new ChatRoom()
                {
                    ChatName = "Coding",
                    UnixTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                };

                // building password hashes
                var userPasswordArr = new String[3] { "WhoaDude123!", "MotherRussia123!", "MeatBallRevolver123!" };
                var userArr = new AppUser[3] { user1, user2, user3 };
                for (var i = 0; i < userPasswordArr.Length; i++)
                {
                    var hasher = new PasswordHasher<AppUser>();
                    var hashedPassword = hasher.HashPassword(userArr[i], userPasswordArr[i]);
                    userArr[i].PasswordHash = hashedPassword;

                    // add user
                    await AddUserAsync(userManager, userArr[i]);
                }

                // building and assigning roles
                var roles = new String[3] { "owner", "admin", "standard" };
                for (var i = 0; i < roles.Length; i++)
                {
                    var currentUser = userArr[i];
                    var currentRole = roles[i];

                    // building roles
                    if(await roleManager.FindByNameAsync(currentRole) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(currentRole));
                    }

                    // find users
                    var foundAppUser = await userManager.FindByNameAsync(currentUser.UserName);
                    if (foundAppUser != null)
                    {
                        await userManager.AddToRoleAsync(foundAppUser, currentRole);
                    }
                }

                // adding users and chat rooms to DB
                await chatRepo.CreateChatRoom(chat1);
                await chatRepo.CreateChatRoom(chat2);
                await chatRepo.CreateChatRoom(chat3);
                
                // message content
                var msgTitleArr = new String[5] { "test 1", "test 2", "test 3", "test 4", "test 5" };
                var msgBodyArr = new String[5] { "hello there, friend", "I hate Java, long live C#", "C# is superior", "Jk, Javascript is much more functional, you absolute nerd nuggets", "electroswing is an unrated and truly fantastic genre" };
                var msgPosterArr = new AppUser[5] { user1, user2, user2, user1, user3 };
                var msgTimeStampArr = new Int64[5] { DateTimeOffset.UtcNow.ToUnixTimeSeconds(), DateTimeOffset.UtcNow.ToUnixTimeSeconds(), DateTimeOffset.UtcNow.ToUnixTimeSeconds(), DateTimeOffset.UtcNow.ToUnixTimeSeconds(), DateTimeOffset.UtcNow.ToUnixTimeSeconds() };
                var msgChatRoomArr = new ChatRoom[5] { chat1, chat2, chat3, chat1, chat2 };

                // reply content
                var rplyBodyArr = new String[5] { "trash lies, dude", "JAVA IS BETTER YOU IDIOT!", "This chat is a dumpster fire", "Javascript literally uses prototype inheritance...", "I actually agree with this..." };
                var rplyPosterArr = new AppUser[5] { user2, user1, user3, user3, user2 };
                var rplyTimeStampArr = new Int64[5] { DateTimeOffset.UtcNow.ToUnixTimeSeconds(), DateTimeOffset.UtcNow.ToUnixTimeSeconds(), DateTimeOffset.UtcNow.ToUnixTimeSeconds(), DateTimeOffset.UtcNow.ToUnixTimeSeconds(), DateTimeOffset.UtcNow.ToUnixTimeSeconds() };

                // dynamically generate messages and replies
                // add messages to chat repos
                // add replies to messages
                // add messages and replies to user history
                for(var i = 0; i < msgTitleArr.Length; i++)
                {
                    var msgChatRoom = msgChatRoomArr[i];

                    var msgTitle = msgTitleArr[i];
                    var msgBody = msgBodyArr[i];
                    var msgPoster = msgPosterArr[i];
                    var msgTimeStamp = msgTimeStampArr[i];
                    var msg = new Message()
                    {
                        MessageTitle = msgTitle,
                        MessageContent = msgBody,
                        Poster = msgPoster.UserName,
                        UnixTimeStamp = msgTimeStamp
                    };

                    var rplyBody = rplyBodyArr[i];
                    var rplyPoster = rplyPosterArr[i];
                    var rplyTimeStamp = rplyTimeStampArr[i];
                    var rply = new Reply()
                    {
                        ReplyContent = rplyBody,
                        Poster = rplyPoster.UserName,
                        UnixTimeStamp = rplyTimeStamp
                    };

                    await msgRepo.AddMsgToRepo(msg);
                    await rplyRepo.AddReplyToRepo(rply);
                    await msgRepo.AddReplytoMsg(rply, msg.MessageID);
                    await chatRepo.AddMsgToChat(msgChatRoom.ChatRoomID, msg);

                    msgPoster.AddMessageToHistory(msg);
                    rplyPoster.AddToReplyHistory(rply);
                    await userManager.UpdateAsync(msgPoster);
                    await userManager.UpdateAsync(rplyPoster);
                }

                context.SaveChanges(); // just in case :)
            }
        }
    }
}
