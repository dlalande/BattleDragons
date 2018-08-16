using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core
{
    [BsonIgnoreExtraElements]
    public class Coordinate
    {
        [BsonElement]
        public int X { get; set; }

        [BsonElement]
        public int Y { get; set; }

        public static Coordinate Random(int size)
        {
            return new Coordinate() {X = new Random().Next(0, size - 1), Y = new Random().Next(0, size - 1)};
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}
