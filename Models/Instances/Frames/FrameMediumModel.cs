using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Models.Instances.Frames
{
    public class FrameMediumModel : FrameBaseModel
    {
        public override IEnumerable<(Vector2Int, TileType)> InitialTiles => new List<(Vector2Int, TileType)>
        {
            (new Vector2Int(-1, 0), TileType.PowerMinus),
            (new Vector2Int(0, 0), TileType.PowerPlus),
            (new Vector2Int(-6, 5), TileType.TerminalPlus),
            (new Vector2Int(-6, -5), TileType.TerminalPlus),
            (new Vector2Int(5, 5), TileType.TerminalMinus),
            (new Vector2Int(5, -5), TileType.TerminalMinus),
        };
        
        public override bool IsInBoard(Vector2Int cellPosition)
        {
            return -6 <= cellPosition.x && cellPosition.x <= 5 && -5 <= cellPosition.y && cellPosition.y <= 5;
        }
    }
}