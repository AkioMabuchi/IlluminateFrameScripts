using System.Collections.Generic;
using Interfaces.Tiles;

namespace Views.Controllers
{
    public class TileRotator
    {
        private readonly Dictionary<int, IRotatableTile> _rotatableTiles = new();

        public void AddTile(int tileId, IRotatableTile rotatableTile)
        {
            _rotatableTiles.Add(tileId, rotatableTile);
        }

        public void RotateTile(int tileId)
        {
            if (_rotatableTiles.TryGetValue(tileId, out var rotatableTile))
            {
                rotatableTile.Rotate();
            }
        }

        public void RotateTileImmediate(int tileId)
        {
            if (_rotatableTiles.TryGetValue(tileId, out var rotatableTile))
            {
                rotatableTile.RotateImmediate();
            }
        }

        public void ClearAllTiles()
        {
            _rotatableTiles.Clear();
        }
    }
}