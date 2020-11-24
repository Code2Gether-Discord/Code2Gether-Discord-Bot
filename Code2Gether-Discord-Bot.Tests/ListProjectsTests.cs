using System;
using System.Collections.Generic;
using System.Text;
using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;
using Code2Gether_Discord_Bot.Static;
using Code2Gether_Discord_Bot.Tests.Fakes;
using NUnit.Framework;

namespace Code2Gether_Discord_Bot.Tests
{
    internal class ListProjectsTests
    {
        private IBusinessLogic _logic;
        private IProjectRepository _repo;

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

            _repo = new FakeProjectRepository()
            {
                Projects = new Dictionary<int, Project>()
                {
                    {0, new Project(0, "unittest", user)},
                }
            };

            _logic = new ListProjectsLogic(UtilityFactory.GetLogger(GetType()), new FakeCommandContext()
            {
                Channel = messageChannel,
                Client = client,
                Guild = guild,
                Message = message,
                User = user
            }, _repo);
        }

        [Test]
        public void InstantiationTest() =>
            Assert.IsTrue(_logic != null);

        [Test]
        public void ExecutionTest()
        {
            _logic.Execute();
            Assert.IsTrue(_repo.ReadAll().Count > 0);
        }

        [Test]
        public void EmbedExecutionTest()
        {
            var embed = _logic.Execute();
            Assert.IsTrue(embed.Description.Contains(_repo.Read(0).ToString()));
        }
    }
}
