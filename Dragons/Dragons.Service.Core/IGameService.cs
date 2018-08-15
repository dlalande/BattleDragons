using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dragons.Core;

namespace Dragons.Service.Core
{
    public interface IGameService
    {
        Task<List<Reservation>> GetReservationsAsync();
        Task<Reservation> InsertReservationAsync(Reservation reservation);
        Task InsertGameStartAsync(GameStart start);
        Task<Game> GetGameAsync(Guid playerId);
        Task<List<Event>> GetGameEventsAsync(Guid playerId, int offset = 0);
        Task<Move> InsertGameMoveAsync(Move move);
    }
}
