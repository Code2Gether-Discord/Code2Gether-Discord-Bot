using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories;
using Code2Gether_Discord_Bot.Tests.Fakes;
using Code2Gether_Discord_Bot.Tests.Fakes.FakeDiscord;
using Code2Gether_Discord_Bot.Tests.Fakes.FakeRepositories;
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
            var fakeUser = new FakeDiscordUser()
            {
                Username = "UnitTest",
                DiscriminatorValue = 1234,
                Id = 123456789123456789
            };

            var user = new Member(fakeUser);

            var client = new FakeDiscordClient()
            {
                FakeApplication = new FakeApplication()
                {
                    Owner = fakeUser
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
                Author = fakeUser
            };

            _repo = new FakeProjectRepository()
            {
                Projects = new Dictionary<int, Project>()
                {
                    {0, new Project(0, "unittest", user)},
                }
            };

            _logic = new ListProjectsLogic(new Logger(GetType()), new FakeCommandContext()
            {
                Channel = messageChannel,
                Client = client,
                Guild = guild,
                Message = message,
                User = fakeUser
            }, _repo);
        }

        [Test]
        public void InstantiationTest() =>
            Assert.IsNotNull(_logic);

        [Test]
        public async Task EmbedContainsEveryProjectNameExecutionTest()
        {
            var embed = await _logic.ExecuteAsync();
            var projects = await _repo.ReadAllAsync();
            var projectNames = projects.Select(p => p.Name);

            bool hasEveryProjectName = true;
            foreach (var projectName in projectNames)
            {
                if (!embed.Description.Contains(projectName))
                {
                    hasEveryProjectName = false;
                }
            }

            Assert.IsTrue(hasEveryProjectName);
        }

        [Test]
        public async Task EmbedContainsTotalProjectCountExecutionTest()
        {
            var embed = await _logic.ExecuteAsync();
            var projects = await _repo.ReadAllAsync();

            Assert.IsTrue(embed.Title.Contains(projects.Count().ToString()));
        }
    }
}
