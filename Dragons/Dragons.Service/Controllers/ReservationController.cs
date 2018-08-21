using Dragons.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;

namespace Dragons.Service.Controllers
{
    /// <summary>
    /// Controller to handle actions for reservations.
    /// </summary>
    [ValidationActionFilter]
    public class reservationController : ApiController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Returns the list of all reservations in the system representing waiting players.
        /// </summary>
        /// <returns>Returns the list of all reservations in the system representing waiting players.</returns>
        [HttpGet]
        public async Task<IEnumerable<Reservation>> Get()
        {
            return await Logger.LogExecuteAsync($"GetReservations()", async () => await WebApiApplication.GameService.GetReservationsAsync());
        }

        /// <summary>
        /// Inserts a new reservation for a waiting player.
        /// </summary>
        /// <param name="reservation">Reservation for waiting player.</param>
        /// <returns>Returns newly inserted reservation.</returns>
        [HttpPost]
        public async Task<Reservation> Post([FromBody]Reservation reservation)
        {
            return await Logger.LogExecuteAsync($"PostReservation({reservation})", async () => await WebApiApplication.GameService.InsertReservationAsync(reservation));
        }

        /// <summary>
        /// Deletes the given reservation for a waiting player.
        /// </summary>
        /// <param name="reservation">Reservation for waiting player to delete.</param>
        /// <returns>Nothing</returns>
        [HttpDelete]
        public async Task Delete([FromBody]Reservation reservation)
        {
            await Logger.LogExecuteAsync($"DeleteReservation({reservation})", async () => await WebApiApplication.GameService.DeleteReservationAsync(reservation));
        }
    }
}