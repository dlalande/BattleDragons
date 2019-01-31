using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core.Models
{
    /// <summary>
    /// Represents all the game pieces that make of a game board.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Dragon : List<Piece>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Dragon()
        {
        }

        /// <summary>
        /// Constructor using initial list.
        /// </summary>
        /// <param name="pieces">Initial list.</param>
        public Dragon(IEnumerable<Piece> pieces)
            : base(pieces)
        {
        }

        /// <summary>
        /// Returns true if all the pieces have been attacked.
        /// </summary>
        public bool IsDead
        {
            get { return this.All(piece => piece.HasBeenAttacked); }
        }

        /// <summary>
        /// Return true if a given piece is in list.
        /// </summary>
        /// <param name="piece">Piece to test.</param>
        /// <returns>Return true if a given piece is in list.</returns>
        public new bool Contains(Piece piece)
        {
            return this.Any(dragonPiece => dragonPiece.Coordinate.Equals(piece.Coordinate));
        }
    }
}
