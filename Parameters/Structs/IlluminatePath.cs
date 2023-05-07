using System.Collections.Generic;
using Parameters.Enums;
using UnityEngine;

namespace Parameters.Structs
{
    public struct IlluminatePath
    {
        public ElectricStatus ElectricStatus;
        public Vector2Int CellPosition;
        public IEnumerable<LineDirectionPair> LineDirectionPairs;
    }
}