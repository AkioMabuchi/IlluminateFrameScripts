using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances.Tiles.Powers;

namespace Views.TileFactories.Powers
{
    public class NormalPowerTileFactory : MonoBehaviour
    {
        [SerializeField] private NormalPowerTile prefabNormalPowerTile;
        [SerializeField] private int preloadNormalPowerTileCount;

        private readonly Dictionary<int, NormalPowerTile> _normalPowerTiles = new();
        private ObjectPool<NormalPowerTile> _objectPoolNormalPowerTiles;

        private void Awake()
        {
            _objectPoolNormalPowerTiles = new ObjectPool<NormalPowerTile>(
                () => Instantiate(prefabNormalPowerTile, transform),
                takenNormalPowerTile => takenNormalPowerTile.Take(),
                releasedNormalPowerTile => releasedNormalPowerTile.Release(),
                destroyedNormalPowerTile => Destroy(destroyedNormalPowerTile.gameObject));
        }

        private void Start()
        {
            var preloadedNormalPowerTiles = new List<NormalPowerTile>();

            for (var i = 0; i < preloadNormalPowerTileCount; i++)
            {
                preloadedNormalPowerTiles.Add(_objectPoolNormalPowerTiles.Get());
            }

            foreach (var preloadedNormalPowerTile in preloadedNormalPowerTiles)
            {
                _objectPoolNormalPowerTiles.Release(preloadedNormalPowerTile);
            }
        }

        public NormalPowerTile Generate(int tileId)
        {
            var tile = _objectPoolNormalPowerTiles.Get();
            _normalPowerTiles.Add(tileId, tile);
            return tile;
        }

        public void ClearAll()
        {
            foreach (var tile in _normalPowerTiles.Values)
            {
                _objectPoolNormalPowerTiles.Release(tile);
            }
            
            _normalPowerTiles.Clear();
        }
    }
}