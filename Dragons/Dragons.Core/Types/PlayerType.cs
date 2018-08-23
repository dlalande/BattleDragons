namespace Dragons.Core.Types
{
    /// <summary>
    /// Types of players in the game.
    /// </summary>
    public enum PlayerType
    {
        /// <summary>
        /// Player is a human.
        /// </summary>
        Human,

        /// <summary>
        /// Player always meditates.
        /// </summary>
        Sleeper,

        /// <summary>
        /// Player 
        /// </summary>
        Voldamort,

        /// <summary>
        /// Computer player that picks attacks at random (rare power moves)
        /// </summary>
        EasyComputer,

        /// <summary>
        /// Computer player that doesn't repeat moves.
        /// </summary>
        MediumComputer,

        /// <summary>
        /// Computer player who can use your board to cheat and win.
        /// </summary>
        HardComputer
    }
}
