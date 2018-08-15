using System.Collections.Generic;

namespace Dragons.Core
{
    public class Game
    {
        public string Name { get; set; }
        public string Opponent { get; set; }
        public int Mana { get; set; }
        public GameBoard Board { get; set; }
        public List<Spell> Spells { get; set; }
    }
}
