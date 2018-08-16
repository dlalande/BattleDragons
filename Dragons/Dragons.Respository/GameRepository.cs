using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dragons.Core;
using MongoDB.Driver;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Dragons.Respository
{
    public class GameRepository : IGameRepository
    { 
        private IMongoCollection<Reservation> _reservationCollection;
        private IMongoCollection<GameState> _gameStateCollection;
        private IReadOnlyList<InitialSetup> _initialSetups;

        public async Task InitializeAsync(GameRepositorySettings settings)
        {
            var mongoClientSettings = MongoClientSettings.FromConnectionString($"mongodb://{settings.Host}:{settings.Port}/{settings.Database}");
            mongoClientSettings.ClusterConfigurator = builder =>
            {
                //builder.Subscribe(new SingleEventSubscriber<CommandSucceededEvent>(CommandSucceededEventHandler));
                //builder.Subscribe(new SingleEventSubscriber<CommandStartedEvent>(CommandStartedEventHandler));

            };
            var client = new MongoClient(mongoClientSettings);
            var db = client.GetDatabase(Constants.DefaultDatabase);
            
            _reservationCollection = db.GetCollection<Reservation>(Constants.ReservationCollection);
            _gameStateCollection = db.GetCollection<GameState>(Constants.GameStateCollection);
            var indexes = await _gameStateCollection.Indexes.CreateManyAsync(new[]
            {
                new CreateIndexModel<GameState>(Builders<GameState>.IndexKeys.Ascending(state => state.Player1.PlayerId)),
                new CreateIndexModel<GameState>(Builders<GameState>.IndexKeys.Ascending(state => state.Player2.PlayerId))
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
                    //Log warning.
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
            var filter = Builders<GameState>.Filter.Where(state => state.Player1.PlayerId.Equals(playerId) || state.Player2.PlayerId.Equals(playerId));
            return await _gameStateCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<GameState> InsertGameStateAsync(GameState gameState)
        {
            await _gameStateCollection.InsertOneAsync(gameState);
            return gameState;
        }

        public async Task<GameState> UpdateGameStateAsync(GameState gameState)
        {
            var result = await _gameStateCollection.ReplaceOneAsync(state => state.Player1.PlayerId.Equals(gameState.Player1.PlayerId), gameState);
            if(!result.IsAcknowledged)
                throw new Exception("Problem with update.");
            return gameState;
        }

        public async Task<Tuple<InitialSetup,InitialSetup>> GetRandomInitialSetupsAsync()
        {
            var random = new Random();
            var player1Index = random.Next(_initialSetups.Count);
            int player2Index;
            do
            {
                player2Index = random.Next(_initialSetups.Count);
            } while (player1Index == player2Index);
            return new Tuple<InitialSetup, InitialSetup>(_initialSetups[player1Index], _initialSetups[player2Index]);
        }

        public async Task<List<Reservation>> GetReservationsAsync()
        {
            return await _reservationCollection.Find(FilterDefinition<Reservation>.Empty).ToListAsync();
        }

        public async Task DeleteReservationAsync(Reservation reservation)
        {
            var filter = Builders<Reservation>.Filter.Where(r => r.PlayerId.Equals(reservation.PlayerId));
            var result = await _reservationCollection.DeleteOneAsync(filter);
            if (!result.IsAcknowledged)
                throw new Exception("Problem with delete.");
        }
    }
}
