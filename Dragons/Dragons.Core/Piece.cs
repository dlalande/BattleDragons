using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dragons.Core
{
    [BsonIgnoreExtraElements]
    public class Piece
    {
        public Piece()
        {
            Coordinate = new Coordinate();
            Type = PieceType.Map;
        }

        [BsonElement]
        public Coordinate Coordinate { get; set; }

        [BsonElement]
        [JsonConverter(typeof(StringEnumConverter))]
        public PieceType Type { get; set; }

        [BsonElement]
        public bool HasBeenAttacked { get;set; }

        [BsonElement]
        public int Orientation { get; set; }
    }
}
