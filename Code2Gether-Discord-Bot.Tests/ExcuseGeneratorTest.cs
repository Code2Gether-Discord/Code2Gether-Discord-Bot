﻿using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Static;
using Code2Gether_Discord_Bot.Tests.Fakes;
using NUnit.Framework;

namespace Code2Gether_Discord_Bot.Tests
{
    internal class ExcuseGeneratorTest
    {
        IBusinessLogic _logic;

        [SetUp]
        public void Setup()
        {
            var user = new FakeUser()
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

            _logic = BusinessLogicFactory.ExcuseGeneratorLogic(GetType(), new FakeCommandContext()
            {
                Client = client,
                Guild = guild,
                User = user,
                Message = message,
                Channel = messageChannel
            });
        }


        [Test]
        public void InstantiationTest() =>
            Assert.IsTrue(_logic != null);

        [Test]
        public void EmbedAuthorHasValueTest() =>
            Assert.IsTrue(_logic.ExecuteAsync().Result.Author.HasValue);

        [Test]
        public void EmbedColorHasValueTest() =>
            Assert.IsTrue(_logic.ExecuteAsync().Result.Color.HasValue);

        [Test]
        public void EmbedHasTitleTest() =>
            Assert.IsTrue(_logic.ExecuteAsync().Result.Title.Length > 0);

        [Test]
        public void EmbedHasDescriptionTest() =>
            Assert.IsTrue(_logic.ExecuteAsync().Result.Description.Length > 0);
    }
}
