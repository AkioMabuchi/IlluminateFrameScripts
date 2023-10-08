using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances.Tiles.Powers;

namespace Views.TileFactories.Powers
{
    public class PlusPowerTileFactory : MonoBehaviour
    {
        [SerializeField] private PlusPowerTile prefabPlusPowerTile;
        [SerializeField] private int preloadPlusPowerTileCount;

        private readonly Dictionary<int, PlusPowerTile> _plusPowerTiles = new();
        private ObjectPool<PlusPowerTile> _objectPoolPlusPowerTiles;

        private void Awake()
        {
            _objectPoolPlusPowerTiles = new ObjectPool<PlusPowerTile>(() => Instantiate(prefabPlusPowerTile, transform),
                takenPlusPowerTile => takenPlusPowerTile.Take(),
                releasedPlusPowerTile => releasedPlusPowerTile.Release(),
                destroyedPlusPowerTile => Destroy(destroyedPlusPowerTile.gameObject));
        }

        private void Start()
        {
            var preloadedPlusPowerTiles = new List<PlusPowerTile>();

            for (var i = 0; i < preloadPlusPowerTileCount; i++)
            {
                preloadedPlusPowerTiles.Add(_objectPoolPlusPowerTiles.Get());
            }

            foreach (var preloadedPlusPowerTile in preloadedPlusPowerTiles)
            {
                _objectPoolPlusPowerTiles.Release(preloadedPlusPowerTile);
            }
        }

        public PlusPowerTile Generate(int tileId)
        {
            var tile = _objectPoolPlusPowerTiles.Get();
            _plusPowerTiles.Add(tileId, tile);
            return tile;
        }

        public void ClearAll()
        {
            foreach (var tile in _plusPowerTiles.Values)
            {
                _objectPoolPlusPowerTiles.Release(tile);
            }
            
            _plusPowerTiles.Clear();
        }
    }
}