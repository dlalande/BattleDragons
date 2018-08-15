using System;
using System.Collections.Generic;

namespace Dragons.Common
{
    public class Player
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public int Mana { get; set; }
        public PlayerType Type { get; set; }
        public GameBoard Board { get; set; }
        public List<Dragon> Dragons { get; set; }
    }
}
