using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Structs
{
    public struct IlluminatePath
    {
        public ElectricStatus electricStatus;
        public Vector2Int cellPosition;
        public IEnumerable<LineDirectionPair> lineDirectionPairs;
    }
}