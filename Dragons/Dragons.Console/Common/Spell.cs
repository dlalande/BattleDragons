using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragons.Common
{
    public class Spell
    {
        public SpellType Type { get; set; }
        public string Description { get; set; }
        public int ManaCost { get; set; }
    }
}
