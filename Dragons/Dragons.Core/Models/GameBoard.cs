using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dragons.Core.Types;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core.Models
{
    /// <summary>
    /// Represents a game board for a single player.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class GameBoard 
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public GameBoard()
        {
            Pieces = new List<List<Piece>>();
        }
  
        /// <summary>
        /// Constructor used to create a game board from an <see cref="InitialSetup">initial setup</see>.
        /// </summary>
        /// <param name="setup">Initial setup used to create board.</param>
        public GameBoard(InitialSetup setup) 
            : this()
        {
            InitialSetup = setup;
            for (var x = 0; x < setup.BoardSize; x++)
            {
                Pieces.Add(new List<Piece>());
                for (var y = 0; y < setup.BoardSize; y++)
                {
                    Pieces[x].Add(new Piece {Coordinate = new Coordinate {X = x, Y = y}, Type = PieceType.Empty});
                }
            }

            foreach (var dragon in setup.Dragons)
                foreach (var piece in dragon)
                    Pieces[piece.Coordinate.X][piece.Coordinate.Y] = piece;

            foreach (var additionalPiece in setup.AdditionalPieces)
                Pieces[additionalPiece.Coordinate.X][additionalPiece.Coordinate.Y] = additionalPiece;
        }

        
        /// <summary>
        /// Initial setup for the board.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        public InitialSetup InitialSetup { get; set; }

        /// <summary>
        /// Two dimensional array of pieces.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        public List<List<Piece>> Pieces { get; set; }

        /// <summary>
        /// List of all the dragons on the board.
        /// </summary>
        [BsonIgnore]
        public List<Dragon> Dragons
        {
            get
            {
                return InitialSetup.Dragons.Select(dragon => new Dragon(dragon.Select(piece => Pieces[piece.Coordinate.X][piece.Coordinate.Y]))).ToList();
            }
        }

        /// <summary>
        /// Returns pretty-printed string
        /// </summary>
        /// <returns>Returns pretty-printed string</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            var line = new StringBuilder();
            line.Append("|");
            foreach (var i in Enumerable.Range(0, this.Pieces.Count))
                line.Append("---");
            line.AppendLine("|");
            sb.Append(line);
            foreach (var column in Pieces)
            {
                sb.Append("|");
                foreach (var piece in column)
                    sb.Append($" {(int) piece.Type} ");
                sb.AppendLine("|");
            }
            sb.Append(line);
            return sb.ToString();
        }
    }
}
