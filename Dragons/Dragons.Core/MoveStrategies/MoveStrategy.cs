using Dragons.Core.Models;

namespace Dragons.Core.MoveStrategies
{
    /// <summary>
    /// Abstract base class for the move strategies.
    /// </summary>
    public abstract class MoveStrategy
    {
        /// <summary>
        /// Player to generate the move for.
        /// </summary>
        protected readonly PlayerState PlayerState;

        /// <summary>
        /// Current state of the game.
        /// </summary>
        protected readonly GameState GameState;
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="playerState">Player state to generate move for.</param>
        /// <param name="gameState">Current state of the game.</param>
        protected MoveStrategy(PlayerState playerState, GameState gameState)
        {
            PlayerState = playerState;
            GameState = gameState;
        }

        /// <summary>
        /// Returns a new move for the player based on the strategy.
        /// </summary>
        /// <returns>Returns a new move for the player based on the strategy.</returns>
        public abstract Move GetNextMove();
    }
}
