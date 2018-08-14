using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragons.Common
{
    public class GamePiece
    {
        public GamePiece()
        {
            Coordinate = new Coordinate();
            Type = GamePieceType.Ground;
        }

        public Coordinate Coordinate { get; set; }
        public GamePieceType Type { get; set; }
        public string State { get; set; }
        public int Value { get; set; }
        public int Orientation { get; set; }
    }
}
