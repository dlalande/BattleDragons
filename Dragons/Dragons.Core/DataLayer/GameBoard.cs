using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core
{
    [BsonIgnoreExtraElements]
    public class GameBoard : List<List<Piece>>
    {
        public GameBoard()
        { }
  

        public GameBoard(InitialSetup setup)
        {
            
            for (var x = 0; x < setup.BoardSize; x++)
            {
                Add(new List<Piece>());
                for (var y = 0; y < setup.BoardSize; y++)
                {
                    this[x].Add(new Piece {Coordinate = new Coordinate {X = x, Y = y}, Type = PieceType.Map});
                }
            }

            foreach (var dragon in setup.Dragons)
                foreach (var piece in dragon)
                    this[piece.Coordinate.X][piece.Coordinate.Y] = piece;

            foreach (var additionalPiece in setup.AdditionalPieces)
                this[additionalPiece.Coordinate.X][additionalPiece.Coordinate.Y] = additionalPiece;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var line = new StringBuilder();
            line.Append("|");
            foreach (var i in Enumerable.Range(0, this.Count))
                line.Append("---");
            line.AppendLine("|");
            sb.Append(line);
            foreach (var column in this)
            {
                sb.Append("|");
                foreach (var cell in column)
                    sb.Append($" {(int) cell.Type} ");
                sb.AppendLine("|");
            }
            sb.Append(line);
            return sb.ToString();
        }
    }
}
