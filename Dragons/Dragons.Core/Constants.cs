using Dragons.Core.Models;
using Dragons.Core.Types;
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
        /// Valid api key
        /// </summary>
        public const string ValidApiKey = "0123456789";

        /// <summary>
        /// Name of configuration app setting containing valid api keys.
        /// </summary>
        public const string ValidApiKeysSettingName = "ValidApiKeys";

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

        /// <summary>
        /// Percentage chance your dragon will be attacked.
        /// </summary>
        public const int AttackDragonPercentage = 35;

        /// <summary>
        /// Percentage chance you cast Meditate spell.
        /// </summary>
        public const int MeditatePercentage = 50;

        /// <summary>
        /// The fixed number of dragons for each player's board.
        /// </summary>
        public const int DragonsPerPlayer = 5;

        /// <summary>
        /// List of all the spells in the game.
        /// </summary>
        /// <returns></returns>
        public static readonly IReadOnlyList<Spell> AllSpells = new List<Spell>()
        {
            new Spell()
            {
                Type = SpellType.Meditate,
                Description = "Quietly mediate to restore Mana.",
                ManaCost = 0
            },
            new Spell()
            {
                Type = SpellType.Lightning,
                Description = "Strike a single cell with your standard lightning attack.",
                ManaCost = 5
            },
            new Spell()
            {
                Type = SpellType.FireBall,
                Description = "Singe a 2x2 region with an explosive charge.",
                ManaCost =  20
            },
            new Spell()
            {
                Type = SpellType.FireStorm,
                Description = "Attacks across the entire column of your choice.",
                ManaCost =  35
            },
            new Spell()
            {
                Type = SpellType.IceStrike,
                Description = "Attacks across the entire row of your choice.",
                ManaCost =  35
            },
            new Spell()
            {
                Type = SpellType.DragonFury,
                Description = "Remaining alive dragons each lay waste to one randomly chosen 2x2 region.",
                ManaCost =  60
            },
            new Spell()
            {
                Type = SpellType.AvadaKedavra,
                Description = "Instantly kills one dragon at random.",
                ManaCost =  150
            }
        }.AsReadOnly();

        #endregion
    }
}
