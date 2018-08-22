using System;
using Dragons.Core.Models;
using Dragons.Core.Types;

namespace Dragons.Core.MoveStrategies
{
    /// <summary>
    /// Represents a factory for move strategies.
    /// </summary>
    public static class MoveStrategyFactory
    {
         /// <summary>
         /// Returns a new strategy based on the given player.
         /// </summary>
         /// <param name="playerState">Player state to generate move for.</param>
         /// <param name="gameState">State of game.</param>
         /// <returns>Returns a new strategy based on the given player.</returns>
        public static IMoveStrategy GetStrategy(PlayerState playerState, GameState gameState)
        {
            switch (playerState.Player.Type)
            {
                case PlayerType.EasyComputer:
                    return new MoveStrategyEasy(playerState, gameState);
                case PlayerType.MediumComputer:
                    return new MoveStrategyMedium(playerState, gameState);
                case PlayerType.HardComputer:
                    return new MoveStrategyHard(playerState, gameState);
                default:
                    throw new Exception("Player is not a computer.");
            }
        }
    }
}
