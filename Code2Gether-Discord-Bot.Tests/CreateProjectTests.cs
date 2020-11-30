using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;
using Code2Gether_Discord_Bot.Static;
using Code2Gether_Discord_Bot.Tests.Fakes;
using Discord;
using NUnit.Framework;

namespace Code2Gether_Discord_Bot.Tests
{
    internal class CreateProjectTests
    {
        private IBusinessLogic _logic;
        private IProjectRepository _repo;
        private IUser _user;

        [SetUp]
        public void Setup()
        {
            _user = new FakeUser()
            {
                Username = "UnitTest",
                DiscriminatorValue = 1234,
                Id = 123456789123456789
            };

            _repo = new FakeProjectRepository()
            {
                Projects = new Dictionary<int, Project>()
            };
        }

        [Test]
        public void InstantiationTest() =>
            Assert.IsTrue(_logic != null);

        /// <summary>
        /// Executing should add a new one from a previously empty project list.
        /// </summary>
        [Test]
        public async Task AddFromEmptyExecutionTest()
        {
            #region Arrange

            var client = new FakeDiscordClient()
            {
                FakeApplication = new FakeApplication()
                {
                    Owner = _user
                }
            };

            var message = new FakeUserMessage()
            {
                Author = _user
            };

            _logic = new CreateProjectLogic(
                UtilityFactory.GetLogger(GetType()),
                new FakeCommandContext()
                {
                    Channel = new FakeMessageChannel(),
                    Client = client,
                    Guild = new FakeGuild(),
                    Message = message,
                    User = _user
                },
                new ProjectManager(_repo),
                "UnitTestProject"
            );

            #endregion

            #region Act

            await _logic.ExecuteAsync();

            #endregion

            #region Assert

            Assert.IsTrue(_repo.ReadAll().Count == 1);

            #endregion
        }

        /// <summary>
        /// Since a project already exists in the repository. Executing again should add a new one.
        /// </summary>
        [Test]
        public async Task AddAnotherProjectExecutionTest()
        {
            #region Arrange

            var client = new FakeDiscordClient()
            {
                FakeApplication = new FakeApplication()
                {
                    Owner = _user
                }
            };

            var message = new FakeUserMessage()
            {
                Author = _user
            };

            var projectManager = new ProjectManager(_repo);
            projectManager.CreateProject("UnitTestProject1", _user);

            _logic = new CreateProjectLogic(
                UtilityFactory.GetLogger(GetType()),
                new FakeCommandContext()
                {
                    Channel = new FakeMessageChannel(),
                    Client = client,
                    Guild = new FakeGuild(),
                    Message = message,
                    User = _user
                },
                projectManager,
                "UnitTestProject2"
            );

            #endregion

            #region Act

            await _logic.ExecuteAsync();

            #endregion

            #region Assert

            Assert.IsTrue(_repo.ReadAll().Count == 2);

            #endregion
        }
    }
}
