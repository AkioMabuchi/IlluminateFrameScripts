using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances.Tiles.Dynamics;

namespace Views.TileFactories.Dynamics
{
    public class StraightTileFactory : MonoBehaviour
    {
        [SerializeField] private StraightTile prefabStraightTile;

        [SerializeField] private int preloadStraightTileCount;

        private readonly Dictionary<int, StraightTile> _straightTiles = new();
        private ObjectPool<StraightTile> _objectPoolStraightTile;

        private void Awake()
        {
            _objectPoolStraightTile = new ObjectPool<StraightTile>(() => Instantiate(prefabStraightTile, transform),
                takenStraightTile => takenStraightTile.Take(), releasedStraightTile => releasedStraightTile.Release()
                , destroyedStraightTile => Destroy(destroyedStraightTile.gameObject));
        }

        private void Start()
        {
            var preloadedStraightTiles = new List<StraightTile>();
            
            for (var i = 0; i < preloadStraightTileCount; i++)
            {
                preloadedStraightTiles.Add(_objectPoolStraightTile.Get());
            }

            foreach (var preloadedTileStraight in preloadedStraightTiles)
            {
                _objectPoolStraightTile.Release(preloadedTileStraight);
            }
        }

        public StraightTile Generate(int tileId)
        {
            var tile = _objectPoolStraightTile.Get();
            _straightTiles.Add(tileId, tile);
            return tile;
        }

        public void ClearAll()
        {
            foreach (var tile in _straightTiles.Values)
            {
                _objectPoolStraightTile.Release(tile);
            }
            
            _straightTiles.Clear();
        }
    }
}