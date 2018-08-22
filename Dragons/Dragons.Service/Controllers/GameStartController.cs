using System.Threading.Tasks;
using System.Web.Http;
using Dragons.Core.Models;
using Dragons.Service.Extensions;
using Dragons.Service.Pipeline;
using NLog;

namespace Dragons.Service.Controllers
{
    /// <summary>
    /// Controller to handle actions for game starts.
    /// </summary>
    [ValidationActionFilter]
    public class gamestartController : ApiController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Starts a new game for the given reservations 
        /// </summary>
        /// <param name="gameStart">Reservations to start the game with.</param>
        /// <returns>Nothing</returns>
        /// <remarks>Removes the player one reservation from the reservations list.</remarks>
        [HttpPut]
        public async Task Put([FromBody]GameStart gameStart)
        {
            await Logger.LogExecuteAsync($"PutGameStart({gameStart})", async () => await WebApiApplication.GameService.InsertGameStartAsync(gameStart));
            
        }
    }
}