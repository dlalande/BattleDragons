using System.Linq;
using Dragons.Core.Models;
using Dragons.Core.Types;

namespace Dragons.Core.MoveStrategies
{
    /// <summary>
    /// Represents a wizard that always meditates.
    /// </summary>
    public class MoveStrategyVoldamort : MoveStrategy, IMoveStrategy
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="playerState">Player state to generate move for.</param>
        /// <param name="gameState">Current state of the game.</param>
        public MoveStrategyVoldamort(PlayerState playerState, GameState gameState) :
            base(playerState, gameState)
        { }

        /// <summary>
        /// Generates a new random move without repeating itself.
        /// </summary>
        /// <returns>Generates a new random move without repeating itself.</returns>
        public override Move GetNextMove()
        {
            return new Move()
            {
                Player = PlayerState.Player,
                Coordinate = Coordinate.Random(GameState.Player1State.Board.InitialSetup.BoardSize),
                Spell = Constants.AllSpells.First(spell => spell.Type == SpellType.AvadaKedavra)
            };
        }
    }
}
