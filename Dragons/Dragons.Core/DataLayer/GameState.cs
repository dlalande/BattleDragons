using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core
{
    /// <summary>
    /// Represents the current state of a game.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class GameState
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public GameState()
        {
            Events = new List<Event>();
            Moves = new Stack<Move>();
            Created = DateTime.UtcNow;
        }

        /// <summary>
        /// Player 1 of the game.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public Player Player1 { get; set; }

        /// <summary>
        /// Player 1 of the game.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public Player Player2 { get; set; }
        
        /// <summary>
        /// List of events that occurred during game play.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public List<Event> Events { get; set; }

        /// <summary>
        /// Stack of moves that have been played during game play.  Last in, first out.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public Stack<Move> Moves { get; set; }
        
        /// <summary>
        /// Date and time in UTC the game state was created.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public DateTime Created { get; set; }

        /// <summary>
        /// Converts the given game state into a <see cref="Game">game</see> from the given player's perspective.
        /// </summary>
        /// <param name="playerId">Id of player whose perspective the converted game if from.</param>
        /// <returns>Returns a <see cref="Game">game</see> from the given player's perspective.</returns>
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

        /// <summary>
        /// Processes a given move, generating any resulting events.
        /// </summary>
        /// <param name="move">Move to process.</param>
        public void ProcessMove(Move move)
        {
             //TODO: Maybe move this to game state.

            if (Moves.Any() && Moves.Peek().PlayerId.Equals(move.PlayerId))
                throw new Exception("Player already moved.");

            Moves.Push(move);
            var player = Player1.PlayerId.Equals(move.PlayerId) ? Player1 : Player2;
            var opponent = Player1.PlayerId.Equals(move.PlayerId) ? Player2 : Player1;

            if (player.Mana < move.Spell.ManaCost)
                throw new Exception("Not enough mana to cast spell.");

            player.Mana -= move.Spell.ManaCost;
            Events.Add(new Event { PlayerId = player.PlayerId, Type = EventType.ManaUpdated, Mana = -1 * move.Spell.ManaCost });

            var attackEvent = new Event
            {
                PlayerId = opponent.PlayerId,
                Type = EventType.Attacked
            };

            switch (move.Spell.Type)
            {
                case SpellType.Meditate:
                    attackEvent = null;
                    break;
                case SpellType.Lightning:
                    attackEvent.Pieces.Add(opponent.Board[move.Coordinate.X][move.Coordinate.Y]);
                    break;
                case SpellType.FireBall:
                    AttackSquare(move.Coordinate, attackEvent, opponent);
                    break;
                case SpellType.FireStorm:
                    for (var row = 0; row < opponent.Board.Count; row++)
                        attackEvent.Pieces.Add(opponent.Board[move.Coordinate.X][row]);
                    break;
                case SpellType.IceStrike:
                    for (var column = 0; column < opponent.Board.Count; column++)
                        attackEvent.Pieces.Add(opponent.Board[column][move.Coordinate.Y]);
                    break;
                case SpellType.DragonFury:
                    foreach (var dragon in player.Dragons)
                        AttackSquare(Coordinate.Random(opponent.Board.Count), attackEvent, opponent);
                    break;
            }

            if (attackEvent != null)
            {
                foreach (var attackEventPiece in attackEvent.Pieces)
                {
                    attackEventPiece.HasBeenAttacked = true;
                    CheckForMana(attackEventPiece, move.PlayerId);
                }
                Events.Add(attackEvent);
            }

            for (var index = opponent.Dragons.Count - 1; index >= 0; index--)
            {
                var dragon = opponent.Dragons[index];
                if (!dragon.All(piece => piece.HasBeenAttacked))
                    continue;
                Events.Add(new Event { PlayerId = opponent.PlayerId, Type = EventType.DragonKilled, Pieces = dragon });
                opponent.Dragons.RemoveAt(index);
            }

            if (opponent.Dragons.Count == 0)
            {
                Events.Add(new Event { PlayerId = move.PlayerId, Type = EventType.GameWon });
                return;
            }

            // Add mana to next player.
            Events.Add(new Event { PlayerId = opponent.PlayerId, Type = EventType.ManaUpdated, Mana = Constants.DefaultManaIncrement });
        }

        private static void AttackSquare(Coordinate coordinate, Event attackEvent, Player opponent)
        {
            var xDirection = coordinate.X == opponent.Board.Count - 1 ? -1 : 1;
            var yDirection = coordinate.Y == opponent.Board.Count - 1 ? -1 : 1;

            attackEvent.Pieces = new List<Piece>
            {
                opponent.Board[coordinate.X][coordinate.Y],
                opponent.Board[coordinate.X][coordinate.Y + yDirection],
                opponent.Board[coordinate.X + xDirection][coordinate.Y],
                opponent.Board[coordinate.X + xDirection][coordinate.Y + yDirection]
            };
        }

        private void CheckForMana(Piece attackedPiece, string playerId)
        {
            if (attackedPiece.Type == PieceType.LargeMana || attackedPiece.Type == PieceType.SmallMana)
            {
                Events.Add(new Event
                {
                    PlayerId = playerId,
                    Type = EventType.ManaUpdated,
                    Mana = attackedPiece.Type == PieceType.LargeMana ? Constants.LargeManaValue : Constants.SmallManaValue
                });
            }
        }
    }
}
