using System.Collections.Generic;

namespace Dragons.Common
{
    public class Constants
    {
        public const string Database = "BattleDragons";
        public const string LobbyCollection = "Lobby";
        public const string GamesCollection = "Games";
        public static readonly List<string> Names = new List<string> { "Gandolf", "Lavarus", "Ezekiel", "Magnus", "Mortius", "Cyrus", "Belzof", "Palpatine", "Morgoth" };

        public const int DefaultInitialMana = 10;
        public const int DefaultManaIncrement = 10;
        public const int DefaultGridSize = 14;
    }
}
