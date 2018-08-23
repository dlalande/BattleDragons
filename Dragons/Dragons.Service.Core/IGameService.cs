using System.Threading.Tasks;
using System.Collections.Generic;
using Dragons.Core.Models;
using System;

namespace Dragons.Service.Core
{
    public interface IGameService
    {
        Task InitializeAsync(string folderPath);
        Player GetRandomPlayer();
        Tuple<Player, Player> GetRandomPlayerPair();
        Task<List<Reservation>> GetReservationsAsync();
        Task<Reservation> InsertReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(Reservation reservation);
        Task InsertGameStartAsync(GameStart start);
        Task<Game> GetGameAsync(string playerId);
        Task<List<Event>> GetGameEventsAsync(string playerId, int offset = 0);
        Task<Move> InsertGameMoveAsync(Move move);
        Move GetRandomMove(int boardSize, int mana);
        Task<Move> GetNextMoveAsync(string playerId);
    }
}
