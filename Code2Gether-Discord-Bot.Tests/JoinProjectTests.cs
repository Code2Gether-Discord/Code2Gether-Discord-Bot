using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;
using Code2Gether_Discord_Bot.Static;
using Code2Gether_Discord_Bot.Tests.Fakes;
using NUnit.Framework;

namespace Code2Gether_Discord_Bot.Tests
{
    internal class JoinProjectTests
    {
        private IBusinessLogic _logic;
        private IProjectRepository _repo;

        [SetUp]
        public void Setup()
        {
            var fakeuser = new FakeDiscordUser()
            {
                Username = "UnitTest",
                DiscriminatorValue = 1234,
                Id = 123456789123456789
            };

            var user = new Member(fakeuser);

            var client = new FakeDiscordClient()
            {
                FakeApplication = new FakeApplication()
                {
                    Owner = fakeuser
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
                Author = fakeuser
            };

            _repo = new FakeProjectRepository()
            {
                Projects = new Dictionary<int, Project>()
                {
                    {0, new Project(0, "UnitTestProject", user)},
                }
            };

            _logic = new JoinProjectLogic(UtilityFactory.GetLogger(GetType()), new FakeCommandContext()
            {
                Channel = messageChannel,
                Client = client,
                Guild = guild,
                Message = message,
                User = fakeuser
            }, new ProjectManager(_repo), "UnitTestProject");
        }

        [Test]
        public void InstantiationTest() =>
            Assert.IsTrue(_logic != null);

        [Test]
        public async Task ExecutionTest()
        {
            await _logic.ExecuteAsync();
            
            // Assert.IsTrue(_repo.Read(0).ProjectMembers.Count > 0);
        }
    }
}
