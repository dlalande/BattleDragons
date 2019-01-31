namespace Dragons.Core.Types
{
    /// <summary>
    /// Type of events that occur in the game.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Occurs when the game is started.
        /// </summary>
        GameStarted,
        
        /// <summary>
        /// Occurs when the user attacks a group of pieces
        /// </summary>
        Attacked,
        
        /// <summary>
        /// Occurs when an attack resulted in a dragon being killed.
        /// </summary>
        DragonKilled,
        
        /// <summary>
        /// Occurs when the mana for a player is added or removed.
        /// </summary>
        ManaUpdated,

        /// <summary>
        /// Occurs when one of the players kills all of the opponent's dragons.
        /// </summary>
        GameWon
    }
}
