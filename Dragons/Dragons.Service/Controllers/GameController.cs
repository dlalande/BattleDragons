using System.Collections.Generic;
using Dragons.Core;
using System.Web.Http;
using System.Threading.Tasks;

namespace Dragons.Service.Controllers
{
    /// <summary>
    /// Controller to handle actions for games.
    /// </summary>
    [ValidationActionFilter]
    [RoutePrefix("dragons/game")]
    public class gameController : ApiController
    {
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
            return await WebApiApplication.GameService.GetGameEventsAsync(id, offset);
        }

        /// <summary>
        /// Returns a game from the perspective of the given player.
        /// </summary>
        /// <param name="id">Id of game player.</param>
        /// <returns>Returns a game from the perspective of the given player.</returns>
        [HttpGet]
        public async Task<Game> Get(string id)
        {
            return await WebApiApplication.GameService.GetGameAsync(id);
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
            return await WebApiApplication.GameService.InsertGameMoveAsync(move);
        }

        /// <summary>
        /// Returns a random move for a given board size.
        /// </summary>
        /// <returns>Returns move played.</returns>
        [HttpGet]
        [Route("move/{boardSize}")]
        public Move Get(int boardSize)
        {
            return WebApiApplication.GameService.GetRandomMove(boardSize);
        }

        /// <summary>
        /// Returns a random player.
        /// </summary>
        /// <returns>Returns a random player.</returns>
        [HttpGet]
        [Route("player")]
        public Player Get()
        {
            return WebApiApplication.GameService.GetRandomPlayer();
        }
    }
}