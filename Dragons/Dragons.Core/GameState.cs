using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core
{
    [BsonIgnoreExtraElements]
    public class GameState
    {
        public GameState()
        {
            Events = new List<Event>();
            Moves = new Stack<Move>();
            Created = DateTime.UtcNow;
        }

        //[BsonId]
        //public ObjectId Id { get; set; }

        [BsonElement]
        public Player Player1 { get; set; }

        [BsonElement]
        public Player Player2 { get; set; }
        
        [BsonElement]
        public List<Event> Events { get; set; }

        [BsonElement]
        public Stack<Move> Moves { get; set; }
        
        [BsonElement]
        public DateTime Created { get; set; }

        public Game ToGame(string playerId)
        {
            var player = Player1.PlayerId.Equals(playerId) ? Player1 : Player2;
            var game = new Game
            {
                Name = player.Name,
                Board = player.Board,
                Mana = player.Mana,
                Opponent = !Player1.PlayerId.Equals(playerId) ? Player1.Name : Player2.Name,
                Spells = Spell.AllSpells()
            };
            return game;
        }
    }
}
