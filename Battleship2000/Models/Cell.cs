namespace Battleship2000.Models
{
    public class Cell
    {
        public enum CellStates
        {
            Unknown = 0,
            Hit,
            Empty,
            Miss,
            Ship
        }

        public CellStates CellState { get; set; }
    }
}
