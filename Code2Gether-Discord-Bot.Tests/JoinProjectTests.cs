﻿using System.Collections.Generic;
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
    internal class JoinProjectTests
    {
        private IBusinessLogic _logic;
        private IMemberRepository _memberRepository;
        private IProjectRepository _projectRepository;

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

            _memberRepository = new FakeMemberRepository();

            _projectRepository = new FakeProjectRepository()
            {
                Projects = new Dictionary<int, Project>()
                {
                    {0, new Project("UnitTestProject", user)},
                }
            };

            _logic = new JoinProjectLogic(Log.Logger.ForContext(GetType()), new FakeCommandContext()
            {
                Channel = messageChannel,
                Client = client,
                Guild = guild,
                Message = message,
                User = fakeuser
            }, new ProjectManager(_memberRepository, _projectRepository), "UnitTestProject");
        }

        [Test]
        public void InstantiationTest() =>
            Assert.IsNotNull(_logic);

        /// <summary>
        /// Passes: If joining an existing project results in 1 member
        /// Fails:  If joining an existing project results in 0 members
        /// </summary>
        [Test]
        public async Task SingleExecutionTest()
        {
            await _logic.ExecuteAsync();
            var project = await _projectRepository.ReadAsync(0);
            Assert.AreEqual(1, project.Members.Count);
        }

        /// <summary>
        /// Passes: If joining an existing project twice results in only 1 member
        /// Fails:  If joining an existing project twice results 2 duplicate members
        /// </summary>
        [Test]
        public async Task DoubleExecutionTest()
        {
            await _logic.ExecuteAsync();
            await _logic.ExecuteAsync();
            var project = await _projectRepository.ReadAsync(0);
            
            Assert.AreEqual(1, project.Members.Count);
        }
    }
}
