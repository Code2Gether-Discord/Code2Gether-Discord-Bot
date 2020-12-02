using Code2Gether_Discord_Bot.Library.BusinessLogic;
using NUnit.Framework;

namespace Code2Gether_Discord_Bot.Tests
{
    public class MakeChannelTests
    {
        IBusinessLogic _logic;

        [SetUp]
        public void Setup() =>
            _logic = TestConfig.MakeChannelLogic();

        [Test]
        public void InstantiationTest() =>
            Assert.IsTrue(_logic != null);
    }
}
