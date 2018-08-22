using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using Dragons.Core.Models;
using Dragons.Service.Extensions;
using Dragons.Service.Pipeline;
using NLog;

namespace Dragons.Service.Controllers
{
    /// <summary>
    /// Controller to handle actions for games.
    /// </summary>
    [ValidationActionFilter]
    [RoutePrefix("dragons/game")]
    public class gameController : ApiController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Returns the list of event for a given game after the index offset.
        /// </summary>
        /// <param name="id">Id of game player.</param>
        /// <param name="offset">Optional offset to load events after.</param>
        /// <returns>Returns the list of event for a given game after the index offset.</returns>
        [HttpGet]
        [Route("{id}/events")]
        public async Task<IEnumerable<Event>> Get(string id, [FromUri] int offset = 0)
        {
            return await Logger.LogExecuteAsync($"GetEvents({id},{offset})", async () => await WebApiApplication.GameService.GetGameEventsAsync(id, offset));
        }

        /// <summary>
        /// Returns a game from the perspective of the given player.
        /// </summary>
        /// <param name="id">Id of game player.</param>
        /// <returns>Returns a game from the perspective of the given player.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<Game> Get(string id)
        {
            return await Logger.LogExecuteAsync($"GetGame({id})", async () => await WebApiApplication.GameService.GetGameAsync(id));
        }

        /// <summary>
        /// Plays a move in the game for the given player.
        /// </summary>
        /// <param name="move">Move to play.</param>
        /// <returns>Returns move played.</returns>
        [HttpPost]
        [Route("move")]
        public async Task<Move> Post(Move move)
        {
            return await Logger.LogExecuteAsync($"PostMove({move})", async () => await WebApiApplication.GameService.InsertGameMoveAsync(move));
        }

        /// <summary>
        /// Returns a random move for a given board size.
        /// </summary>
        /// <param name="boardSize">Size of board.</param>
        /// <returns>Returns a random move for a given board size.</returns>
        [HttpGet]
        [Route("move/random/{boardSize}")]
        public Move Get(int boardSize)
        {
            return Logger.LogExecute($"GetRandomMove({boardSize})", () => WebApiApplication.GameService.GetRandomMove(boardSize));
        }

        /// <summary>
        /// Returns a next move for a given player.
        /// <remarks>Player should not be a human.</remarks>
        /// </summary>
        /// <param name="playerId">Id of player.</param>
        /// <returns>Returns a next move for a given player.</returns>
        [HttpGet]
        [Route("move/{playerId}")]
        public async Task<Move> GetNextMove(string playerId)
        {
            return await Logger.LogExecuteAsync($"GetNextMove({playerId})", async () => await WebApiApplication.GameService.GetNextMoveAsync(playerId));
        }

        /// <summary>
        /// Returns a random player.
        /// </summary>
        /// <returns>Returns a random player.</returns>
        [HttpGet]
        [Route("player/random")]
        public Player Get()
        { 
            return Logger.LogExecute($"GetRandomPlayer()", () => WebApiApplication.GameService.GetRandomPlayer());
        }
    }
}