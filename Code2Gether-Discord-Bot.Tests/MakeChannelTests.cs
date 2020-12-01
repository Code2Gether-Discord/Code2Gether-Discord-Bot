using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Static;
using Code2Gether_Discord_Bot.Tests.Fakes;
using NUnit.Framework;

namespace Code2Gether_Discord_Bot.Tests
{
    public class MakeChannelTests
    {
        IBusinessLogic _logic;

        [SetUp]
        public void Setup()
        {
            var user = TestConfig.User();

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
                Author = user,
                Content = "debug!makechannel make-me"
            };

            _logic = BusinessLogicFactory.GetMakeChannelLogic(GetType(), new FakeCommandContext()
            {
                Client = client,
                Guild = guild,
                User = user,
                Message = message,
                Channel = messageChannel
            }, "unit-test");
        }

        [Test]
        public void InstantiationTest() =>
            Assert.IsTrue(_logic != null);
    }
}
