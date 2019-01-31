using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core.Models
{
    /// <summary>
    /// Represents a specific coordinate on the game board.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Coordinate
    {
        /// <summary>
        /// Position on the horizontal axis.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public int X { get; set; }

        /// <summary>
        /// Position on the vertical axis.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public int Y { get; set; }

        /// <summary>
        /// Returns a random coordinate bounded by the size of the game board.
        /// </summary>
        /// <param name="boardSize"></param>
        /// <returns></returns>
        public static Coordinate Random(int boardSize)
        {
            return new Coordinate() { X = Dice.Roll(boardSize), Y = Dice.Roll(boardSize) };
        }

        /// <summary>
        /// Returns pretty-printed string
        /// </summary>
        /// <returns>Returns pretty-printed string</returns>
        public override string ToString()
        {
            return $"[{X},{Y}]";
        }

        /// <summary>
        /// Returns true if the given object is a coordinate and the coordinates match.
        /// </summary>
        /// <param name="obj">Object to test.</param>
        /// <returns>Returns true if the given object is a coordinate and the coordinates match.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Coordinate other && Equals(other);
        }

        /// <summary>
        /// Returns true if the coordinates match.
        /// </summary>
        /// <param name="other">Coordinate to test.</param>
        /// <returns>Returns true if the coordinates match.</returns>
        protected bool Equals(Coordinate other)
        {
            return X == other.X && Y == other.Y;
        }

        /// <summary>
        ///  A hash code for the current object.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }
}
