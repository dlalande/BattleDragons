using System;

namespace Dragons.Common
{
    public class Move
    {
        public Guid PlayerId { get; set; }
        public Coordinate Coordinate { get; set; }
        public Spell Spell { get; set; }

        public void Execute(GameState gameState)
        {

        }
    }
}
