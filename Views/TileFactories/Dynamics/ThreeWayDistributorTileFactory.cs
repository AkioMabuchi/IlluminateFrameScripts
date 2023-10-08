using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances.Tiles.Dynamics;

namespace Views.TileFactories.Dynamics
{
    public class ThreeWayDistributorTileFactory : MonoBehaviour
    {
        [SerializeField] private ThreeWayDistributorTile prefabThreeWayDistributorTile;
        [SerializeField] private int preloadThreeWayDistributorTileCount;

        private readonly Dictionary<int, ThreeWayDistributorTile> _threeWayDistributorTiles = new();
        private ObjectPool<ThreeWayDistributorTile> _objectPoolThreeWayDistributorTiles;

        private void Awake()
        {
            _objectPoolThreeWayDistributorTiles = new ObjectPool<ThreeWayDistributorTile>(
                () => Instantiate(prefabThreeWayDistributorTile, transform),
                takenThreeWayDistributorTile => takenThreeWayDistributorTile.Take(),
                releasedThreeWayDistributorTile => releasedThreeWayDistributorTile.Release(),
                destroyedThreeWayDistributorTile => Destroy(destroyedThreeWayDistributorTile.gameObject));
        }

        private void Start()
        {
            var preloadedThreeWayDistributorTiles = new List<ThreeWayDistributorTile>();

            for (var i = 0; i < preloadThreeWayDistributorTileCount; i++)
            {
                preloadedThreeWayDistributorTiles.Add(_objectPoolThreeWayDistributorTiles.Get());
            }

            foreach (var preloadedThreeWayDistributorTile in preloadedThreeWayDistributorTiles)
            {
                _objectPoolThreeWayDistributorTiles.Release(preloadedThreeWayDistributorTile);
            }
        }

        public ThreeWayDistributorTile Generate(int tileId)
        {
            var tile = _objectPoolThreeWayDistributorTiles.Get();
            _threeWayDistributorTiles.Add(tileId, tile);
            return tile;
        }

        public void ClearAll()
        {
            foreach (var tile in _threeWayDistributorTiles.Values)
            {
                _objectPoolThreeWayDistributorTiles.Release(tile);
            }

            _threeWayDistributorTiles.Clear();
        }
    }
}