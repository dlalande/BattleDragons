using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragons.Common
{
    public class Reservation
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
    }
}
