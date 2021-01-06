﻿using System.Linq;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories;
using Code2Gether_Discord_Bot.Tests.Fakes;
using Code2Gether_Discord_Bot.Tests.Fakes.FakeDiscord;
using Code2Gether_Discord_Bot.Tests.Fakes.FakeRepositories;
using NUnit.Framework;
using Serilog;

namespace Code2Gether_Discord_Bot.Tests
{
    internal class CreateProjectTests
    {
        private IBusinessLogic _logic;
        private IMemberRepository _memberRepository;
        private IProjectRepository _projectRepository;

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

            _memberRepository = new FakeMemberRepository();
            _projectRepository = new FakeProjectRepository();

            _logic = new CreateProjectLogic(Log.Logger.ForContext(GetType()), new FakeCommandContext()
            {
                Channel = messageChannel,
                Client = client,
                Guild = guild,
                Message = message,
                User = fakeUser
            }, new ProjectManager(_memberRepository, _projectRepository), "unittest");
        }

        [Test]
        public void InstantiationTest() =>
            Assert.IsNotNull(_logic);

        /// <summary>
        /// Passes: If when creating a new project, project repo contains an additional project
        /// Fails:  If when creating a new project, project repo does not change
        /// </summary>
        [Test]
        public async Task SingleExecutionTest()
        {
            var initialProjects = await _projectRepository.ReadAllAsync();
            await _logic.ExecuteAsync();
            var finalProjects = await _projectRepository.ReadAllAsync();

            Assert.AreEqual(initialProjects.Count() + 1, finalProjects.Count());
        }

        /// <summary>
        /// Passes: If when creating a duplicate project, total projects do not change during second execution
        /// Fails:  If when creating a duplicate project, an two additional projects now exist in project repo
        /// </summary>
        [Test]
        public async Task DoubleExecutionTest()
        {
            await _logic.ExecuteAsync();
            var intermediaryProjects = await _projectRepository.ReadAllAsync();
            await _logic.ExecuteAsync();

            var finalProjects = await _projectRepository.ReadAllAsync();

            Assert.AreEqual(intermediaryProjects.Count(), finalProjects.Count());
        }
    }
}
