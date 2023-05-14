using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Models.Instances.Frames
{
    public class FrameSmallModel : FrameBaseModel
    {
        public override IEnumerable<(Vector2Int, TileType)> InitialTiles => new List<(Vector2Int, TileType)>
        {
            (new Vector2Int(0, 0), TileType.PowerNormal),
            (new Vector2Int(-4, 3), TileType.TerminalNormalL),
            (new Vector2Int(-4, -3), TileType.TerminalNormalL),
            (new Vector2Int(4, 3), TileType.TerminalNormalR),
            (new Vector2Int(4, -3), TileType.TerminalNormalR)
        };
        
        public override bool IsInBoard(Vector2Int cellPosition)
        {
            return -4 <= cellPosition.x && cellPosition.x <= 4 && -3 <= cellPosition.y && cellPosition.y <= 3;
        }
    }
}