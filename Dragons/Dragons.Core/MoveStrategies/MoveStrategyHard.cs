using System.Linq;
using Dragons.Core.Models;
using Dragons.Core.Types;

namespace Dragons.Core.MoveStrategies
{
    /// <summary>
    /// Represents an hard strategy for generating a new move.
    /// </summary>
    public class MoveStrategyHard : MoveStrategy, IMoveStrategy
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="playerState">Player state to generate move for.</param>
        /// <param name="gameState">Current state of the game.</param>
        public MoveStrategyHard(PlayerState playerState, GameState gameState) :
            base(playerState, gameState)
        { }

        /// <summary>
        /// Generates a new move using opponent's board to cheat and win.
        /// </summary>
        /// <returns>Generates a new move using opponent's board to cheat and win.</returns>
        public override Move GetNextMove()
        {
            var coordinate = Coordinate.Random(GameState.Player1State.Board.InitialSetup.BoardSize);
            var spell = Constants.AllSpells.First(s => s.Type == SpellType.Meditate);

            // As you run out of dragons, your attacks increase.
            if (Dice.Roll(100) >= (PlayerState.Board.AliveDragons.Count - 1 * 100) / Constants.DragonsPerPlayer)
                spell = Constants.AllSpells.Costing(PlayerState.Mana).Where(s => s.Type != SpellType.Meditate).ToList().Random();
    
            if (Dice.Roll(100) <= Constants.AttackDragonPercentage)
            {
                var opponentState = GameState.Player1State.Player.Equals(PlayerState.Player) ? GameState.Player2State : GameState.Player1State;
                coordinate = opponentState.Board.AliveDragons.First().First(p => !p.HasBeenAttacked).Coordinate;
                spell = Constants.AllSpells.Costing(PlayerState.Mana).OrderByDescending(s => s.ManaCost).First();
            }

            return new Move
            {
                Player = PlayerState.Player,
                Coordinate = coordinate,
                Spell = spell
            };

        }
    }
}
