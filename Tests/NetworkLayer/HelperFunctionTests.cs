using NetworkLayer.Logic;
using NUnit.Framework;

namespace NetworkLayer
{
    [TestFixture]
    public class HelperFunctionTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void IsPortValid_ValidPort_ReturnsTrue()
        {
            // Arrange
            uint port = 1025;

            // Act
            bool result = HelperFunctions.IsPortValid(port);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsPortValid_InvalidPort_ReturnsFalse()
        {
            // Arrange
            uint port = 1024;

            // Act
            bool result = HelperFunctions.IsPortValid(port);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsIpAddressValid_ValidIpAddress_ReturnsTrue()
        {
            // Arrange
            string ipAddress = "192.168.0.1";

            // Act
            bool result = HelperFunctions.IsIpAddressValid(ipAddress);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsIpAddressValid_InvalidIpAddress_ReturnsFalse()
        {
            // Arrange
            string ipAddress = "192.168.0";

            // Act
            bool result = HelperFunctions.IsIpAddressValid(ipAddress);

            // Assert
            Assert.That(result, Is.False);
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
