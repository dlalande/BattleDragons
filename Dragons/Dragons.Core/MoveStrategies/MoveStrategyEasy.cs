using System.Linq;
using Dragons.Core.Models;

namespace Dragons.Core.MoveStrategies
{
    /// <summary>
    /// Represents an easy strategy for generating a new move.
    /// </summary>
    public class MoveStrategyEasy : MoveStrategy, IMoveStrategy
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="playerState">Player state to generate move for.</param>
        /// <param name="gameState">Current state of the game.</param>
        public MoveStrategyEasy(PlayerState playerState, GameState gameState) :
            base(playerState, gameState)
        { }

        /// <summary>
        /// Generates a new completely random move.
        /// </summary>
        /// <returns>Generates a new completely random move.</returns>
        public override Move GetNextMove()
        {
            return new Move
            {
                Player = PlayerState.Player,
                Coordinate = Coordinate.Random(GameState.Player1State.Board.InitialSetup.BoardSize),
                Spell = Constants.AllSpells.Costing(PlayerState.Mana).Random()
            };
            
        }
    }
}
