using EngineLayer.Exceptions;
using EngineLayer.Models;
using EngineLayer.Models.Ships;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace EngineLayer
{
    public class PlayfieldClassic
    {
        public Cell[,] Cells { get; private set; }

        public List<Ship> Ships { get; private set; }

        public enum ShipLayouts
        {
            Classic
        }

        #region Constructor
        public PlayfieldClassic()
        {
            this.Ships = new();

            this.InitializeNewField();
        }

        public PlayfieldClassic(int fieldsize)
        {
            this.InitializeNewField(fieldsize);
        }
        #endregion

        public void InitializeNewField(int fieldsize = 10)
        {
            if (fieldsize <= 1)
            {
                throw new ArgumentException("Integer must be more than one and non-negative", nameof(fieldsize));
            }

            this.Cells = new Cell[fieldsize, fieldsize];

            for (int i = 0; i < this.Cells.GetLength(0); i++)
            {
                for (int ii = 0; ii < this.Cells.GetLength(1); ii++)
                {
                    this.Cells[i, ii] = new()
                    {
                        CellState = Cell.CellStates.Empty
                    };
                }
            }
        }

        public void PlaceShip(Ship ship)
        {
            PropertyInfo coordinateProperty = ship.GetType().GetProperties().First(x => x.PropertyType == typeof(Point));
            IEnumerable<double> coordinatesOfShipObject = ship.Coordinate.GetType().GetProperties().Where(x => x.PropertyType == typeof(double)).Select(x => (double)x.GetValue(coordinateProperty.GetValue(ship)));

            if (ship.Coordinate.X <= -1 || ship.Coordinate.Y <= -1)
            {
                throw new ArgumentException("Coordinates are irrational");
            }
            if (coordinatesOfShipObject.Any(x => x < 0))
            {
                throw new ArgumentException("Coordinates contains negative values");
            }
            if (ship.Coordinate.X > this.Cells.GetLength(0))
            {
                throw new ArgumentException("\"X\" coordinate is out of range of playfield");
            }
            if (ship.Coordinate.Y > this.Cells.GetLength(1))
            {
                throw new ArgumentException("\"Y\" coordinate is out of range of playfield");
            }
            if (ship == null || ship.Width <= 0 || ship.IsSunk)
            {
                throw new ArgumentException("Invalid ship object");
            }

            if (!this.IsPlacementValid(ship))
            {
                throw new InvalidPlacementException("Placement is invalid");
            }

            if (ship.Orientation == Ship.Orientations.Horizontal)
            {
                for (int i = 0; i < ship.Width; i++)
                {
                    this.Cells[ship.Coordinate.Y, ship.Coordinate.X + i].CellState = Cell.CellStates.Ship;
                }
            }
            else
            {
                for (int i = 0; i < ship.Width; i++)
                {
                    this.Cells[ship.Coordinate.Y + i, ship.Coordinate.X].CellState = Cell.CellStates.Ship;
                }
            }


        }

        private bool IsPlacementValid(Ship ship)
        {
            bool isValid = true;

            for (int i = 0; i < ship.Width; i++)
            {
                if (ship.Orientation == Ship.Orientations.Horizontal && ship.Coordinate.Y > this.Cells.GetLength(0))
                {
                    isValid = false;
                    break;
                }
                if (ship.Orientation == Ship.Orientations.Vertical && ship.Coordinate.X > this.Cells.GetLength(1))
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
            {
                for (int i = 0; i < ship.Width; i++)
                {
                    if (ship.Orientation == Ship.Orientations.Horizontal && this.Cells[ship.Coordinate.Y, ship.Coordinate.X + i].CellState != Cell.CellStates.Empty)
                    {
                        isValid = false;
                        break;
                    }
                    if (ship.Orientation == Ship.Orientations.Vertical && this.Cells[ship.Coordinate.Y + i, ship.Coordinate.X].CellState != Cell.CellStates.Empty)
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            if (isValid)
            {
                for (int i = 0; i < ship.Width; i++)
                {
                    if (ship.Orientation == Ship.Orientations.Horizontal && ship.Coordinate.Y - 1 != -1 || ship.Coordinate.X + i != -1)
                    {
                        if (ship.Coordinate.Y - 1 >= 0)
                        {
                            if (this.Cells[ship.Coordinate.Y - 1, ship.Coordinate.X].CellState != Cell.CellStates.Empty)
                            {
                                isValid = false;
                                break;
                            }
                        }
                        if (ship.Coordinate.Y + 1 < this.Cells.GetLength(0))
                        {
                            if (this.Cells[ship.Coordinate.Y + 1, ship.Coordinate.X].CellState != Cell.CellStates.Empty)
                            {
                                isValid = false;
                                break;
                            }
                        }
                        if (i + 1 == ship.Width && this.Cells.GetLength(0) < ship.Width + 1 && this.Cells[ship.Coordinate.Y + 1, ship.Width + 1].CellState != Cell.CellStates.Empty)
                        {
                            isValid = false;
                            break;
                        }
                    }

                    if (ship.Orientation == Ship.Orientations.Vertical && ship.Coordinate.X - 1 != -1 || ship.Coordinate.Y + i != -1)
                    {
                        if (ship.Coordinate.X - 1 > 0)
                        {
                            if (this.Cells[ship.Coordinate.Y, ship.Coordinate.X - 1].CellState != Cell.CellStates.Empty)
                            {
                                isValid = false;
                                break;
                            }
                        }
                        if (ship.Coordinate.X + 1 < this.Cells.GetLength(0))
                        {
                            if (this.Cells[ship.Coordinate.Y, ship.Coordinate.X + 1].CellState != Cell.CellStates.Empty)
                            {
                                isValid = false;
                                break;
                            }
                        }
                        if (i + 1 == ship.Width && this.Cells.GetLength(0) < ship.Width + 1 && this.Cells[ship.Width + 1, ship.Coordinate.X + 1].CellState != Cell.CellStates.Empty)
                        {
                            isValid = false;
                            break;
                        }
                    }
                }
            }

            return isValid;
        }
    }
}
