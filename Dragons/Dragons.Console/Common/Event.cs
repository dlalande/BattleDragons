using System;
using System.Collections.Generic;

namespace Dragons.Common
{
    public class Event
    {
        public Guid PlayerId { get; set; }
        public EventType Type { get; set; }
        public List<Piece> Pieces { get; set; }
        public int Mana { get; set; }
    }
}
