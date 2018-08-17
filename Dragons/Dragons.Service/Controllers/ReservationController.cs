using Dragons.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dragons.Service.Controllers
{
    /// <summary>
    /// Controller to handle actions for reservations.
    /// </summary>
    [ValidationActionFilter]
    public class ReservationController : ApiController
    {
        /// <summary>
        /// Returns the list of all reservations in the system representing waiting players.
        /// </summary>
        /// <returns>Returns the list of all reservations in the system representing waiting players.</returns>
        public async Task<IEnumerable<Reservation>> Get()
        {
            return await WebApiApplication.GameService.GetReservationsAsync();
        }

        /// <summary>
        /// Inserts a new reservation for a waiting player.
        /// </summary>
        /// <param name="reservation">Reservation for waiting player.</param>
        /// <returns>Returns newly inserted reservation.</returns>
        public async Task<Reservation> Post([FromBody]Reservation reservation)
        {
            return await WebApiApplication.GameService.InsertReservationAsync(reservation);
        }

        /// <summary>
        /// Deletes the given reservation for a waiting player.
        /// </summary>
        /// <param name="reservation">Reservation for waiting player to delete.</param>
        /// <returns>Nothing</returns>
        public async Task Delete([FromBody]Reservation reservation)
        {
            await WebApiApplication.GameService.DeleteReservationAsync(reservation);
        }
    }
}