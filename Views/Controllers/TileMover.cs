using System.Collections.Generic;
using Interfaces.Tiles;
using UnityEngine;

namespace Views.Controllers
{
    public class TileMover
    {
        private readonly Dictionary<int, IMovableTile> _movableTiles = new();

        public void AddTile(int tileId, IMovableTile movableTile)
        {
            _movableTiles.Add(tileId, movableTile);
        }

        public void MoveTile(int tileId, Vector3 position)
        {
            if (_movableTiles.TryGetValue(tileId, out var movableTile))
            {
                movableTile.Move(position);
            }
        }

        public void ClearAllTiles()
        {
            _movableTiles.Clear();
        }
    }
}