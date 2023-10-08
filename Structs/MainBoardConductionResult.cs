using System.Collections.Generic;
using UnityEngine;

namespace Structs
{
    public struct ConductBoardResult
    {
        public bool isCircuitShorted;
        public bool isCircuitClosed;
        public IEnumerable<CircuitPath> illuminatedCircuitPaths;
        public IEnumerable<CircuitPath> shortedCircuitPaths;
        public IReadOnlyDictionary<Vector2Int, ScoredTile> scoredTiles;
    }
}