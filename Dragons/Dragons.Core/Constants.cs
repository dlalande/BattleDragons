using System.Collections.Generic;

namespace Dragons.Core
{
    public class Constants
    {
        #region MongoDB Constants

        /// <summary>
        /// Default host for mongodb server
        /// </summary>
        public const string DefaultHost = "localhost";

        /// <summary>
        /// Default port for mongodb server
        /// </summary>
        public const int DefaultPort = 27017;

        /// <summary>
        /// Default database for mongodb server
        /// </summary>
        public const string DefaultDatabase = "Dragons";

        /// <summary>
        /// Collection used to store player game reservations.
        /// </summary>
        public const string ReservationCollection = "Reservations";

        /// <summary>
        /// Collection used to store game states.
        /// </summary>
        public const string GameStateCollection = "GameStates";

        #endregion

        #region Game Constants

        /// <summary>
        /// List of wizard names.
        /// </summary>
        public static readonly List<string> WizardNames = new List<string> { "Gandolf", "Lavarus", "Ezekiel", "Magnus", "Mortius", "Cyrus", "Belzof", "Palpatine", "Morgoth" };

        /// <summary>
        /// List of wizard names.
        /// </summary>
        public const int DefaultInitialMana = 10;

        /// <summary>
        /// List of wizard names.
        /// </summary>
        public const int DefaultManaIncrement = 10;

        /// <summary>
        /// List of wizard names.
        /// </summary>
        public const int LargeManaValue = 50;

        /// <summary>
        /// List of wizard names.
        /// </summary>
        public const int SmallManaValue = 20;

        #endregion
    }
}
