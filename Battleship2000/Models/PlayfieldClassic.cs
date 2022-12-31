using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship2000.Models
{
    public class PlayfieldClassic
    {
        public Cell[,] Cells { get; private set; }

        #region Constructor
        public PlayfieldClassic()
        {
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
    }
}
