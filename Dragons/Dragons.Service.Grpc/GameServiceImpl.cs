using Dragons.Core.Grpc;
using Grpc.Core;
using System.Threading.Tasks;

namespace Dragons.Service.Grpc
{
    class GameServiceImpl : GameService.GameServiceBase
    {
        /// <summary>
        /// Global static game service used by controllers.
        /// </summary>
        private Core.IGameService _gameService = new Core.GameService();

        public GameServiceImpl(Core.IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<Player> GetRandomPlayer(Void request, ServerCallContext context)
        {
            var player = _gameService.GetRandomPlayer();
            return Task.FromResult(player.ToPlayer());
        }

        public override Task<PlayerTuple> GetRandomPlayerPair(Void request, ServerCallContext context)
        {
            var playerPair = _gameService.GetRandomPlayerPair();
            return Task.FromResult(playerPair.ToPlayerTuple());
        }

        public override async Task<Reservation> InsertReservation(Reservation request, ServerCallContext context)
        {
            var reservation = await _gameService.InsertReservationAsync(request.ToReservation());
            return reservation.ToReservation();
        }

        public override async Task GetReservations(Void request, IServerStreamWriter<Reservation> responseStream, ServerCallContext context)
        {
            var reservations = await _gameService.GetReservationsAsync();
            foreach (var reservation in reservations)
            {
                await responseStream.WriteAsync(reservation.ToReservation());
            }
        }

        public override async Task<Void> DeleteReservation(Reservation request, ServerCallContext context)
        {
            await _gameService.DeleteReservationAsync(request.ToReservation());
            return new Void();
        }

        public override async Task<Game> GetGame(GetGameRequest request, ServerCallContext context)
        {
            var game = await _gameService.GetGameAsync(request.PlayerId);
            return game.ToGame();
        }

        public override async Task<Void> InsertGameStart(GameStart request, ServerCallContext context)
        {
            await _gameService.InsertGameStartAsync(request.ToGameStart());
            return new Void();
        }

        public override async Task<Move> InsertGameMove(Move request, ServerCallContext context)
        {
            var move = await _gameService.InsertGameMoveAsync(request.ToMove());
            return move.ToMove();
        }

        public override async Task GetGameEvents(GetGameEventsRequest request, IServerStreamWriter<Event> responseStream, ServerCallContext context)
        {
            var events = await _gameService.GetGameEventsAsync(request.PlayerId, request.Offset);
            foreach (var e in events)
            {
                await responseStream.WriteAsync(e.ToEvent());
            }
        }

        public override async Task<Move> GetNextMove(GetNextMoveRequest request, ServerCallContext context)
        {
            var move = await _gameService.GetNextMoveAsync(request.PlayerId);
            return move.ToMove();      
        }

        public override Task<Move> GetRandomMove(GetRandomMoveRequest request, ServerCallContext context)
        {
            var move =  _gameService.GetRandomMove(request.BoardSize, request.Mana);
            return Task.FromResult(move.ToMove());
        }
    }
}