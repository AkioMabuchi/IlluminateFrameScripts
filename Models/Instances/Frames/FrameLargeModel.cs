using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Models.Instances.Frames
{
    public class FrameLargeModel : FrameBaseModel
    {
        public override IEnumerable<(Vector2Int, TileType)> InitialTiles => new List<(Vector2Int, TileType)>
        {
            (new Vector2Int(-1, 0), TileType.PowerMinus),
            (new Vector2Int(0, 0), TileType.PowerAlternating),
            (new Vector2Int(1, 0), TileType.PowerPlus),
            (new Vector2Int(-7, 6), TileType.TerminalPlus),
            (new Vector2Int(-7, -6), TileType.TerminalPlus),
            (new Vector2Int(-7, 0), TileType.TerminalAlternatingL),
            (new Vector2Int(7, 0), TileType.TerminalAlternatingR),
            (new Vector2Int(7, 6), TileType.TerminalMinus),
            (new Vector2Int(7, -6), TileType.TerminalMinus),
        };

        public override bool IsInBoard(Vector2Int cellPosition)
        {
            return -7 <= cellPosition.x && cellPosition.x <= 7 && -6 <= cellPosition.y && cellPosition.y <= 6;
        }
    }
}