using NUnit.Framework;
using System;

namespace NetworkLayer
{
    [TestFixture]
    public class NetworkLayerTests
    {
        private NetworkServer networkServer;

        [SetUp]
        public void SetUp()
        {
            this.networkServer = new("0.0.0.0");
        }

        [Test]
        public void ConstructorTests()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new NetworkServer(null, default);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new NetworkServer("", default);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new NetworkServer("0.0.0.0", 23);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new NetworkServer("0.0.0.0", 1000);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new NetworkServer("0.0.0.0", 70000);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new NetworkServer("0.0.0.0", 65535);
            });

            Assert.DoesNotThrow(() =>
            {
                _ = new NetworkServer("0.0.0.0", 65534);
            });
            Assert.DoesNotThrow(() =>
            {
                _ = new NetworkServer("0.0.0.0", 1025);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new NetworkServer("0...0-0:0");
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new NetworkServer("0.0.0:0");
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new NetworkServer("0.0.0");
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new NetworkServer("0.0.0.256");
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new NetworkServer("123");
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new NetworkServer("0");
            });
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
