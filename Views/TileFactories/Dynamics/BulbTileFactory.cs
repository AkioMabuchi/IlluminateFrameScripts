using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances.Tiles.Dynamics;

namespace Views.TileFactories.Dynamics
{
    public class BulbTileFactory : MonoBehaviour
    {
        [SerializeField] private BulbTile prefabBulbTile;
        [SerializeField] private int preloadBulbTileCount;

        private readonly Dictionary<int, BulbTile> _bulbTiles = new();
        private ObjectPool<BulbTile> _objectPoolBulbTiles;

        private void Awake()
        {
            _objectPoolBulbTiles = new ObjectPool<BulbTile>(() => Instantiate(prefabBulbTile, transform),
                takenBulbTile => takenBulbTile.Take(), releasedBulbTile => releasedBulbTile.Release(),
                destroyedBulbTile => Destroy(destroyedBulbTile.gameObject));
        }

        private void Start()
        {
            var preloadedBulbTiles = new List<BulbTile>();

            for (var i = 0; i < preloadBulbTileCount; i++)
            {
                preloadedBulbTiles.Add(_objectPoolBulbTiles.Get());
            }

            foreach (var preloadedBulbTile in preloadedBulbTiles)
            {
                _objectPoolBulbTiles.Release(preloadedBulbTile);
            }
        }

        public BulbTile Generate(int tileId)
        {
            var tile = _objectPoolBulbTiles.Get();
            _bulbTiles.Add(tileId, tile);
            return tile;
        }

        public void ClearAll()
        {
            foreach (var tile in _bulbTiles.Values)
            {
                _objectPoolBulbTiles.Release(tile);
            }

            _bulbTiles.Clear();
        }
    }
}