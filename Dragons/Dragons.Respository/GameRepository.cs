using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Authentication;
using System.Threading.Tasks;
using Dragons.Core;
using Dragons.Core.Models;
using MongoDB.Driver;
using NLog;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Dragons.Respository
{
    public class GameRepository : IGameRepository
    { 
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private IMongoCollection<Reservation> _reservationCollection;
        private IMongoCollection<GameState> _gameStateCollection;
        private IReadOnlyList<InitialSetup> _initialSetups;

        public async Task InitializeAsync(GameRepositorySettings settings)
        {
            var mongoClientSettings = MongoClientSettings.FromConnectionString(settings.ConnectionString); 
            mongoClientSettings.SslSettings = new SslSettings() {EnabledSslProtocols = SslProtocols.Tls12};
            mongoClientSettings.ClusterConfigurator = builder =>
            {
                //builder.Subscribe(new SingleEventSubscriber<CommandSucceededEvent>(CommandSucceededEventHandler));
                //builder.Subscribe(new SingleEventSubscriber<CommandStartedEvent>(CommandStartedEventHandler));

            };
            mongoClientSettings.ConnectTimeout = TimeSpan.FromSeconds(5);
            mongoClientSettings.ServerSelectionTimeout = TimeSpan.FromSeconds(5);
            var client = new MongoClient(mongoClientSettings);
            var db = client.GetDatabase(settings.Database);
            
            _reservationCollection = db.GetCollection<Reservation>(Constants.ReservationCollection);
            await _reservationCollection.Indexes.CreateOneAsync(
                new CreateIndexModel<Reservation>(Builders<Reservation>.IndexKeys.Ascending(reservation => reservation.Player.PlayerId)));

            _gameStateCollection = db.GetCollection<GameState>(Constants.GameStateCollection);
            await _gameStateCollection.Indexes.CreateManyAsync(new[]
            {
                new CreateIndexModel<GameState>(Builders<GameState>.IndexKeys.Ascending(state => state.Player1State.Player.PlayerId)),
                new CreateIndexModel<GameState>(Builders<GameState>.IndexKeys.Ascending(state => state.Player2State.Player.PlayerId))
            });

            if (!Directory.Exists(settings.InitialSetupsFolderPath))
                throw new DirectoryNotFoundException($"Folder {settings.InitialSetupsFolderPath} does not exist.");

            var initialSetups = new List<InitialSetup>();
            foreach (var layoutFilePath in Directory.EnumerateFiles(settings.InitialSetupsFolderPath, "*.json"))
            {
                try
                {
                    initialSetups.Add(JsonConvert.DeserializeObject<InitialSetup>(File.ReadAllText(layoutFilePath)));
                }
                catch (Exception e)
                {
                    Logger.Warn(e);
                }
            }
            _initialSetups = initialSetups.AsReadOnly();
        }

        public async Task<Reservation> InsertReservationAsync(Reservation reservation)
        {
            await _reservationCollection.InsertOneAsync(reservation);
            return reservation;
        }

        public async Task<GameState> GetGameStateAsync(string playerId)
        {
            var filter = Builders<GameState>.Filter.Where(state => state.Player1State.Player.PlayerId.Equals(playerId) || state.Player2State.Player.PlayerId.Equals(playerId));
            return await _gameStateCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<GameState> InsertGameStateAsync(GameState gameState)
        {
            await _gameStateCollection.InsertOneAsync(gameState);
            return gameState;
        }

        public async Task<GameState> UpdateGameStateAsync(GameState gameState)
        {
            var result = await _gameStateCollection.ReplaceOneAsync(state => state.Player1State.Player.PlayerId.Equals(gameState.Player1State.Player.PlayerId), gameState);
            if(!result.IsAcknowledged)
                throw new Exception("Error saving game state.");
            return gameState;
        }

        public Tuple<InitialSetup,InitialSetup> GetRandomInitialSetupPair()
        {
            return _initialSetups.RandomPair();
        }

        public InitialSetup GetRandomInitialSetup()
        {
            return _initialSetups.Random();
        }

        public Player GetRandomPlayer()
        {
            return new Player(Guid.NewGuid().ToString(), Constants.WizardNames.Random());
        }

        public Tuple<Player, Player> GetRandomPlayerPair()
        {
            var namePair = Constants.WizardNames.RandomPair();
            return new Tuple<Player, Player>(
                new Player(Guid.NewGuid().ToString(), namePair.Item1), 
                new Player(Guid.NewGuid().ToString(), namePair.Item2));
        }

        public async Task<List<Reservation>> GetReservationsAsync()
        {
            return await _reservationCollection.Find(FilterDefinition<Reservation>.Empty).ToListAsync();
        }

        public async Task DeleteReservationAsync(Reservation reservation)
        {
            var filter = Builders<Reservation>.Filter.Where(r => r.Player.PlayerId.Equals(reservation.Player.PlayerId));
            var result = await _reservationCollection.DeleteOneAsync(filter);
            if (!result.IsAcknowledged)
                throw new Exception("Error deleting reservation.");
        }
    }
}
