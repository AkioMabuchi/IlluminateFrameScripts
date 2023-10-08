using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances.Tiles.Dynamics;

namespace Views.TileFactories.Dynamics
{
    public class FourWayDistributorTileFactory : MonoBehaviour
    {
        [SerializeField] private FourWayDistributorTile prefabFourWayDistributorTile;
        [SerializeField] private int preloadFourWayDistributorTileCount;

        private readonly Dictionary<int, FourWayDistributorTile> _fourWayDistributorTiles = new();
        private ObjectPool<FourWayDistributorTile> _objectPoolFourWayDistributorTiles;

        private void Awake()
        {
            _objectPoolFourWayDistributorTiles = new ObjectPool<FourWayDistributorTile>(
                () => Instantiate(prefabFourWayDistributorTile, transform),
                takenFourWayDistributorTile => takenFourWayDistributorTile.Take(),
                releasedFourWayDistributorTile => releasedFourWayDistributorTile.Release(),
                destroyedFourWayDistributorTile => Destroy(destroyedFourWayDistributorTile.gameObject));
        }

        private void Start()
        {
            var preloadedFourWayDistributorTiles = new List<FourWayDistributorTile>();

            for (var i = 0; i < preloadFourWayDistributorTileCount; i++)
            {
                preloadedFourWayDistributorTiles.Add(_objectPoolFourWayDistributorTiles.Get());
            }

            foreach (var preloadedFourWayDistributorTile in preloadedFourWayDistributorTiles)
            {
                _objectPoolFourWayDistributorTiles.Release(preloadedFourWayDistributorTile);
            }
        }

        public FourWayDistributorTile Generate(int tileId)
        {
            var tile = _objectPoolFourWayDistributorTiles.Get();
            _fourWayDistributorTiles.Add(tileId, tile);
            return tile;
        }

        public void ClearAll()
        {
            foreach (var tile in _fourWayDistributorTiles.Values)
            {
                _objectPoolFourWayDistributorTiles.Release(tile);
            }

            _fourWayDistributorTiles.Clear();
        }
    }
}