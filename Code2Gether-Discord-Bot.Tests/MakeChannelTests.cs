﻿using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Tests.Fakes;
using Code2Gether_Discord_Bot.Tests.Fakes.FakeDiscord;
using NUnit.Framework;
using Serilog;

namespace Code2Gether_Discord_Bot.Tests
{
    public class MakeChannelTests
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
                Author = user,
                Content = "debug!makechannel make-me"
            };

            _logic = new MakeChannelLogic(Log.Logger.ForContext(GetType()), new FakeCommandContext()
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
            Assert.IsNotNull(_logic);
    }
}
