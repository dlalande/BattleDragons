using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragons.Common
{
    public class ScreenService : IScreenService
    {
        private readonly IGameRepository _repo = new GameRepository();

        public async Task<Game> GetGameAsync(Guid playerId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Event>> GetGameEventsAsync(Guid playerId, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Reservation>> GetReservationsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Move> InsertGameMoveAsync(Move move)
        {
            throw new NotImplementedException();
        }

        public async Task<GameStart> InsertGameStartAsync(GameStart start)
        {
            throw new NotImplementedException();
        }

        public async Task<Reservation> InsertReservationAsync(Reservation reservation)
        {
            return await _repo.InsertReservationAsync(reservation);
        }
    }
}
