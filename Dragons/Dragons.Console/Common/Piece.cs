using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragons.Common
{
    public class Piece
    {
        public Piece()
        {
            Coordinate = new Coordinate();
            Type = PieceType.Ground;
        }

        public Coordinate Coordinate { get; set; }
        public PieceType Type { get; set; }
        public string State { get; set; }
        public int Value { get; set; }
        public int Orientation { get; set; }
    }
}
