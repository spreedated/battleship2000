using EngineLayer.Models.Ships;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Miscellaneous
{
    [TestFixture]
    public class ShipModelTests
    {
        private Carrier testShip;

        [SetUp]
        public void SetUp()
        {
            this.testShip = new();
        }

        [Test]
        public void RepairShipTests()
        {
            this.testShip.RegisterHit(1);
            this.testShip.RegisterHit(2);
            this.testShip.RegisterHit(3);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitHistory.Count(), Is.EqualTo(3));
                Assert.That(this.testShip.Hits, Is.EqualTo(3));
            });

            this.testShip.Repair(2);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitHistory.Count(), Is.EqualTo(2));
                Assert.That(this.testShip.Hits, Is.EqualTo(2));
            });

            this.testShip.Repair(2);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitHistory.Count(), Is.EqualTo(2));
                Assert.That(this.testShip.Hits, Is.EqualTo(2));
            });

            this.testShip.Repair(1);
            this.testShip.Repair(3);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitHistory.Count(), Is.EqualTo(0));
                Assert.That(this.testShip.Hits, Is.EqualTo(0));
            });

            this.testShip.Repair(0);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitHistory.Count(), Is.EqualTo(0));
                Assert.That(this.testShip.Hits, Is.EqualTo(0));
            });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                this.testShip.Repair(-1);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                this.testShip.Repair(100);
            });
        }

        [Test]
        public void AntiManipulationTests()
        {
            this.testShip.RegisterHit(0);
            this.testShip.RegisterHit(2);
            this.testShip.RegisterHit(4);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitHistory.Count(), Is.EqualTo(3));
                Assert.That(this.testShip.Hits, Is.EqualTo(3));
            });

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitHistory.Count(), Is.EqualTo(3));
                Assert.That(this.testShip.Hits, Is.EqualTo(3));
            });
        }

        [Test]
        public void CorrectHitPointCoordinatesHorizontal()
        {
            this.testShip.Coordinate = new Point(2, 4);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.Zero);
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitPointCoordinates, Is.Empty);
            });

            this.testShip.RegisterHit(0);
            this.testShip.RegisterHit(1);
            this.testShip.RegisterHit(2);
            this.testShip.RegisterHit(3);
            this.testShip.RegisterHit(4);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.EqualTo(5));
                Assert.That(this.testShip.IsSunk, Is.True);
                Assert.That(this.testShip.HitPointCoordinates.Count(), Is.EqualTo(5));
            });

            IEnumerable<Point> h = this.testShip.HitPointCoordinates;

            for (int i = 0; i < this.testShip.Hits; i++)
            {
                Assert.That(h.ToArray()[i], Is.EqualTo(new Point(2 + i, 4)));
            }

            this.testShip = new()
            {
                Coordinate = new Point(2, 4)
            };

            this.testShip.RegisterHit(2);
            this.testShip.RegisterHit(4);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.EqualTo(2));
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitPointCoordinates.Count(), Is.EqualTo(2));
                Assert.That(this.testShip.HitPointCoordinates.ToArray()[0], Is.EqualTo(new Point(4, 4)));
                Assert.That(this.testShip.HitPointCoordinates.ToArray()[1], Is.EqualTo(new Point(6, 4)));
            });

            this.testShip = new()
            {
                Coordinate = new Point(2, 4)
            };

            this.testShip.RegisterHit(4);
            this.testShip.RegisterHit(2);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.EqualTo(2));
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitPointCoordinates.Count(), Is.EqualTo(2));
                Assert.That(this.testShip.HitPointCoordinates.ToArray()[0], Is.EqualTo(new Point(4, 4)));
                Assert.That(this.testShip.HitPointCoordinates.ToArray()[1], Is.EqualTo(new Point(6, 4)));
            });
        }

        [Test]
        public void CorrectHitPointCoordinatesVertical()
        {
            this.testShip.Coordinate = new Point(2, 4);
            this.testShip.Orientation = Ship.Orientations.Vertical;

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.Zero);
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitPointCoordinates, Is.Empty);
            });

            this.testShip.RegisterHit(0);
            this.testShip.RegisterHit(1);
            this.testShip.RegisterHit(2);
            this.testShip.RegisterHit(3);
            this.testShip.RegisterHit(4);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.EqualTo(5));
                Assert.That(this.testShip.IsSunk, Is.True);
                Assert.That(this.testShip.HitPointCoordinates.Count(), Is.EqualTo(5));
            });

            IEnumerable<Point> h = this.testShip.HitPointCoordinates;

            for (int i = 0; i < this.testShip.Hits; i++)
            {
                Assert.That(h.ToArray()[i], Is.EqualTo(new Point(2, 4 + i)));
            }

            this.testShip = new()
            {
                Coordinate = new Point(2, 4),
                Orientation = Ship.Orientations.Vertical
            };

            this.testShip.RegisterHit(2);
            this.testShip.RegisterHit(4);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.EqualTo(2));
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitPointCoordinates.Count(), Is.EqualTo(2));
                Assert.That(this.testShip.HitPointCoordinates.ToArray()[0], Is.EqualTo(new Point(2, 6)));
                Assert.That(this.testShip.HitPointCoordinates.ToArray()[1], Is.EqualTo(new Point(2, 8)));
            });

            this.testShip = new()
            {
                Coordinate = new Point(2, 4),
                Orientation = Ship.Orientations.Vertical
            };

            this.testShip.RegisterHit(4);
            this.testShip.RegisterHit(2);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.EqualTo(2));
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitPointCoordinates.Count(), Is.EqualTo(2));
                Assert.That(this.testShip.HitPointCoordinates.ToArray()[0], Is.EqualTo(new Point(2, 6)));
                Assert.That(this.testShip.HitPointCoordinates.ToArray()[1], Is.EqualTo(new Point(2, 8)));
            });
        }

        [Test]
        public void MultipleHitTests()
        {
            this.testShip.RegisterHit(4);
            this.testShip.RegisterHit(4);
            this.testShip.RegisterHit(4);
            this.testShip.RegisterHit(4);
            this.testShip.RegisterHit(4);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.EqualTo(1));
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitPointCoordinates.Count(), Is.EqualTo(1));
            });

            this.testShip.RegisterHit(2);
            this.testShip.RegisterHit(2);
            this.testShip.RegisterHit(2);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.EqualTo(2));
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitPointCoordinates.Count(), Is.EqualTo(2));
            });

            this.testShip.RegisterHit(0);
            this.testShip.RegisterHit(0);
            this.testShip.RegisterHit(0);
            this.testShip.RegisterHit(0);
            this.testShip.RegisterHit(0);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.EqualTo(3));
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitPointCoordinates.Count(), Is.EqualTo(3));
            });

            this.testShip.RegisterHit(2);
            this.testShip.RegisterHit(0);
            this.testShip.RegisterHit(4);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.EqualTo(3));
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitPointCoordinates.Count(), Is.EqualTo(3));
            });
        }

        [Test]
        public void RepairTests()
        {
            this.testShip.Orientation = Ship.Orientations.Vertical;

            this.testShip.RegisterHit(4);
            this.testShip.RegisterHit(2);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.EqualTo(2));
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitPointCoordinates.Count(), Is.EqualTo(2));
            });

            this.testShip.Repair(0);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.EqualTo(2));
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitPointCoordinates.Count(), Is.EqualTo(2));
            });

            this.testShip.Repair(4);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.EqualTo(1));
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitPointCoordinates.Count(), Is.EqualTo(1));
            });

            this.testShip.Repair(2);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.Zero);
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitPointCoordinates, Is.Empty);
            });

            this.testShip.Repair(2);
            this.testShip.Repair(3);
            this.testShip.Repair(0);
            this.testShip.Repair(4);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.Zero);
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitPointCoordinates, Is.Empty);
            });

            this.testShip.RegisterHit(2);
            this.testShip.Repair(3);
            this.testShip.RegisterHit(0);
            this.testShip.Repair(4);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.EqualTo(2));
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitPointCoordinates.Count(), Is.EqualTo(2));
            });

            this.testShip.RegisterHit(0);
            this.testShip.RegisterHit(1);
            this.testShip.RegisterHit(2);
            this.testShip.RegisterHit(3);
            this.testShip.RegisterHit(4);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.EqualTo(5));
                Assert.That(this.testShip.IsSunk, Is.True);
                Assert.That(this.testShip.HitPointCoordinates.Count(), Is.EqualTo(5));
            });

            this.testShip.Repair(1);

            Assert.Multiple(() =>
            {
                Assert.That(this.testShip.Hits, Is.EqualTo(4));
                Assert.That(this.testShip.IsSunk, Is.False);
                Assert.That(this.testShip.HitPointCoordinates.Count(), Is.EqualTo(4));
            });
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
