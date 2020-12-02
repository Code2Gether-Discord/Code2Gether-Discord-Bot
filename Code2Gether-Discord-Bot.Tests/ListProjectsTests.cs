using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Tests
{
    internal class ListProjectsTests
    {
        private IBusinessLogic _logic;
        private IProjectRepository _repo;

        [SetUp]
        public void Setup()
        {
            _repo = TestConfig.ProjectRepository();
            _repo.Create(TestConfig.Project(0));

            _logic = TestConfig.ListProjectsLogic(_repo);
        }

        [Test]
        public void InstantiationTest() =>
            Assert.IsTrue(_logic != null);

        [Test]
        public async Task ExecutionTest()
        {
            _ = await _logic.ExecuteAsync();
            Assert.IsTrue(_repo.ReadAll().Count > 0);
        }

        [Test]
        public async Task EmbedExecutionTest()
        {
            var embed = await _logic.ExecuteAsync();
            Assert.IsTrue(embed.Description.Contains(_repo.Read(0).ToString()));
        }
    }
}
