using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dragons.Core;
using Dragons.Core.Models;
using Dragons.Core.Types;
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

        public Move GetRandomMove(int boardSize)
        {
            return new Move
            {
                Player = null,
                Coordinate = Coordinate.Random(boardSize),
                Spell = Spell.AllSpells.Random()
            };
        }

        public async Task<Game> GetGameAsync(string playerId)
        {
            var gameState = await _repo.GetGameStateAsync(playerId);
            if (gameState == null)
                return null;

            var playerState = gameState.Player1State.Player.PlayerId.Equals(playerId) ? gameState.Player1State : gameState.Player2State;
            return gameState.ToGame(playerState);
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
            if (string.IsNullOrWhiteSpace(move.Player?.PlayerId))
                throw new ArgumentException("Player must not be empty.", nameof(move));
            var gameState = await _repo.GetGameStateAsync(move.Player.PlayerId);
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
                Player1State = new PlayerState
                {
                    Player = start.Player1,
                    Mana = Constants.DefaultInitialMana,
                    Board = new GameBoard(player1Setup)
                },
                Player2State = new PlayerState
                {
                    Player = start.Player2,
                    Mana = Constants.DefaultInitialMana,
                    Board = new GameBoard(player2Setup)
                },
                Events = new List<Event>
                {
                    new Event()
                    {
                        Player = start.Player1,
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

        public async Task<Move> GetNextMoveAsync(string playerId)
        {
            var gameState = await _repo.GetGameStateAsync(playerId);
            return gameState.GetNextMove(playerId);
        }

    }
}
