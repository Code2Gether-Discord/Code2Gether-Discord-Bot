using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.BusinessLogic;
using NUnit.Framework;

namespace Code2Gether_Discord_Bot.Tests
{
    internal class ExcuseGeneratorTest
    {
        IBusinessLogic _logic;

        [SetUp]
        public void Setup() =>
            _logic = TestConfig.ExcuseGeneratorLogic();


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
