using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dragons.Core.Types;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dragons.Core.Models
{
    /// <summary>
    /// Represents a spell in the game.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Spell
    {
        /// <summary>
        /// Type of spell.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public SpellType Type { get; set; }
        
        /// <summary>
        /// Description of spell.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Cost of mana to cast the spell.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public int ManaCost { get; set; }

        /// <summary>
        /// Returns pretty-printed string
        /// </summary>
        /// <returns>Returns pretty-printed string</returns>
        public override string ToString()
        {
            return $"{Type} for {ManaCost}";
        }

        
    }
}
