using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Tests
{
    internal class JoinProjectTests
    {
        private IBusinessLogic _logic;
        private IProjectRepository _repo;

        [SetUp]
        public void Setup()
        {
            _repo = TestConfig.ProjectRepository();

            const string PROJECT_NAME = "testProj";
            _repo.Create(TestConfig.Project(0, PROJECT_NAME));

            _logic = TestConfig.JoinProjectLogic(_repo, PROJECT_NAME);
        }

        [Test]
        public void InstantiationTest() =>
            Assert.IsTrue(_logic != null);

        [Test]
        public async Task ExecutionTest()
        {
            var task = await _logic.ExecuteAsync();

            Assert.IsTrue(_repo.Read(0).ProjectMembers.Count > 0);
        }
    }
}
