using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core
{
    /// <summary>
    /// Represents a move made by a player in the game.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Move
    {
        /// <summary>
        /// Id of player who made the move.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public string PlayerId { get; set; }

        /// <summary>
        /// Coordinate on game board the move applies to.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public Coordinate Coordinate { get; set; }

        /// <summary>
        /// Spell used in the move.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public Spell Spell { get; set; }
    }
}
