using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace EngineLayer.Models.Ships
{
    public abstract class Ship
    {
        public enum Orientations
        {
            Horizontal,
            Vertical
        }
        public Orientations Orientation { get; set; }
        public string ShipType { get; set; }
        public int Width { get; set; }
        public int Hits
        {
            get
            {
                return this.securedHitHistory.Count;
            }
        }
        private Stack<int> securedHitHistory;
        public IEnumerable<int> HitHistory
        {
            get
            {
                return this.securedHitHistory.Reverse();
            }
        }
        public bool IsSunk
        {
            get
            {
                return this.Hits >= this.Width;
            }
        }
        public Point Coordinate { get; set; }
        public IEnumerable<Point> HitPointCoordinates
        {
            get
            {
                foreach (int h in this.HitHistory.OrderBy(x => x).ToArray())
                {
                    if (this.Orientation == Orientations.Horizontal)
                    {
                        yield return new Point(this.Coordinate.X + h, this.Coordinate.Y);
                    }
                    if (this.Orientation == Orientations.Vertical)
                    {
                        yield return new Point(this.Coordinate.X, this.Coordinate.Y + h);
                    }
                }
            }
        }

        #region Constructor
        protected Ship()
        {
            this.securedHitHistory = new();
        }
        protected Ship(Orientations orientation) : this()
        {
            this.Orientation = orientation;
        }
        protected Ship(Point coordinates) : this()
        {
            this.Coordinate = coordinates;
        }
        protected Ship(Orientations orientation, Point coordinates) : this(orientation)
        {
            this.Coordinate = coordinates;
        }
        #endregion

        /// <summary>
        /// Zero-based hitpoint register
        /// </summary>
        /// <param name="pointOfShiplength">Zero-based</param>
        /// <exception cref="ArgumentException"></exception>
        public void RegisterHit(int pointOfShiplength)
        {
            if (pointOfShiplength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pointOfShiplength), "Cannot be irrational");
            }
            if (pointOfShiplength > this.Width - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pointOfShiplength), "Cannot be outside of shiplength");
            }
            if (this.IsSunk || this.securedHitHistory.Contains(pointOfShiplength))
            {
                return;
            }
            this.securedHitHistory.Push(pointOfShiplength);
        }

        /// <summary>
        /// Zero-based point of ship length<br/>Repairs a hit point
        /// </summary>
        /// <param name="pointOfShiplength">Zero-based</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Repair(int pointOfShiplength)
        {
            if (pointOfShiplength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pointOfShiplength), "Cannot be irrational");
            }
            if (pointOfShiplength > this.Width - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pointOfShiplength), "Cannot be outside of shiplength");
            }
            if (this.securedHitHistory.Count <= 0 && !this.securedHitHistory.Contains(pointOfShiplength))
            {
                return;
            }
            this.securedHitHistory = new(this.securedHitHistory.Except(new int[] { pointOfShiplength }));
        }
    }
}
