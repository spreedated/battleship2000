using Battleship2000.ViewLogic;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class HelperTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void GetXmlPathTests()
        {
            string r = HelperFunctions.GetXamlPath("Playfield");

            Assert.Multiple(() =>
            {
                Assert.That(r, Does.Contain("application"));
                Assert.That(r, Does.Contain("pack"));
                Assert.That(r, Does.Contain(",,,"));
                Assert.That(r, Does.Contain("xaml"));
            });

            r = HelperFunctions.GetXamlPath("none");

            Assert.That(r, Is.Null);
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
