using System.Collections.Generic;

namespace Dragons.Core
{
    public class InitialSetup
    {
        public int BoardSize { get; set; }
        public List<Dragon> Dragons { get; set; }
        public List<Piece> AdditionalPieces { get; set; }
    }
}
