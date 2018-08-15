using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dragons.Core;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dragons.Respository
{
    public class GameRepository : IGameRepository
    { 
        private readonly IMongoCollection<Reservation> _reservationCollection;
        private readonly IMongoCollection<GameState> _gameStateCollection;

        public GameRepository(string host, int port, string database)
        {
            var settings = MongoClientSettings.FromConnectionString($"mongodb://{host}:{port}/{database}");
            settings.ClusterConfigurator = builder =>
            {
                //builder.Subscribe(new SingleEventSubscriber<CommandSucceededEvent>(CommandSucceededEventHandler));
                //builder.Subscribe(new SingleEventSubscriber<CommandStartedEvent>(CommandStartedEventHandler));

            };
            var client = new MongoClient(settings);
            var db = client.GetDatabase(Constants.DefaultDatabase);
            _reservationCollection = db.GetCollection<Reservation>(Constants.ReservationCollection);
            _gameStateCollection = db.GetCollection<GameState>(Constants.GameStateCollection);
            var s = _gameStateCollection.Indexes.CreateMany(new[]
            {
                new CreateIndexModel<GameState>(Builders<GameState>.IndexKeys.Ascending(state => state.Player1.PlayerId)),
                new CreateIndexModel<GameState>(Builders<GameState>.IndexKeys.Ascending(state => state.Player2.PlayerId))
            });
        }

        public async Task<Reservation> InsertReservationAsync(Reservation reservation)
        {
            await _reservationCollection.InsertOneAsync(reservation);
            return reservation;
        }

        public async Task<GameState> GetGameStateAsync(Guid playerId)
        {
            return await _gameStateCollection.Find(state => state.Player1.PlayerId.Equals(playerId) || state.Player2.PlayerId.Equals(playerId)).SingleAsync();
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
            var setups = InitialSetup.AllSetups();
            var random = new Random();
            var player1Index = random.Next(setups.Count);
            int player2Index;
            do
            {
                player2Index = random.Next(setups.Count);
            } while (player1Index == player2Index);
            return new Tuple<InitialSetup, InitialSetup>(setups[player1Index], setups[player2Index]);
        }

        public async Task<List<Reservation>> GetReservationsAsync()
        {
            return await _reservationCollection.Find(FilterDefinition<Reservation>.Empty).ToListAsync();
        }
    }
}
