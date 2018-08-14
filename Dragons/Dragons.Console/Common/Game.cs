using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragons.Common
{
    public class Game
    {
        public Guid Id { get; set; }
        public Player Me { get; set; }
        public Player Opponent { get; set; }
        public GameBoard Board { get; set; }
    }
}
