using Dragons.Core.Models;

namespace Dragons.Core.MoveStrategies
{
    /// <summary>
    /// Represents the strategy for generating a new move.
    /// </summary>
    public interface IMoveStrategy
    {
        /// <summary>
        /// Returns the next move for the given strategy.
        /// </summary>
        /// <returns>Returns the next move for the given strategy.</returns>
        Move GetNextMove();
    }
}
