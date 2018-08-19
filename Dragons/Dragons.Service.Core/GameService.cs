using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dragons.Core;
using Dragons.Respository;

namespace Dragons.Service.Core
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repo= new GameRepository();

        public async Task InitializeAsync(string folderPath)
        {
            await _repo.InitializeAsync(new GameRepositorySettings {InitialSetupsFolderPath = folderPath});
        }

        public Player GetRandomPlayer()
        {
            return _repo.GetRandomPlayer();
        }

        public Tuple<Player,Player> GetRandomPlayerPair()
        {
            return _repo.GetRandomPlayerPair();
        }

        public Move GetRandomMove(int size)
        {
            return new Move
            {
                PlayerId = null,
                Coordinate = Coordinate.Random(size),
                Spell = Spell.AllSpells.Random()
            };
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
            if (string.IsNullOrWhiteSpace(move.PlayerId))
                throw new ArgumentException("PlayerId must not be empty.", nameof(move));
            var gameState = await _repo.GetGameStateAsync(move.PlayerId);
            gameState.ProcessMove(move);
            await _repo.UpdateGameStateAsync(gameState);
            return move;
        }

        public async Task InsertGameStartAsync(GameStart start)
        {
            var player1Setup = start.Player1Setup;
            var player2Setup = start.Player1Setup;

            if (player1Setup == null && player1Setup == null)
            {
                var initialSetupPair = _repo.GetRandomInitialSetupPair();
                player1Setup = initialSetupPair.Item1;
                player2Setup = initialSetupPair.Item2;
            }

            player1Setup = player1Setup ?? _repo.GetRandomInitialSetup();
            player2Setup = player2Setup ?? _repo.GetRandomInitialSetup();

            var gameState = new GameState()
            {
                Player1 = new PlayerState
                {
                    PlayerId = start.Player1.PlayerId,
                    Name = start.Player1.Name,
                    Mana = Constants.DefaultInitialMana,
                    Type = PlayerType.Human,
                    Board = new GameBoard(player1Setup),
                    Dragons = player1Setup.Dragons
                },
                Player2 = new PlayerState
                {
                    PlayerId = start.Player2.PlayerId,
                    Name = start.Player2.Name,
                    Mana = Constants.DefaultInitialMana,
                    Type = PlayerType.Human,
                    Board = new GameBoard(player2Setup),
                    Dragons = player2Setup.Dragons
                },
                Events = new List<Event>
                {
                    new Event()
                    {
                        PlayerId = start.Player1.PlayerId,
                        Type = EventType.GameStarted
                    }
                }
            };
            await _repo.InsertGameStateAsync(gameState);
            await _repo.DeleteReservationAsync(new Reservation {Player = start.Player1});
        }

        public async Task<Reservation> InsertReservationAsync(Reservation reservation)
        {
            return await _repo.InsertReservationAsync(reservation);
        }

        public async Task DeleteReservationAsync(Reservation reservation)
        {
            await _repo.DeleteReservationAsync(reservation);
        }

    }
}
