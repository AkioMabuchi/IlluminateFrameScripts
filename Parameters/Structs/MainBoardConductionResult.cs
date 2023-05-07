using System.Collections.Generic;

namespace Parameters.Structs
{
    public struct MainBoardConductionResult
    {
        public bool IsCircuitShorted;
        public bool IsCircuitClosed;
        public int TotalScore;
        public IEnumerable<IlluminatePath> IlluminatePaths;
    }
}