using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dragons.Core
{
    [BsonIgnoreExtraElements]
    public class Player
    {
        [BsonId]
        public Guid PlayerId { get; set; }
        
        [BsonElement]
        public string Name { get; set; }
        
        [BsonElement]
        public int Mana { get; set; }

        [BsonElement]
        [JsonConverter(typeof(StringEnumConverter))]
        public PlayerType Type { get; set; }

        [BsonElement]
        public GameBoard Board { get; set; }
        
        [BsonElement]
        public List<Dragon> Dragons { get; set; }
    }
}
