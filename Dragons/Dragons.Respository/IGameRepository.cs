using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dragons.Core;
using MongoDB.Bson;

namespace Dragons.Respository
{
    public interface IGameRepository
    {
        Task<List<Reservation>> GetReservationsAsync();
        Task<Reservation> InsertReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(Reservation reservation);
        Task<GameState> GetGameStateAsync(string playerId);
        Task<GameState> InsertGameStateAsync(GameState gameState);
        Task<GameState> UpdateGameStateAsync(GameState gameState);
        Task<Tuple<InitialSetup, InitialSetup>> GetRandomInitialSetupsAsync();
    }
}
