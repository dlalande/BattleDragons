using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dragons.Core
{
    /// <summary>
    /// Represents the initial setup of a player's game board.
    /// </summary>
    public class InitialSetup
    {
        /// <summary>
        /// Size of the square game board.
        /// </summary>
        [Required]
        public int BoardSize { get; set; }

        /// <summary>
        /// List of the starting positions for the player's dragons.
        /// </summary>
        [Required]
        public List<Dragon> Dragons { get; set; }
        
        /// <summary>
        /// List of addition pieces on the board. (Currently only mana)
        /// </summary>
        [Required]
        public List<Piece> AdditionalPieces { get; set; }
    }
}
