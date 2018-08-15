using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dragons.Common
{
    public class GameRepository : IGameRepository
    {
        public Task<Reservation> InsertReservationAsync(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public Task<GameState> GetGameSateAsync(Guid playerId)
        {
            throw new NotImplementedException();
        }

        public Task<GameState> InsertGameStateAsync(GameState gameState)
        {
            throw new NotImplementedException();
        }
        public Task<GameState> UpdateGameStateAsync(GameState gameState)
        {
            throw new NotImplementedException();
        }
        public Task<Tuple<InitialSetup,InitialSetup>> GetRandomInitialSetupsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Reservation>> GetReservationsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
