using System.Configuration;
using Dragons.Core;

namespace Dragons.Respository
{
    public class GameRepositorySettings
    {
        public GameRepositorySettings()
        {
            Host = ConfigurationManager.AppSettings["host"] ?? Constants.DefaultHost;
            Port = int.TryParse(ConfigurationManager.AppSettings["port"], out var portSetting) ? portSetting : Constants.DefaultPort;
            Database = ConfigurationManager.AppSettings["database"] ?? Constants.DefaultDatabase;
        }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string InitialSetupsFolderPath { get; set; }
    }
}
