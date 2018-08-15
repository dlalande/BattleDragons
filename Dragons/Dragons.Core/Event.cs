using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dragons.Core
{
    [BsonIgnoreExtraElements]
    public class Event
    {
        public Event()
        {
            Pieces = new List<Piece>();
        }

        [BsonElement]
        public Guid PlayerId { get; set; }

        [BsonElement]
        [JsonConverter(typeof(StringEnumConverter))]
        public EventType Type { get; set; }
        
        [BsonElement]
        public List<Piece> Pieces { get; set; }

        [BsonElement]
        public int Mana { get; set; }
    }
}
