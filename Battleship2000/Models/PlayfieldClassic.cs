using System;
using System.Collections.Generic;

namespace Battleship2000.Models
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
            this.Ships= new();

            this.InitializeNewField();
            this.InitializeShips();
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

        public void InitializeShips(ShipLayouts shipLayout = ShipLayouts.Classic)
        {
            switch (shipLayout)
            {
                case ShipLayouts.Classic:
                    this.Ships.Add(new Carrier());
                    this.Ships.Add(new Battleship());
                    this.Ships.Add(new Cruiser());
                    this.Ships.Add(new Destroyer());
                    this.Ships.Add(new Submarine());
                    break;
            }
        }
    }
}
