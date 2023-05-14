using Enums;
using UnityEngine;

namespace Structs
{
    public struct ScoredTile
    {
        public Vector2Int cellPosition;
        public ElectricStatus electricStatus;
        public int score;
    }
}