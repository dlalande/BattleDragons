using System.ComponentModel.DataAnnotations;
using Dragons.Core.Types;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dragons.Core.Models
{
    /// <summary>
    /// Represents a game piece on the board.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Piece
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Piece()
        {
            Coordinate = new Coordinate();
            Type = PieceType.Empty;
        }

        /// <summary>
        /// Coordinate of piece on game board.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public Coordinate Coordinate { get; set; }

        /// <summary>
        /// Type of game piece.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public PieceType Type { get; set; }

        /// <summary>
        /// Determines if the piece has been attacked.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        public bool HasBeenAttacked { get;set; }

        /// <summary>
        /// Piece's visual orientation in rotational degrees.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public int Orientation { get; set; }

        /// <summary>
        /// Returns pretty-printed string
        /// </summary>
        /// <returns>Returns pretty-printed string</returns>
        public override string ToString()
        {
            return $"{Type} piece at {Coordinate}";
        }

        /// <summary>
        /// Returns true if the piece type is a dragon.
        /// </summary>
        /// <returns></returns>
        public bool IsDragonPiece()
        {
            return Type == PieceType.DragonHead || Type == PieceType.DragonBody || Type == PieceType.DragonTail;
        }

        /// <summary>
        /// Returns true if the given object is a piece and the coordinates match.
        /// </summary>
        /// <param name="obj">Object to test.</param>
        /// <returns>Returns true if the given object is a piece and the coordinates match.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Piece other && Equals(other);
        }
        
        /// <summary>
        /// Returns true if the coordinates match.
        /// </summary>
        /// <param name="other">Object to test.</param>
        /// <returns>Returns true if the coordinates match.</returns>
        protected bool Equals(Piece other)
        {
            return Equals(Coordinate, other.Coordinate);
        }

        /// <summary>
        ///  A hash code for the current object.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return (Coordinate != null ? Coordinate.GetHashCode() : 0);
        }
    }
}
