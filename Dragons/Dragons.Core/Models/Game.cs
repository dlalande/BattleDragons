using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dragons.Core.Models
{
    /// <summary>
    /// Represents the state of the game from a player's perspective.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Name of the player.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Name of the opponent player.
        /// </summary>
        [Required]
        public string Opponent { get; set; }
        
        /// <summary>
        /// Amount of mana (magic power) the player has.
        /// </summary>
        [Required]
        public int Mana { get; set; }

        /// <summary>
        /// Game board of the player.
        /// </summary>
        [Required]
        public GameBoard Board { get; set; }
        
        /// <summary>
        /// Spells available to the user to cast.
        /// </summary>
        [Required]
        public List<Spell> Spells { get; set; }

        /// <summary>
        /// Indicates whether the player can make a move.
        /// </summary>
        [Required]
        public bool CanMove { get; set; }

        /// <summary>
        /// Indicates whether the game is over. (probably not needed)
        /// </summary>
        [Required]
        public bool IsOver { get; set; }

        /// <summary>
        /// Returns pretty-printed string
        /// </summary>
        /// <returns>Returns pretty-printed string</returns>
        public override string ToString()
        {
            return $"{Name} vs. {Opponent}. {Environment.NewLine}{Board}";
        }
    }
}
