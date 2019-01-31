using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dragons.Core.Models;

namespace Dragons.Respository
{
    public interface IGameRepository
    {
        Task InitializeAsync(GameRepositorySettings settings);
        Task<List<Reservation>> GetReservationsAsync();
        Task<Reservation> InsertReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(Reservation reservation);
        Task<GameState> GetGameStateAsync(string playerId);
        Task<GameState> InsertGameStateAsync(GameState gameState);
        Task<GameState> UpdateGameStateAsync(GameState gameState);
        Tuple<InitialSetup, InitialSetup> GetRandomInitialSetupPair();
        InitialSetup GetRandomInitialSetup();
        Player GetRandomPlayer();
        Tuple<Player, Player> GetRandomPlayerPair();
    }
}
