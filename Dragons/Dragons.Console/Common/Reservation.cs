using System;

namespace Dragons.Common
{
    public class Reservation
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
    }
}
