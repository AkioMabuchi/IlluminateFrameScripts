using System.Collections.Generic;
using Interfaces.Tiles;

namespace Views.Controllers
{
    public class BulbTiles
    {
        private readonly Dictionary<int, IBulbTile> _bulbTiles = new();

        public void AddTile(int tileId, IBulbTile bulbTile)
        {
            _bulbTiles.Add(tileId, bulbTile);
        }

        public void IlluminateTile(int tileId)
        {
            if (_bulbTiles.TryGetValue(tileId, out var bulbTile))
            {
                bulbTile.Illuminate();
            }
        }

        public void ClearAllTiles()
        {
            _bulbTiles.Clear();
        }
    }
}