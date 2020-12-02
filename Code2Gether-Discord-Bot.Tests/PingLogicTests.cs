using Code2Gether_Discord_Bot.Library.BusinessLogic;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Tests
{
    public class PingLogicTests
    {
        IBusinessLogic _logic;

        [SetUp]
        public void Setup() =>
            _logic = TestConfig.PingLogic();

        [Test]
        public void InstantiationTest() =>
            Assert.IsTrue(_logic != null);

        [Test]
        public async Task EmbedAuthorHasValueTest()
        {
            var result = await _logic.ExecuteAsync();
            Assert.IsTrue(result.Author.HasValue);
        }

        [Test]
        public async Task EmbedColorHasValueTest()
        {
            var result = await _logic.ExecuteAsync();
            Assert.IsTrue(result.Color.HasValue);
        }

        [Test]
        public async Task EmbedHasTitleTest()
        {
            var result = await _logic.ExecuteAsync();
            Assert.IsTrue(result.Title.Length > 0);
        }

        [Test]
        public async Task EmbedHasDescriptionTest()
        {
            var result = await _logic.ExecuteAsync();
            Assert.IsTrue(result.Description.Length > 0);
        }
    }
}