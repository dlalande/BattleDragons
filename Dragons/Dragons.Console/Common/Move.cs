using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragons.Common
{
    public class Move
    {
        public Guid PlayerId { get; set; }
        public Coordinate Coordinate { get; set; }
        public Spell Spell { get; set; }
    }
}
