using System.Collections.Generic;

namespace Dragons.Core
{
    /// <summary>
    /// Constants used in the code.
    /// </summary>
    public class Constants
    {
        #region Web Api

        /// <summary>
        /// Api key used for web api calls.
        /// </summary>
        public const string ApiKey = "0123456789";

        /// <summary>
        /// Header used to transmit the api key.
        /// </summary>
        public const string ApiKeyHeader = "X-ApiKey";

        /// <summary>
        /// Header used to transmit the client id.
        /// </summary>
        public const string ClientIdHeader = "X-ClientId";

        #endregion

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
        public static readonly IReadOnlyList<string> WizardNames = new List<string>
        {
            "Gandolf",
            "Merlin",
            "Lavarus",
            "Ezekiel",
            "Magnus",
            "Mortius",
            "Cyrus",
            "Belzof",
            "Palpatine",
            "Morgoth"
        }.AsReadOnly();

        /// <summary>
        /// List of wizard names.
        /// </summary>
        public const int DefaultInitialMana = 1000000;

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

        /// <summary>
        /// Percentage chance your dragon will be attacked.
        /// </summary>
        public const int AttackDragonPercentage = 35;

        /// <summary>
        /// Percentage chance you cast Meditate spell.
        /// </summary>
        public const int MeditatePercentage = 50;

        #endregion
    }
}
