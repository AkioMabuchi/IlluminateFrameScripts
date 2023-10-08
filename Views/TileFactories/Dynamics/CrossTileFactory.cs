using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances.Tiles.Dynamics;

namespace Views.TileFactories.Dynamics
{
    public class CrossTileFactory : MonoBehaviour
    {
        [SerializeField] private CrossTile prefabCrossTile;
        [SerializeField] private int preloadCrossTileCount;

        private readonly Dictionary<int, CrossTile> _crossTiles = new();
        private ObjectPool<CrossTile> _objectPoolCrossTiles;

        private void Awake()
        {
            _objectPoolCrossTiles = new ObjectPool<CrossTile>(() => Instantiate(prefabCrossTile, transform),
                takenCrossTile => takenCrossTile.Take(), releasedCrossTile => releasedCrossTile.Release(),
                destroyedCrossTile => Destroy(destroyedCrossTile.gameObject));
        }

        private void Start()
        {
            var preloadedCrossTiles = new List<CrossTile>();

            for (var i = 0; i < preloadCrossTileCount; i++)
            {
                preloadedCrossTiles.Add(_objectPoolCrossTiles.Get());
            }

            foreach (var preloadedCrossTile in preloadedCrossTiles)
            {
                _objectPoolCrossTiles.Release(preloadedCrossTile);
            }
        }

        public CrossTile Generate(int tileId)
        {
            var tile = _objectPoolCrossTiles.Get();
            _crossTiles.Add(tileId, tile);
            return tile;
        }

        public void ClearAll()
        {
            foreach (var tile in _crossTiles.Values)
            {
                _objectPoolCrossTiles.Release(tile);
            }

            _crossTiles.Clear();
        }
    }
}