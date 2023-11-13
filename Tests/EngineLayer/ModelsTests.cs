using EngineLayer.Models.Ships;
using NUnit.Framework;
using System;
using System.Linq;

namespace EngineLayer
{
    [TestFixture]
    public class ModelsTests
    {
        [Test]
        public void RegisterHit_InvalidPoint_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            Ship ship = new MockShip();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => ship.RegisterHit(-1));
        }

        [Test]
        public void RegisterHit_PointOutsideOfShipLength_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            Ship ship = new MockShip();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => ship.RegisterHit(ship.Width));
        }

        [Test]
        public void RegisterHit_ShipIsSunk_Returns()
        {
            // Arrange
            Ship ship = new MockShip();

            // Act
            for (int i = 0; i < ship.Width; i++)
            {
                ship.RegisterHit(i);
                if (ship.Width < i)
                {
                    Assert.That(ship.IsSunk, Is.False);
                }
            }

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(ship.Width, Is.EqualTo(ship.Hits));
                Assert.That(ship.IsSunk, Is.True);
            });
        }

        [Test]
        public void RegisterHit_HitAlreadyRegistered_Returns()
        {
            // Arrange
            Ship ship = new MockShip();
            ship.RegisterHit(0);

            // Act
            ship.RegisterHit(0);

            // Assert
            Assert.That(ship.Hits, Is.EqualTo(1));
        }

        [Test]
        public void RegisterHit_ValidPoint_AddsPointToHitHistory()
        {
            // Arrange
            Ship ship = new MockShip();

            // Act
            ship.RegisterHit(0);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(ship.Hits, Is.EqualTo(1));
                Assert.That(ship.HitHistory.First(), Is.EqualTo(0));
            });
        }

        [Test]
        public void Repair_InvalidPoint_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            Ship ship = new MockShip();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => ship.Repair(-1));
        }

        [Test]
        public void Repair_PointOutsideOfShipLength_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            Ship ship = new MockShip();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => ship.Repair(ship.Width));
        }

        [Test]
        public void Repair_NoHitPoints_Returns()
        {
            // Arrange
            Ship ship = new MockShip();

            // Act
            ship.Repair(0);

            // Assert
            Assert.That(ship.Hits, Is.EqualTo(0));
        }

        [Test]
        public void Repair_ValidPoint_RemovesPointFromHitHistory()
        {
            // Arrange
            Ship ship = new MockShip();
            ship.RegisterHit(0);

            // Act
            ship.Repair(0);

            // Assert
            Assert.That(ship.Hits, Is.EqualTo(0));
        }

        [TearDown]
        public void TearDown()
        {

        }

        private class MockShip : Ship
        {
            public MockShip() : base()
            {
                this.Width = 3;
            }
        }
    }
}
