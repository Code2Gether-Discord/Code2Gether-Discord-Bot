using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Static;
using Code2Gether_Discord_Bot.Tests.Fakes;
using NUnit.Framework;

namespace Code2Gether_Discord_Bot.Tests
{
    internal class InfoLogicTests
    {
        IBusinessLogic _logic;

        [SetUp]
        public void Setup()
        {
            var client = new FakeDiscordClient();
            var guild = new FakeGuild();
            var user = new FakeUser();
            var messageChannel = new FakeMessageChannel();
            var message = new FakeUserMessage();

            _logic = BusinessLogicFactory.InfoLogic(new FakeCommandContext()
            {
                Client = client,
                Guild = guild,
                User = user,
                Message = message,
                Channel = messageChannel
            });
        }

        [Test]
        public void InstantiateTest() =>
            Assert.IsTrue(_logic != null);

        [Test]
        public void EmbedAuthorHasValueTest() =>
            Assert.IsTrue(_logic.Execute().Author.HasValue);

        [Test]
        public void EmbedColorHasValueTest() =>
            Assert.IsTrue(_logic.Execute().Color.HasValue);

        [Test]
        public void EmbedHasTitleTest() =>
            Assert.IsTrue(_logic.Execute().Title.Length > 0);

        [Test]
        public void EmbedHasDescriptionTest() =>
            Assert.IsTrue(_logic.Execute().Description.Length > 0);
    }
}
