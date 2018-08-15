using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragons.Common
{
    interface IGameRepository
    {
        Task<Reservation> InsertReservationAsync(Reservation reservation);
    }
}
