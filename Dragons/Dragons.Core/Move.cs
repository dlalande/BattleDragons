using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core
{
    [BsonIgnoreExtraElements]
    public class Move
    {
        [BsonElement]
        public Guid PlayerId { get; set; }

        [BsonElement]
        public Coordinate Coordinate { get; set; }

        [BsonElement]
        public Spell Spell { get; set; }

        public void Execute(GameState gameState)
        {
            if (gameState.Moves.Any() && gameState.Moves.Peek().PlayerId.Equals(PlayerId))
                throw new Exception("Player already moved.");

            gameState.Moves.Push(this);
            var player = gameState.Player1.PlayerId.Equals(this.PlayerId) ? gameState.Player1 : gameState.Player2;
            var opponent = gameState.Player1.PlayerId.Equals(this.PlayerId) ? gameState.Player2 : gameState.Player1;

            if (player.Mana < Spell.ManaCost)
                throw new Exception("Not enough mana to cast spell.");

            player.Mana -= Spell.ManaCost;
            gameState.Events.Add(new Event { PlayerId = player.PlayerId, Type = EventType.ManaUpdated, Mana = -1 * Spell.ManaCost });

            var attackEvent = new Event
            {
                PlayerId = opponent.PlayerId,
                Type = EventType.Attacked
            };

            switch (Spell.Type)
            {
                case SpellType.Meditate:
                    attackEvent = null;
                    break;
                case SpellType.Lightning:
                    attackEvent.Pieces.Add(opponent.Board[Coordinate.X][Coordinate.Y]);
                    break;
                case SpellType.FireBall:
                    AttackSquare(Coordinate, attackEvent, opponent);
                    break;
                case SpellType.FireStorm:
                    for (var row = 0; row < opponent.Board.Count; row++)
                        attackEvent.Pieces.Add(opponent.Board[Coordinate.X][row]);
                    break;
                case SpellType.IceStrike:
                    for (var column = 0; column < opponent.Board.Count; column++)
                        attackEvent.Pieces.Add(opponent.Board[column][Coordinate.Y]);
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
                    CheckForMana(attackEventPiece, gameState);
                }
                gameState.Events.Add(attackEvent);
            }

            for (var index = opponent.Dragons.Count - 1; index < 0; index--)
            {
                var dragon = opponent.Dragons[index];
                if (!dragon.All(piece => piece.HasBeenAttacked))
                    continue;
                gameState.Events.Add(new Event { PlayerId = opponent.PlayerId, Type = EventType.DragonKilled, Pieces = dragon });
                opponent.Dragons.RemoveAt(index);
            }

            if (opponent.Dragons.Count == 0)
            {
                gameState.Events.Add(new Event { PlayerId = PlayerId, Type = EventType.GameWon });
                return;
            }

            // Add mana to next player.
            gameState.Events.Add(new Event { PlayerId = opponent.PlayerId, Type = EventType.ManaUpdated, Mana = Constants.DefaultManaIncrement });
        }

        private void AttackSquare(Coordinate coordinate, Event attackEvent, Player opponent)
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

        private void CheckForMana(Piece attackedPiece, GameState gameState)
        {
            if (attackedPiece.Type == PieceType.LargeMana || attackedPiece.Type == PieceType.SmallMana)
            {
                gameState.Events.Add(new Event
                {
                    PlayerId = PlayerId,
                    Type = EventType.ManaUpdated,
                    Mana = attackedPiece.Type == PieceType.LargeMana ? Constants.LargeManaValue : Constants.SmallManaValue
                });
            }
        }
    }
}
