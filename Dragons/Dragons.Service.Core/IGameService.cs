using System.Threading.Tasks;
using System.Collections.Generic;
using Dragons.Core;

namespace Dragons.Service.Core
{
    public interface IGameService
    {
        Task InitializeAsync(string folderPath);
        Player GetRandomPlayer();
        Task<List<Reservation>> GetReservationsAsync();
        Task<Reservation> InsertReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(Reservation reservation);
        Task InsertGameStartAsync(GameStart start);
        Task<Game> GetGameAsync(string playerId);
        Task<List<Event>> GetGameEventsAsync(string playerId, int offset = 0);
        Task<Move> InsertGameMoveAsync(Move move);
        // This would be used to generate a random move for the given player id.  
        // It will load the game by playerId, and based on the playerType (Human, EasyComputer, MediumComputer, HardComputer)
        // Task<Move> GetNextMoveAsync(string playerId);
        Move GetRandomMove(int size);
    }
}
