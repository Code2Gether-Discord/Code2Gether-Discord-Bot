using Code2Gether_Discord_Bot.Library.BusinessLogic;
using NUnit.Framework;

namespace Code2Gether_Discord_Bot.Tests
{
    internal class InfoLogicTests
    {
        IBusinessLogic _logic;

        [SetUp]
        public void Setup() =>
            _logic = TestConfig.InfoLogic();

        [Test]
        public void InstantiationTest() =>
            Assert.IsTrue(_logic != null);
    }
}
