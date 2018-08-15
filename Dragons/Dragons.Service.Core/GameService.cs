using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Dragons.Core;
using Dragons.Respository;

namespace Dragons.Service.Core
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repo;

        public GameService()
        {
            var host = ConfigurationManager.AppSettings["host"] ?? Constants.DefaultHost;
            var port = int.TryParse(ConfigurationManager.AppSettings["port"], out var portSetting) ? portSetting : Constants.DefaultPort;
            var database = ConfigurationManager.AppSettings["database"] ?? Constants.DefaultDatabase;
            _repo = new GameRepository(host, port, database);
        }

        public async Task<Game> GetGameAsync(string playerId)
        {
            var gameState = await _repo.GetGameStateAsync(playerId);
            return gameState?.ToGame(playerId);
        }

        public async Task<List<Event>> GetGameEventsAsync(string playerId, int offset = 0)
        {
            var gameState = await _repo.GetGameStateAsync(playerId);
            return gameState?.Events.Where((e, index) => index > offset - 1).ToList();
        }

        public async Task<List<Reservation>> GetReservationsAsync()
        {
            return await _repo.GetReservationsAsync();
        }

        public async Task<Move> InsertGameMoveAsync(Move move)
        {
            var gameState = await _repo.GetGameStateAsync(move.PlayerId);
            move.Execute(gameState);
            await _repo.UpdateGameStateAsync(gameState);
            return move;
        }

        public async Task InsertGameStartAsync(GameStart start)
        {
            var initialSetups = await _repo.GetRandomInitialSetupsAsync();

            var gameState = new GameState()
            {
                Player1 = new Player()
                {
                    PlayerId = start.Player1.PlayerId,
                    Name = start.Player1.Name,
                    Mana = Constants.DefaultInitialMana,
                    Type = PlayerType.Human,
                    Board = new GameBoard(initialSetups.Item1),
                    Dragons = initialSetups.Item1.Dragons
                },
                Player2 = new Player()
                {
                    PlayerId = start.Player2.PlayerId,
                    Name = start.Player2.Name,
                    Mana = Constants.DefaultInitialMana,
                    Type = PlayerType.Human,
                    Board = new GameBoard(initialSetups.Item2),
                    Dragons = initialSetups.Item2.Dragons
                },
                Events = new List<Event>
                {
                    new Event()
                    {
                        PlayerId = start.Player1.PlayerId,
                        Type = EventType.GameStarted
                    }
                },
                Created = DateTime.UtcNow
            };
            await _repo.InsertGameStateAsync(gameState);
            //delete reservation
        }

        public async Task<Reservation> InsertReservationAsync(Reservation reservation)
        {
            return await _repo.InsertReservationAsync(reservation);
        }
    }
}
