using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories;
using Code2Gether_Discord_Bot.Static;
using Code2Gether_Discord_Bot.Tests.Fakes;
using Code2Gether_Discord_Bot.Tests.Fakes.FakeDiscord;
using Code2Gether_Discord_Bot.Tests.Fakes.FakeRepositories;
using NUnit.Framework;

namespace Code2Gether_Discord_Bot.Tests
{
    internal class CreateProjectTests
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
                    { 0, new Project(0, "unittest", user) },
                }
            };

            _logic = new CreateProjectLogic(UtilityFactory.GetLogger(GetType()), new FakeCommandContext()
            {
                Channel = messageChannel,
                Client = client,
                Guild = guild,
                Message = message,
                User = fakeUser
            }, new ProjectManager(_repo), "unittest");
        }

        [Test]
        public void InstantiationTest() =>
            Assert.IsTrue(_logic != null);

        [Test]
        public async Task ExecutionTest()
        {
            await _logic.ExecuteAsync();
            var projects = await _repo.ReadAllAsync();

            Assert.IsTrue(projects.Count() > 0);
        }
    }
}
