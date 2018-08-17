using System.ComponentModel.DataAnnotations;

namespace Dragons.Core
{
    /// <summary>
    /// Represents the initial information needed to start a new game.
    /// </summary>
    public class GameStart
    {
        /// <summary>
        /// Details for player 1.
        /// </summary>
        [Required]
        public PlayerDetails Player1 { get; set; }

        /// <summary>
        /// Details for player 2.
        /// </summary>
        [Required]
        public PlayerDetails Player2 { get; set; }
    }
}
