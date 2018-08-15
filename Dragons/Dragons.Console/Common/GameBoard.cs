using System.Collections.Generic;

namespace Dragons.Common
{
    public class GameBoard : List<List<Piece>>
    {
        public GameBoard()
        { }
  

        public GameBoard(int size)
        {
            for (var x = 0; x < size; x++)
            {
                Add(new List<Piece>());
                for (var y = 0; y < size; y++)
                    this[x].Add(new Piece());
            }
        }
    }
}
