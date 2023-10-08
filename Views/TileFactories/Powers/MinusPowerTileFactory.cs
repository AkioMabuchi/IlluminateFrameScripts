using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances.Tiles.Powers;

namespace Views.TileFactories.Powers
{
    public class MinusPowerTileFactory : MonoBehaviour
    {
        [SerializeField] private MinusPowerTile prefabMinusPowerTile;
        [SerializeField] private int preloadMinusPowerTileCount;

        private readonly Dictionary<int, MinusPowerTile> _minusPowerTiles = new();
        private ObjectPool<MinusPowerTile> _objectPoolMinusPowerTiles;

        private void Awake()
        {
            _objectPoolMinusPowerTiles = new ObjectPool<MinusPowerTile>(
                () => Instantiate(prefabMinusPowerTile, transform), takenMinusPowerTile => takenMinusPowerTile.Take(),
                releasedMinusPowerTile => releasedMinusPowerTile.Release(),
                destroyedMinusPowerTile => Destroy(destroyedMinusPowerTile.gameObject));
        }

        private void Start()
        {
            var preloadedMinusPowerTiles = new List<MinusPowerTile>();

            for (var i = 0; i < preloadMinusPowerTileCount; i++)
            {
                preloadedMinusPowerTiles.Add(_objectPoolMinusPowerTiles.Get());
            }

            foreach (var preloadedMinusPowerTile in preloadedMinusPowerTiles)
            {
                _objectPoolMinusPowerTiles.Release(preloadedMinusPowerTile);
            }
        }

        public MinusPowerTile Generate(int tileId)
        {
            var tile = _objectPoolMinusPowerTiles.Get();
            _minusPowerTiles.Add(tileId, tile);
            return tile;
        }

        public void ClearAll()
        {
            foreach (var tile in _minusPowerTiles.Values)
            {
                _objectPoolMinusPowerTiles.Release(tile);
            }
            
            _minusPowerTiles.Clear();
        }
    }
}