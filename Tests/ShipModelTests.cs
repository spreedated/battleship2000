using Battleship2000.Models.Ships;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Tests
{
    [TestFixture]
    public class ShipModelTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void ShipHitRegisterTests()
        {
            Carrier c = new();
            Assert.That(c.Hits, Is.Zero);
            Assert.Throws<InvalidOperationException>(() =>
            {
                c.HitHistory.Peek();
            });
            Assert.That(c.IsSunk, Is.False);

            c.RegisterHit(4);
            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.EqualTo(1));
                Assert.That(c.HitHistory.Peek(), Is.EqualTo(4));
                Assert.That(c.IsSunk, Is.False);
            });

            c.RegisterHit(0);
            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.EqualTo(2));
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitHistory, Has.Count);
                Assert.That(c.HitHistory, Has.Count.EqualTo(2));
                Assert.That(c.HitHistory.Peek(), Is.EqualTo(0));
                Assert.That(c.IsSunk, Is.False);
            });

            c.RegisterHit(1);
            c.RegisterHit(2);
            c.RegisterHit(3);

            Assert.That(c.IsSunk, Is.True);

            c.RegisterHit(3);
            c.RegisterHit(3);
            c.RegisterHit(3);

            Assert.Multiple(() =>
            {
                Assert.That(c.IsSunk, Is.True);
                Assert.That(c.HitHistory, Has.Count.EqualTo(5));
            });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                c.RegisterHit(-1);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                c.RegisterHit(100);
            });
        }

        [Test]
        public void RepairShipTests()
        {
            Carrier c = new();
            c.RegisterHit(1);
            c.RegisterHit(2);
            c.RegisterHit(3);

            Assert.Multiple(() =>
            {
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitHistory, Has.Count.EqualTo(3));
                Assert.That(c.Hits, Is.EqualTo(3));
            });

            c.Repair(2);

            Assert.Multiple(() =>
            {
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitHistory, Has.Count.EqualTo(2));
                Assert.That(c.Hits, Is.EqualTo(2));
            });

            c.Repair(2);

            Assert.Multiple(() =>
            {
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitHistory, Has.Count.EqualTo(2));
                Assert.That(c.Hits, Is.EqualTo(2));
            });

            c.Repair(1);
            c.Repair(3);

            Assert.Multiple(() =>
            {
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitHistory, Has.Count.EqualTo(0));
                Assert.That(c.Hits, Is.EqualTo(0));
            });

            c.Repair(0);

            Assert.Multiple(() =>
            {
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitHistory, Has.Count.EqualTo(0));
                Assert.That(c.Hits, Is.EqualTo(0));
            });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                c.Repair(-1);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                c.Repair(100);
            });
        }

        [Test]
        public void AntiManipulationTests()
        {
            Carrier c = new();

            c.RegisterHit(0);
            c.RegisterHit(2);
            c.RegisterHit(4);

            Assert.Multiple(() =>
            {
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitHistory, Has.Count.EqualTo(3));
                Assert.That(c.Hits, Is.EqualTo(3));
            });

            for (int i = 0; i < c.HitHistory.Count; i++)
            {
                c.HitHistory.Pop();
            }

            Assert.Multiple(() =>
            {
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitHistory, Has.Count.EqualTo(3));
                Assert.That(c.Hits, Is.EqualTo(3));
            });
        }

        [Test]
        public void EnsureCorrectStack()
        {
            Carrier c = new();

            int[] hits = new int[] { 2, 0, 4, 1 };

            for (int i = 0; i < hits.Length; i++)
            {
                c.RegisterHit(hits[i]);
            }

            Stack<int> b = new(c.HitHistory.Reverse());
            int l = b.Count;

            for (int i = 0; i < l; i++)
            {
                Assert.That(b.Pop(), Is.EqualTo(hits.Reverse().ToArray()[i]));
            }

            Assert.Throws<InvalidOperationException>(() =>
            {
                for (int i = 0; i < c.HitHistory.Count; i++)
                {
                    if (c.HitHistory.Pop() != hits.Reverse().ToArray()[i])
                    {
                        throw new InvalidOperationException();
                    }
                }
            });
        }

        [Test]
        public void CorrectHitPointCoordinates()
        {
            Carrier c = new()
            {
                Coordinate = new Point(2, 4)
            };

            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.Zero);
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitPointCoordinates, Is.Empty);
            });

            c.RegisterHit(0);
            c.RegisterHit(1);
            c.RegisterHit(2);
            c.RegisterHit(3);
            c.RegisterHit(4);

            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.EqualTo(5));
                Assert.That(c.IsSunk, Is.True);
                Assert.That(c.HitPointCoordinates.Count(), Is.EqualTo(5));
            });

            IEnumerable<Point> h = c.HitPointCoordinates;

            for (int i = 0; i < c.Hits; i++)
            {
                Assert.That(h.ToArray()[i], Is.EqualTo(new Point(2 + i, 4)));
            }

            c = new()
            {
                Coordinate = new Point(2, 4)
            };

            c.RegisterHit(2);
            c.RegisterHit(4);

            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.EqualTo(2));
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitPointCoordinates.Count(), Is.EqualTo(2));
                Assert.That(c.HitPointCoordinates.ToArray()[0], Is.EqualTo(new Point(4, 4)));
                Assert.That(c.HitPointCoordinates.ToArray()[1], Is.EqualTo(new Point(6, 4)));
            });

            c = new()
            {
                Coordinate = new Point(2, 4)
            };

            c.RegisterHit(4);
            c.RegisterHit(2);

            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.EqualTo(2));
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitPointCoordinates.Count(), Is.EqualTo(2));
                Assert.That(c.HitPointCoordinates.ToArray()[0], Is.EqualTo(new Point(4, 4)));
                Assert.That(c.HitPointCoordinates.ToArray()[1], Is.EqualTo(new Point(6, 4)));
            });
        }

        [Test]
        public void MultipleHitTests()
        {
            Carrier c = new();

            c.RegisterHit(4);
            c.RegisterHit(4);
            c.RegisterHit(4);
            c.RegisterHit(4);
            c.RegisterHit(4);

            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.EqualTo(1));
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitPointCoordinates.Count(), Is.EqualTo(1));
            });

            c.RegisterHit(2);
            c.RegisterHit(2);
            c.RegisterHit(2);

            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.EqualTo(2));
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitPointCoordinates.Count(), Is.EqualTo(2));
            });

            c.RegisterHit(0);
            c.RegisterHit(0);
            c.RegisterHit(0);
            c.RegisterHit(0);
            c.RegisterHit(0);

            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.EqualTo(3));
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitPointCoordinates.Count(), Is.EqualTo(3));
            });

            c.RegisterHit(2);
            c.RegisterHit(0);
            c.RegisterHit(4);

            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.EqualTo(3));
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitPointCoordinates.Count(), Is.EqualTo(3));
            });
        }

        [Test]
        public void RepairTests()
        {
            Carrier c = new();

            c.RegisterHit(4);
            c.RegisterHit(2);

            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.EqualTo(2));
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitPointCoordinates.Count(), Is.EqualTo(2));
            });

            c.Repair(0);

            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.EqualTo(2));
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitPointCoordinates.Count(), Is.EqualTo(2));
            });

            c.Repair(4);

            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.EqualTo(1));
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitPointCoordinates.Count(), Is.EqualTo(1));
            });

            c.Repair(2);

            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.Zero);
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitPointCoordinates, Is.Empty);
            });

            c.Repair(2);
            c.Repair(3);
            c.Repair(0);
            c.Repair(4);

            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.Zero);
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitPointCoordinates, Is.Empty);
            });

            c.RegisterHit(2);
            c.Repair(3);
            c.RegisterHit(0);
            c.Repair(4);

            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.EqualTo(2));
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitPointCoordinates.Count(), Is.EqualTo(2));
            });

            c.RegisterHit(0);
            c.RegisterHit(1);
            c.RegisterHit(2);
            c.RegisterHit(3);
            c.RegisterHit(4);

            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.EqualTo(5));
                Assert.That(c.IsSunk, Is.True);
                Assert.That(c.HitPointCoordinates.Count(), Is.EqualTo(5));
            });

            c.Repair(1);

            Assert.Multiple(() =>
            {
                Assert.That(c.Hits, Is.EqualTo(4));
                Assert.That(c.IsSunk, Is.False);
                Assert.That(c.HitPointCoordinates.Count(), Is.EqualTo(4));
            });
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
