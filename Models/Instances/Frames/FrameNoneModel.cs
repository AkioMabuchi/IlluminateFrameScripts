using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Models.Instances.Frames
{
    public class FrameNoneModel : FrameBaseModel
    {
        public override IEnumerable<(Vector2Int, TileType)> InitialTiles => new List<(Vector2Int, TileType)>();
        public override bool IsInBoard(Vector2Int cellPosition)
        {
            return false;
        }
    }
}