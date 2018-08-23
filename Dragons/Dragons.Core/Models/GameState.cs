using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Dragons.Core.MoveStrategies;
using Dragons.Core.Types;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core.Models
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
        public PlayerState Player1State { get; set; }

        /// <summary>
        /// Player 1 of the game.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public PlayerState Player2State { get; set; }
        
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
        /// Indicates whether the game is over. (probably not needed)
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public bool IsOver { get; set; }

        /// <summary>
        /// Converts the given game state into a <see cref="Game">game</see> from the given player's perspective.
        /// </summary>
        /// <param name="playerState">State of player whose perspective the converted game if from.</param>
        /// <returns>Returns a <see cref="Game">game</see> from the given player's perspective.</returns>
        public Game ToGame(PlayerState playerState)
        {
            var game = new Game
            {
                Name = playerState.Player.Name,
                Board = playerState.Board,
                Mana = playerState.Mana,
                Opponent = !Player1State.Player.Equals(playerState.Player) ? Player1State.Player.Name : Player2State.Player.Name,
                CanMove = CanMove(playerState.Player),
                IsOver = IsOver,
                Spells = Constants.AllSpells.ToList()
            };
            return game;
        }

        private bool CanMove(Player player)
        {
            if (!Moves.Any())  
                return Player1State.Player.Equals(player); //player 1 gets the first move.
            return !Moves.Peek().Player.Equals(player);
        }

        /// <summary>
        /// Returns the next move for a player based on player type.
        /// </summary>
        /// <param name="playerId">Id of player.</param>
        /// <returns>Returns the next move for a player based on player type.</returns>
        public Move GetNextMove(string playerId)
        {
            var playerState = Player1State.Player.PlayerId.Equals(playerId) ? Player1State : Player2State;
            var moveStrategy = MoveStrategyFactory.GetStrategy(playerState, this);
            return moveStrategy.GetNextMove();
        }

        /// <summary>
        /// Processes a given move, generating any resulting events.
        /// </summary>
        /// <param name="move">Move to process.</param>
        public void ProcessMove(Move move)
        {
             if(!CanMove(move.Player))
                throw new Exception("It is not this player's turn.");

            Moves.Push(move);
            var player = Player1State.Player.Equals(move.Player) ? Player1State : Player2State;
            var opponent = Player1State.Player.Equals(move.Player) ? Player2State : Player1State;

            if (player.Player.Type != PlayerType.Voldamort && player.Mana < move.Spell.ManaCost)
                throw new Exception("Not enough mana to cast spell.");

            if(player.Player.Type != PlayerType.Voldamort)
                player.Mana -= move.Spell.ManaCost;

            Events.Add(new Event { Player = player.Player, Type = EventType.ManaUpdated, Mana = -1 * move.Spell.ManaCost });

            var attackEvent = new Event
            {
                Player = opponent.Player,
                Type = EventType.Attacked,
                Spell = move.Spell
            };

            switch (move.Spell.Type)
            {
                case SpellType.Meditate:
                    attackEvent = null;
                    break;
                case SpellType.Lightning:
                    attackEvent.Pieces.Add(opponent.Board.Pieces[move.Coordinate.X][move.Coordinate.Y]);
                    break;
                case SpellType.FireBall:
                    AttackSquare(move.Coordinate, attackEvent, opponent);
                    break;
                case SpellType.FireStorm:
                    for (var row = 0; row < opponent.Board.Pieces.Count; row++)
                        attackEvent.Pieces.Add(opponent.Board.Pieces[move.Coordinate.X][row]);
                    break;
                case SpellType.IceStrike:
                    for (var column = 0; column < opponent.Board.Pieces.Count; column++)
                        attackEvent.Pieces.Add(opponent.Board.Pieces[column][move.Coordinate.Y]);
                    break;
                case SpellType.DragonFury:
                    foreach (var dragon in player.Board.Dragons)
                        AttackSquare(Coordinate.Random(opponent.Board.Pieces.Count), attackEvent, opponent);
                    break;
                case SpellType.AvadaKedavra:
                    var dragonToKill = opponent.Board.Dragons.Where(dragon => !dragon.IsDead).ToList().AsReadOnly().Random();
                    attackEvent.Pieces.AddRange(dragonToKill);
                    break;
            }

            if (attackEvent != null)
            {
                Events.Add(attackEvent);
                foreach (var attackEventPiece in attackEvent.Pieces)
                {
                    if(attackEventPiece.HasBeenAttacked)
                        continue;

                    attackEventPiece.HasBeenAttacked = true;
                    CheckForMana(attackEventPiece, move.Player);
                    CheckForDeadDragon(attackEventPiece, opponent);
                }

                if (opponent.Board.Dragons.All(dragon => dragon.IsDead))
                {
                    Events.Add(new Event { Player = move.Player, Type = EventType.GameWon });
                    IsOver = true;
                    return;
                }
            }
            opponent.Mana += Constants.DefaultManaIncrement;
            // Add mana to next player.
            Events.Add(new Event { Player = opponent.Player, Type = EventType.ManaUpdated, Mana = Constants.DefaultManaIncrement });

            //if (opponent.Player.IsComputerPlayer())
            //    ProcessMove(GetNextMove(opponent.Player.PlayerId));
        }

        private static void AttackSquare(Coordinate coordinate, Event attackEvent, PlayerState opponent)
        {
            var xDirection = coordinate.X == opponent.Board.Pieces.Count - 1 ? -1 : 1;
            var yDirection = coordinate.Y == opponent.Board.Pieces.Count - 1 ? -1 : 1;

            attackEvent.Pieces.AddRange(new []
            {
                opponent.Board.Pieces[coordinate.X][coordinate.Y],
                opponent.Board.Pieces[coordinate.X][coordinate.Y + yDirection],
                opponent.Board.Pieces[coordinate.X + xDirection][coordinate.Y],
                opponent.Board.Pieces[coordinate.X + xDirection][coordinate.Y + yDirection]
            });
        }

        private void CheckForMana(Piece attackedPiece, Player player)
        {
            if (attackedPiece.Type == PieceType.LargeMana || attackedPiece.Type == PieceType.SmallMana)
            {
                Events.Add(new Event
                {
                    Player = player,
                    Type = EventType.ManaUpdated,
                    Mana = attackedPiece.Type == PieceType.LargeMana ? Constants.LargeManaValue : Constants.SmallManaValue
                });
            }
        }

        private void CheckForDeadDragon(Piece attackedPiece, PlayerState opponent)
        {
            if (attackedPiece.IsDragonPiece())
            {
                foreach (var dragon in opponent.Board.Dragons)
                    if(dragon.IsDead && dragon.Contains(attackedPiece))
                        Events.Add(new Event { Player = opponent.Player, Type = EventType.DragonKilled, Pieces = dragon });
            }
        }
    }
}
