using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Models.Instances.Frames
{
    public abstract class FrameBaseModel
    {
        public abstract IEnumerable<(Vector2Int, TileType)> InitialTiles
        {
            get;
        }

        public abstract bool IsInBoard(Vector2Int cellPosition);
    }
}