﻿using Battleship2000.Exceptions;
using Battleship2000.Logic;
using Battleship2000.Models;
using Battleship2000.Models.Ships;
using NUnit.Framework;
using System;
using System.Linq;
using System.Windows;

namespace Tests
{
    [TestFixture]
    public class PlayfieldTests
    {
        private PlayfieldClassic testPlayField;

        [SetUp]
        public void SetUp()
        {
            this.testPlayField = new();
        }

        [Test]
        public void PlayfieldInstanceTests()
        {
            Assert.Multiple(() =>
            {
                Assert.That(this.testPlayField.Cells.GetLength(0), Is.EqualTo(10));
                Assert.That(this.testPlayField.Cells.GetLength(1), Is.EqualTo(10));
                Assert.That(this.testPlayField.Cells.Length, Is.EqualTo(10 * 10));
                Assert.DoesNotThrow(() =>
                {
                    this.testPlayField.InitializeNewField(12);
                });
                Assert.That(this.testPlayField.Cells.GetLength(0), Is.EqualTo(12));
                Assert.That(this.testPlayField.Cells.GetLength(1), Is.EqualTo(12));
                Assert.That(this.testPlayField.Cells.Length, Is.EqualTo(12 * 12));
            });

            Cell[] oneDim = new Cell[this.testPlayField.Cells.Length];
            int c = 0;
            for (int i = 0; i < this.testPlayField.Cells.GetLength(0); i++)
            {
                for (int ii = 0; ii < this.testPlayField.Cells.GetLength(1); ii++)
                {
                    oneDim[c] = this.testPlayField.Cells[i, ii];
                    c++;
                }
            }

            Assert.Multiple(() =>
            {
                Assert.That(oneDim.Count(x => x.CellState == Cell.CellStates.Empty), Is.EqualTo(12 * 12));
                Assert.Throws<ArgumentException>(() =>
                {
                    this.testPlayField.InitializeNewField(-4);
                });
                Assert.Throws<ArgumentException>(() =>
                {
                    this.testPlayField.InitializeNewField(0);
                });
                Assert.Throws<ArgumentException>(() =>
                {
                    this.testPlayField.InitializeNewField(1);
                });
            });
        }

        [Test]
        public void ShipPlacementTests()
        {
            Carrier c = new() { Width = 1 };

            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() =>
                {
                    c.Coordinate = new Point(1, 1);
                    this.testPlayField.PlaceShip(c);
                    c.Coordinate = new Point(3, 3);
                    this.testPlayField.PlaceShip(c);
                    c.Coordinate = new Point(0, 7);
                    this.testPlayField.PlaceShip(c);
                    c.Coordinate = new Point(7, 0);
                    this.testPlayField.PlaceShip(c);
                    c.Coordinate = new Point(5, 4);
                    this.testPlayField.PlaceShip(c);
                });

                Assert.Throws<ArgumentException>(() =>
                {
                    c.Coordinate = new Point(10,11);
                    this.testPlayField.PlaceShip(c);
                });
                this.testPlayField = new();
                Assert.Throws<ArgumentException>(() =>
                {
                    c.Coordinate = new Point(-1, 11);
                    this.testPlayField.PlaceShip(c);
                });
                this.testPlayField = new();
                Assert.Throws<ArgumentException>(() =>
                {
                    c.Coordinate = new Point(-1,0);
                    this.testPlayField.PlaceShip(c);
                });
                this.testPlayField = new();
                Assert.Throws<ArgumentException>(() =>
                {
                    c.Coordinate = new Point(0, 11);
                    this.testPlayField.PlaceShip(c);
                });
                this.testPlayField = new();
                Assert.Throws<ArgumentException>(() =>
                {
                    c.Coordinate = new Point(12, 0);
                    this.testPlayField.PlaceShip(c);
                });
                this.testPlayField = new();
                Assert.Throws<ArgumentException>(() =>
                {
                    c.Coordinate = new Point(12, -1);
                    this.testPlayField.PlaceShip(c);
                });
                this.testPlayField = new();
                Assert.Throws<ArgumentException>(() =>
                {
                    c.Coordinate = new Point(-1, 45);
                    this.testPlayField.PlaceShip(c);
                });
 
                this.testPlayField = new();
                Assert.Throws<ArgumentException>(() =>
                {
                    c.Width = 0;
                    this.testPlayField.PlaceShip(c);
                });
                this.testPlayField = new();
                Assert.Throws<ArgumentException>(() =>
                {
                    this.testPlayField.PlaceShip(new Carrier() { Hits = 5, Width = 5 });
                });
                this.testPlayField = new();
                Assert.Throws<InvalidPlacementException>(() =>
                {
                    this.testPlayField.PlaceShip(new Carrier() { Orientation = Ship.Orientations.Horizontal, Coordinate = new Point(0, 0) });
                    this.testPlayField.PlaceShip(new Carrier() { Orientation = Ship.Orientations.Horizontal, Coordinate = new Point(4, 0) });
                });
                this.testPlayField = new();
                Assert.Throws<InvalidPlacementException>(() =>
                {
                    this.testPlayField.PlaceShip(new Carrier() { Orientation = Ship.Orientations.Vertical, Coordinate = new Point(0, 0) });
                    this.testPlayField.PlaceShip(new Carrier() { Orientation = Ship.Orientations.Vertical, Coordinate = new Point(0, 4) });
                });
                this.testPlayField = new();
                Assert.Throws<InvalidPlacementException>(() =>
                {
                    this.testPlayField.PlaceShip(new Carrier() { Orientation = Ship.Orientations.Horizontal, Coordinate = new Point(0, 0) });
                    this.testPlayField.PlaceShip(new Carrier() { Orientation = Ship.Orientations.Horizontal, Coordinate = new Point(1, 0) });
                });
            });
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
