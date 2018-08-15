using System.Collections.Generic;

namespace Dragons.Core
{
    public class Constants
    {
        public const string DefaultHost = "localhost";
        public const int DefaultPort = 27017;
        public const string DefaultDatabase = "BattleDragons";

        public const string ReservationCollection = "Reservations";
        public const string GameStateCollection = "GameStates";
        public const string InitialSetupCollection = "InitialSetups";
        public static readonly List<string> Names = new List<string> { "Gandolf", "Lavarus", "Ezekiel", "Magnus", "Mortius", "Cyrus", "Belzof", "Palpatine", "Morgoth" };

        public const int DefaultInitialMana = 10;
        public const int DefaultManaIncrement = 10;

        public const int LargeManaValue = 50;
        public const int SmallManaValue = 20;
        //public const int DefaultGridSize = 14;
    }
}
