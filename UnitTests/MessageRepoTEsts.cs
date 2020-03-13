using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MessagingApp.Models;
using MessagingApp.Repositories;
using Xunit;

namespace UnitTests
{
    public class MessageRepoTests : IDisposable
    {
        // private fields
        private IMessageRepo messageRepo;
        private Message testMsg1;
        private Message updatedMsg1;

        public MessageRepoTests()
        {
            messageRepo = new FakeMessageRepo();
            testMsg1 = new Message()
            {
                MessageID = 40,
                MessageTitle = "test1",
                MessageContent = "test 1"
            };

            updatedMsg1 = new Message()
            {
                MessageID = 40,
                MessageTitle = "updated test 1",
                MessageContent = "updated test 1"
            };
        }

        public void Dispose()
        {
            messageRepo = null;
            testMsg1 = null;
            updatedMsg1 = null;
        }

        [Fact]
        public async Task TestAddMessage()
        {
            // act
            await messageRepo.AddMsgToRepo(testMsg1);

            // assert
            Assert.Contains(testMsg1, messageRepo.MessageList);
        }

        [Fact]
        public async Task TestRemoveMessage()
        {
            // arrange
            await messageRepo.AddMsgToRepo(testMsg1);

            // act
            await messageRepo.DeleteMsgFromRepo(testMsg1.MessageID);

            // assert
            Assert.DoesNotContain(testMsg1, messageRepo.MessageList);
        }

        [Fact]
        public async Task TestUpdateMessage()
        {
            // arrange
            await messageRepo.AddMsgToRepo(testMsg1);

            // act
            await messageRepo.UpdateMsg(updatedMsg1);

            // assert
            Assert.Contains(updatedMsg1, messageRepo.MessageList);
            Assert.DoesNotContain(testMsg1, messageRepo.MessageList);
        }
    }
}
