using System.Collections.Generic;
using Interfaces.Tiles;
using UnityEngine;

namespace Views.Controllers
{
    public class TileThrower
    {
        private readonly Dictionary<int, IThrowableTile> _throwableTiles = new();

        public void AddTile(int tileId, IThrowableTile throwableTile)
        {
            _throwableTiles.Add(tileId, throwableTile);
        }

        public void ThrowTile(int tileId, Vector3 velocity, Vector3 angularVelocity)
        {
            if (_throwableTiles.TryGetValue(tileId, out var throwableTile))
            {
                throwableTile.Throw(velocity, angularVelocity);
            }
        }

        public void ClearAllTiles()
        {
            _throwableTiles.Clear();
        }
    }
}