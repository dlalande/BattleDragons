using System.ComponentModel.DataAnnotations;

namespace Dragons.Core.Models
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
        public Player Player1 { get; set; }

        /// <summary>
        /// Details for player 2.
        /// </summary>
        [Required]
        public Player Player2 { get; set; }

        /// <summary>
        /// Initial setup for player 1.
        /// </summary>
        public InitialSetup Player1Setup { get; set; }

        /// <summary>
        /// Initial setup for player 2.
        /// </summary>
        public InitialSetup Player2Setup { get; set; }
    }
}
