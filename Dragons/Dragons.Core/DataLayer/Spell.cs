using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dragons.Core
{
    [BsonIgnoreExtraElements]
    public class Spell
    {
        [BsonElement]
        [JsonConverter(typeof(StringEnumConverter))]
        public SpellType Type { get; set; }
        
        [BsonElement]
        public string Description { get; set; }

        [BsonElement]
        public int ManaCost { get; set; }

        public static List<Spell> AllSpells()
        {
            return new List<Spell>()
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
            };
        }
    }
}
