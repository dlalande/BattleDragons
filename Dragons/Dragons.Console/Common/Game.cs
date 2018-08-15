﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragons.Common
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
