namespace Dragons.Core
{
    /// <summary>
    /// Types of spells to cast in the game.
    /// </summary>
    public enum SpellType
    {
        /// <summary>
        /// The player rests for a turn without costing any mana.
        /// </summary>
        Meditate,

        /// <summary>
        /// Attack a single coordinate.
        /// </summary>
        Lightning,

        /// <summary>
        /// Attack a 2x2 square of coordinates.
        /// </summary>
        FireBall,

        /// <summary>
        /// Attack an entire column of the game board.
        /// </summary>
        FireStorm,

        /// <summary>
        /// Attack an entire row of the game board.
        /// </summary>
        IceStrike,

        /// <summary>
        /// Attacks with a fireball for each dragon you still have alive.
        /// </summary>
        DragonFury
    }
}
