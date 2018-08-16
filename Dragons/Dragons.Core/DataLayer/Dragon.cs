using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core
{
    [BsonIgnoreExtraElements]
    public class Dragon : List<Piece>
    {

    }
}
