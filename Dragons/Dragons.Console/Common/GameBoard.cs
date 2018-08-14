using System.Collections.Generic;

namespace Dragons.Common
{
    public class GameBoard : List<List<GamePiece>>
    {
        public GameBoard(int size)
        {
            for (var x = 0; x < size; x++)
            {
                Add(new List<GamePiece>());
                for (var y = 0; y < size; y++)
                    this[x].Add(new GamePiece());
            }
        }

        public GameBoard() 
        {
        }
    }
}
