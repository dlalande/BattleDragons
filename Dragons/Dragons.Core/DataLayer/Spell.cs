using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dragons.Core
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
            return $"{Type} cast for {ManaCost}";
        }

        /// <summary>
        /// List of all the spells in the game.
        /// </summary>
        /// <returns></returns>
        public static readonly IReadOnlyList<Spell> AllSpells = new List<Spell>()
        {
            new Spell()
            {
                Type = SpellType.Meditate,
                Description = "Quietly mediate to restore Mana.",
                ManaCost = 0
            },
            new Spell()
            {
                Type = SpellType.Lightning,
                Description = "Strike a single cell with your standard lightning attack.",
                ManaCost = 5
            },
            new Spell()
            {
                Type = SpellType.FireBall,
                Description = "Singe a 2x2 region with an explosive charge.",
                ManaCost =  20
            },
            new Spell()
            {
                Type = SpellType.FireStorm,
                Description = "Attacks across the entire column of your choice.",
                ManaCost =  35
            },
            new Spell()
            {
                Type = SpellType.IceStrike,
                Description = "Attacks across the entire row of your choice.",
                ManaCost =  35
            },
            new Spell()
            {
                Type = SpellType.DragonFury,
                Description = "Remaining alive dragons each lay waste to one randomly chosen 2x2 region.",
                ManaCost =  60
            }
        }.AsReadOnly();
    }
}
