using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core.Models
{
    /// <summary>
    /// Represents the current state of the player during game play.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class PlayerState
    {
        /// <summary>
        /// Id of player.
        /// </summary>
        [BsonId]
        [BsonRequired]
        [Required]
        public Player Player { get; set; }
        
        /// <summary>
        /// Amount of mana (magic power) the user has.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public int Mana { get; set; }

        /// <summary>
        /// Game board of player.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public GameBoard Board { get; set; }
    }
}
