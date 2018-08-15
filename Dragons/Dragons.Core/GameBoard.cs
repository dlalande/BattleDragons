using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core
{
    [BsonIgnoreExtraElements]
    public class GameBoard : List<List<Piece>>
    {
        public GameBoard()
        { }
  

        public GameBoard(InitialSetup setup)
        {
            
            for (var x = 0; x < setup.BoardSize; x++)
            {
                Add(new List<Piece>());
                for (var y = 0; y < setup.BoardSize; y++)
                {
                    this[x].Add(new Piece());
                }
            }

            foreach (var dragon in setup.Dragons)
                foreach (var piece in dragon)
                    this[piece.Coordinate.X][piece.Coordinate.Y] = piece;

            foreach (var additionalPiece in setup.AdditionalPieces)
                this[additionalPiece.Coordinate.X][additionalPiece.Coordinate.Y] = additionalPiece;
        }
    }
}
