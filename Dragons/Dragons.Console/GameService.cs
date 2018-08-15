using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dragons.Common
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repo = new GameRepository();

        public async Task<Game> GetGameAsync(Guid playerId)
        {
            var gameState = await _repo.GetGameSateAsync(playerId);
            return gameState?.ToGame(playerId);
        }

        public async Task<List<Event>> GetGameEventsAsync(Guid playerId, int offset = 0)
        {
            var gameState = await _repo.GetGameSateAsync(playerId);
            return gameState?.Events.SkipWhile((e, index) => index > offset - 1).ToList();
        }

        public async Task<List<Reservation>> GetReservationsAsync()
        {
            return await _repo.GetReservationsAsync();
        }

        public async Task<Move> InsertGameMoveAsync(Move move)
        {
            var gameState = await _repo.GetGameSateAsync(move.PlayerId);
            gameState.Moves.Push(move);
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
                }
            };
            await _repo.InsertGameStateAsync(gameState);
        }

        public async Task<Reservation> InsertReservationAsync(Reservation reservation)
        {
            return await _repo.InsertReservationAsync(reservation);
        }
    }
}
