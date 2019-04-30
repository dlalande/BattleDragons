using System.Configuration;
using Dragons.Core;

namespace Dragons.Respository
{
    public class GameRepositorySettings
    {
        public GameRepositorySettings()
        {
            Database = ConfigurationManager.AppSettings["database"] ?? Constants.DefaultDatabase;
            ConnectionString = ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                Host = ConfigurationManager.AppSettings["host"] ?? Constants.DefaultHost;
                Port = int.TryParse(ConfigurationManager.AppSettings["port"], out var portSetting) ? portSetting : Constants.DefaultPort;
                ConnectionString = $"mongodb://{Host}:{Port}/{Database}";
            }
        }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string ConnectionString { get; set; }
        public string InitialSetupsFolderPath { get; set; }
    }
}
