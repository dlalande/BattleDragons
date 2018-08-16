using Dragons.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dragons.Service.Controllers
{
    [ValidationActionFilter]
    public class ReservationController : ApiController
    {
        public async Task<IEnumerable<Reservation>> Get()
        {
            return await WebApiApplication.GameService.GetReservationsAsync();
        }

        public async Task<Reservation> Post([FromBody]Reservation reservation)
        {
            return await WebApiApplication.GameService.InsertReservationAsync(reservation);
        }
    }
}