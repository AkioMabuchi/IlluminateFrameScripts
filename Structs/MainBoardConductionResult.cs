using System.Collections.Generic;

namespace Structs
{
    public struct MainBoardConductionResult
    {
        public bool isCircuitShorted;
        public bool isCircuitClosed;
        public IEnumerable<ScoredTile> scoredTiles;
        public IEnumerable<IlluminatePath> illuminatePaths;
    }
}