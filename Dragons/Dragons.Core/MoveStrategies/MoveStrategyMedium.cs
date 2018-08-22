using System.Linq;
using Dragons.Core.Models;
using Dragons.Core.Types;

namespace Dragons.Core.MoveStrategies
{
    /// <summary>
    /// Represents an medium hard strategy for generating a new move.
    /// </summary>
    public class MoveStrategyMedium : MoveStrategy, IMoveStrategy
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="playerState">Player state to generate move for.</param>
        /// <param name="gameState">Current state of the game.</param>
        public MoveStrategyMedium(PlayerState playerState, GameState gameState) :
            base(playerState, gameState)
        { }

        /// <summary>
        /// Generates a new random move without repeating itself.
        /// </summary>
        /// <returns>Generates a new random move without repeating itself.</returns>
        public override Move GetNextMove()
        {
            var attackEvents = GameState.Events.Where(e => !e.Player.Equals(PlayerState.Player) && e.Type == EventType.Attacked).ToList();
            var pieces = attackEvents.SelectMany(e => e.Pieces).Distinct().ToList();
            Coordinate coordinate;
            do
            {
                coordinate = Coordinate.Random(GameState.Player1State.Board.InitialSetup.BoardSize);
            } while (pieces.Any(p => p.Coordinate.Equals(coordinate)));

            return new Move
            {
                Player = PlayerState.Player,
                Coordinate = coordinate,
                Spell = Spell.AllSpells.Where(spell => spell.ManaCost <= PlayerState.Mana).ToList().Random()
            };

        }
    }
}
