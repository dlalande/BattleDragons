using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dragons.Core
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
    }
}
