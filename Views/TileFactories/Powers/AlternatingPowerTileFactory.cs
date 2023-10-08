using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances.Tiles.Powers;

namespace Views.TileFactories.Powers
{
    public class AlternatingPowerTileFactory : MonoBehaviour
    {
        [SerializeField] private AlternatingPowerTile prefabAlternatingPowerTile;
        [SerializeField] private int preloadAlternatingPowerTileCount;

        private readonly Dictionary<int, AlternatingPowerTile> _alternatingPowerTiles = new();
        private ObjectPool<AlternatingPowerTile> _objectPoolAlternatingPowerTiles;

        private void Awake()
        {
            _objectPoolAlternatingPowerTiles = new ObjectPool<AlternatingPowerTile>(
                () => Instantiate(prefabAlternatingPowerTile, transform),
                takenAlternatingPowerTile => takenAlternatingPowerTile.Take(),
                releasedAlternatingPowerTile => releasedAlternatingPowerTile.Release(),
                destroyedAlternatingPowerTile => Destroy(destroyedAlternatingPowerTile.gameObject));
        }

        private void Start()
        {
            var preloadedAlternatingPowerTiles = new List<AlternatingPowerTile>();

            for (var i = 0; i < preloadAlternatingPowerTileCount; i++)
            {
                preloadedAlternatingPowerTiles.Add(_objectPoolAlternatingPowerTiles.Get());
            }

            foreach (var preloadedAlternatingPowerTile in preloadedAlternatingPowerTiles)
            {
                _objectPoolAlternatingPowerTiles.Release(preloadedAlternatingPowerTile);
            }
        }

        public AlternatingPowerTile Generate(int tileId)
        {
            var tile = _objectPoolAlternatingPowerTiles.Get();
            _alternatingPowerTiles.Add(tileId, tile);
            return tile;
        }

        public void ClearAll()
        {
            foreach (var tile in _alternatingPowerTiles.Values)
            {
                _objectPoolAlternatingPowerTiles.Release(tile);
            }
            
            _alternatingPowerTiles.Clear();
        }
    }
}