using System;
using System.Collections.Generic;

namespace Dragons.Common
{
    public class GameEvent
    {
        public PlayerSide Side { get; set; }
        public GameEventType Type { get; set; }
        public List<GamePiece> Pieces { get; set; }
    }
}
