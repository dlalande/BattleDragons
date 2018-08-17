using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core
{
    /// <summary>
    /// Represents all the game pieces that make of a game board.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Dragon : List<Piece>
    {

    }
}
