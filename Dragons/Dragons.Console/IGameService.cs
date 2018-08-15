using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Dragons.Common
{
    interface IGameService
    {
        Task<List<Reservation>> GetReservationsAsync();
        Task<Reservation> InsertReservationAsync(Reservation reservation);
        Task InsertGameStartAsync(GameStart start);
        Task<Game> GetGameAsync(Guid playerId);
        Task<List<Event>> GetGameEventsAsync(Guid playerId, int offset = 0);
        Task<Move> InsertGameMoveAsync(Move move);
    }
}
