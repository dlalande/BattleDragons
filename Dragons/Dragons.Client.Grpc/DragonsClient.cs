using Dragons.Core.Grpc;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dragons.Client.Grpc
{
    public class DragonsClient : IDisposable
    {
        private Channel _channel;
        private Core.Grpc.GameService.GameServiceClient client;

        public DragonsClient(string host, int port)
        {
            _channel = new Channel(host, port, ChannelCredentials.Insecure);
            client = new Core.Grpc.GameService.GameServiceClient(_channel);
        }

        public async Task<Player> GetRandomPlayerAsync()
        {
            return await client.GetRandomPlayerAsync(new Core.Grpc.Void());
        }

        public async Task<PlayerTuple> GetRandomPlayerPairAsync()
        {
            return await client.GetRandomPlayerPairAsync(new Core.Grpc.Void());

        }

        public async Task<Reservation> InsertReservationAsync(Reservation reservation)
        {
            return await client.InsertReservationAsync(reservation);
        }

        public async Task<Move> GetRandomMoveAsync(int boardSize, int maxMana)
        {
            return await client.GetRandomMoveAsync(new GetRandomMoveRequest { BoardSize = boardSize, Mana = maxMana });
        }

        public async Task<Move> GetNextMoveAsync(string playerId)
        {
            return await client.GetNextMoveAsync(new GetNextMoveRequest { PlayerId = playerId });
        }

        public async Task<Game> GetGameAsync(string playerId)
        {
            return await client.GetGameAsync(new GetGameRequest { PlayerId = playerId });
        }

        public async Task<List<Event>> GetGameEventsAsync(string playerId, int offset = 0)
        {
            var results = new List<Event>();
            using (var response = client.GetGameEvents(new GetGameEventsRequest { PlayerId = playerId, Offset = offset })) 
            { 
                while(await response.ResponseStream.MoveNext())
                    results.Add(response.ResponseStream.Current); 
            }
            return results;
        }

        public async Task<Move> InsertGameMoveAsync(Move move)
        {
            return await client.InsertGameMoveAsync(move);
        }

        public async Task<List<Reservation>> GetReservationsAsync()
        {
            var results = new List<Reservation>();
            using (var response = client.GetReservations(new Core.Grpc.Void()))
            {
                while (await response.ResponseStream.MoveNext())
                    results.Add(response.ResponseStream.Current);
            }
            return results;
        }

        public async Task DeleteReservationAsync(Reservation reservation)
        {
            await client.DeleteReservationAsync(reservation);
        }

        public async Task InsertGameStartAsync(GameStart gameStart)
        {
            await client.InsertGameStartAsync(gameStart);
        }

        public void Dispose()
        {
            _channel?.ShutdownAsync().Wait();
        }
    }
}
