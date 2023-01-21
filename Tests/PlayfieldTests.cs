using Battleship2000.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class PlayfieldTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void PlayfieldInstanceTests()
        {
            PlayfieldClassic p = new();

            Assert.Multiple(() =>
            {
                Assert.That(p.Cells.GetLength(0), Is.EqualTo(10));
                Assert.That(p.Cells.GetLength(1), Is.EqualTo(10));
                Assert.That(p.Cells.Length, Is.EqualTo(10 * 10));
                Assert.DoesNotThrow(() =>
                {
                    p.InitializeNewField(12);
                });
                Assert.That(p.Cells.GetLength(0), Is.EqualTo(12));
                Assert.That(p.Cells.GetLength(1), Is.EqualTo(12));
                Assert.That(p.Cells.Length, Is.EqualTo(12 * 12));
            });

            Cell[] oneDim = new Cell[p.Cells.Length];
            int c = 0;
            for (int i = 0; i < p.Cells.GetLength(0); i++)
            {
                for (int ii = 0; ii < p.Cells.GetLength(1); ii++)
                {
                    oneDim[c] = p.Cells[i, ii];
                    c++;
                }
            }

            Assert.Multiple(() =>
            {
                Assert.That(oneDim.Count(x => x.CellState == Cell.CellStates.Empty), Is.EqualTo(12 * 12));
                Assert.Throws<ArgumentException>(() =>
                {
                    p.InitializeNewField(-4);
                });
                Assert.Throws<ArgumentException>(() =>
                {
                    p.InitializeNewField(0);
                });
                Assert.Throws<ArgumentException>(() =>
                {
                    p.InitializeNewField(1);
                });
            });
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
