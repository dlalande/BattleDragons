using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core
{
    [BsonIgnoreExtraElements]
    public class Reservation
    {
        public Reservation()
        {
            Created = DateTime.UtcNow;
        }

        [BsonId]
        public string PlayerId { get; set; }

        [BsonElement]
        public string Name { get; set; }
        
        [BsonElement()]
        //[JsonProperty( propertyName:"created")]
        public DateTime Created { get; set; }
    }
}
