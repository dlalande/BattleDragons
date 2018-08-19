using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core
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
        /// <param name="size"></param>
        /// <returns></returns>
        public static Coordinate Random(int size)
        {
            return new Coordinate() { X = Dice.Roll(size), Y = Dice.Roll(size) };
        }

        /// <summary>
        /// Returns pretty-printed string
        /// </summary>
        /// <returns>Returns pretty-printed string</returns>
        public override string ToString()
        {
            return $"[{X},{Y}]";
        }
    }
}
