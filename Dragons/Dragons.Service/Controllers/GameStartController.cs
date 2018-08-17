using Dragons.Core;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dragons.Service.Controllers
{
    /// <summary>
    /// Controller to handle actions for game starts.
    /// </summary>
    [ValidationActionFilter]
    public class GameStartController : ApiController
    {
        /// <summary>
        /// Starts a new game for the given reservations 
        /// </summary>
        /// <param name="gameStart">Reservations to start the game with.</param>
        /// <returns>Nothing</returns>
        /// <remarks>Removes the player one reservation from the reservations list.</remarks>
        public async Task Put([FromBody]GameStart gameStart)
        {
            await WebApiApplication.GameService.InsertGameStartAsync(gameStart);
        }
    }
}