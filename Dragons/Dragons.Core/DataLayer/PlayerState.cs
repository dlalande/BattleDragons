using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dragons.Core
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
        public string PlayerId { get; set; }
        
        /// <summary>
        /// Name of player.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// Amount of mana (magic power) the user has.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public int Mana { get; set; }

        /// <summary>
        /// Type of user.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public PlayerType Type { get; set; }

        /// <summary>
        /// Game board of player.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public GameBoard Board { get; set; }
        
        /// <summary>
        /// Collection of the player's dragons that are still alive.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public List<Dragon> Dragons { get; set; }
    }
}
