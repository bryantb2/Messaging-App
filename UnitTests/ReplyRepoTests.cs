using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MessagingApp.Models;
using MessagingApp.Repositories;
using Xunit;

namespace UnitTests
{
    public class ReplyRepoTests : IDisposable
    {
        // private fields
        private IReplyRepo replyRepo;
        private Reply testRply1;
        private Reply updatedRply1;

        // setup
        public ReplyRepoTests()
        {
            // arrange
            replyRepo = new FakeReplyRepo();
            testRply1 = new Reply()
            {
                ReplyID = 12,
                ReplyContent = "test",
                Poster = "abc"
            };

            updatedRply1 = new Reply()
            {
                ReplyID = 12,
                ReplyContent = "not test",
                Poster = "123"
            };
        }

        // cleanup and remove code
        public void Dispose() 
        {
            replyRepo = null;
        }

        [Fact]
        public async Task TestAddReply()
        {
            // act
            await replyRepo.AddReplyToRepo(testRply1);

            // assert
            Assert.Contains(testRply1, replyRepo.ReplyList);
        }

        [Fact]
        public async Task TestDeleteReply()
        {
            // arrange
            await replyRepo.AddReplyToRepo(testRply1);

            // act
            await replyRepo.DeleteRepFromRepo(testRply1.ReplyID);

            // assert
            Assert.DoesNotContain(testRply1, replyRepo.ReplyList);
        }

        [Fact]
        public async Task TestUpdateReply()
        {
            // arrange 
            await replyRepo.AddReplyToRepo(testRply1);

            // act
            await replyRepo.UpdateRplyById(updatedRply1);

            // assert
            Assert.Contains(updatedRply1, replyRepo.ReplyList);
            Assert.DoesNotContain(testRply1, replyRepo.ReplyList);
        }
    }
}
