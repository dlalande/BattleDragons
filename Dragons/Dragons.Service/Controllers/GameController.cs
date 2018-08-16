using System.Collections.Generic;
using Dragons.Core;
using System.Web.Http;
using System.Threading.Tasks;

namespace Dragons.Service.Controllers
{
    [ValidationActionFilter]
    [RoutePrefix("api/Game")]
    public class GameController : ApiController
    {
        [Route("{id}/Events")]
        public async Task<IEnumerable<Event>> Get(string id, [FromUri] int offset = 0)
        {
            return await WebApiApplication.GameService.GetGameEventsAsync(id, offset);
        }

        public async Task<Game> Get(string id)
        {
            return await WebApiApplication.GameService.GetGameAsync(id);
        }

        [Route("Move")]
        public async Task<Move> Post(Move move)
        {
            return await WebApiApplication.GameService.InsertGameMoveAsync(move);
        }
    }
}