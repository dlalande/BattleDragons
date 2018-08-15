using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Dragons.Core
{
    [BsonIgnoreExtraElements]
    public class Reservation
    {
        [BsonId]
        public string PlayerId { get; set; }

        [BsonElement]
        public string Name { get; set; }
        
        [BsonElement()]
        //[JsonProperty( propertyName:"created")]
        public DateTime Created { get; set; }
    }
}
