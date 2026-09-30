using System;
using Xunit;
using MessagingApp.Models;
using MessagingApp.Repositories;

namespace UnitTests
{
    public class ChatRepoTests : IDisposable
    {
        // private fields
        private IChatRepo chatRepo;
        private ChatRoom chat1;

        // setup
        public ChatRepoTests()
        {
            chatRepo = new FakeChatRepo();
            chat1 = new ChatRoom()
            {
                ChatRoomID = 64,
                ChatName = "General Chat",
            };
        }

        // cleanup and dispoable
        public void Dispose()
        {
            chatRepo = null;
            chat1 = null;
        }

        [Fact]
        public async void TestAddChat()
        {
            // act
            await chatRepo.CreateChatRoom(chat1);

            // assert
            Assert.Contains(chat1, chatRepo.ChatRoomList);
        }

        [Fact]
        public async void TestRemoveChat()
        {
            // arrange
            await chatRepo.CreateChatRoom(chat1);

            // act
            await chatRepo.DeleteChatRoom(chat1.ChatRoomID);

            // assert
            Assert.DoesNotContain(chat1, chatRepo.ChatRoomList);
        }
    }
}
