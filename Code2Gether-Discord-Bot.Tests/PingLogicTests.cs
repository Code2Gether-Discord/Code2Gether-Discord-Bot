using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Tests.Fakes;
using Code2Gether_Discord_Bot.Tests.Fakes.FakeDiscord;
using NUnit.Framework;

namespace Code2Gether_Discord_Bot.Tests
{
    public class PingLogicTests
    {
        IBusinessLogic _logic;

        [SetUp]
        public void Setup()
        {
            var user = new FakeDiscordUser()
            {
                Username = "UnitTest",
                DiscriminatorValue = 1234,
                Id = 123456789123456789
            };

            var client = new FakeDiscordClient()
            {
                FakeApplication = new FakeApplication()
                {
                    Owner = user
                }
            };

            var guild = new FakeGuild()
            {

            };

            var messageChannel = new FakeMessageChannel()
            {

            };

            var message = new FakeUserMessage()
            {
                Author = user
            };

            _logic = new PingLogic(new Logger(GetType()), new FakeCommandContext()
            {
                Client = client,
                Guild = guild,
                User = user,
                Message = message,
                Channel = messageChannel
            }, 999);
        }

        [Test]
        public void InstantiationTest() =>
            Assert.IsNotNull(_logic);

        [Test]
        public async Task EmbedAuthorHasValueTest()
        {
            var result = await _logic.ExecuteAsync();
            Assert.IsTrue(result.Author.HasValue);
        }

        [Test]
        public async Task EmbedColorHasValueTest()
        {
            var result = await _logic.ExecuteAsync();
            Assert.IsTrue(result.Color.HasValue);
        }

        [Test]
        public async Task EmbedHasTitleTest()
        {
            var result = await _logic.ExecuteAsync();
            Assert.IsTrue(result.Title.Length > 0);
        }

        [Test]
        public async Task EmbedHasDescriptionTest()
        {
            var result = await _logic.ExecuteAsync();
            Assert.IsTrue(result.Description.Length > 0);
        }
    }
}