using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances.Tiles.Dynamics;

namespace Views.TileFactories.Dynamics
{
    public class TwinCurvesTileFactory : MonoBehaviour
    {
        [SerializeField] private TwinCurvesTile prefabTwinCurvesTile;
        [SerializeField] private int preloadTwinCurvesTileCount;

        private readonly Dictionary<int, TwinCurvesTile> _twinCurvesTiles = new();
        private ObjectPool<TwinCurvesTile> _objectPoolTwinCurvesTiles;

        private void Awake()
        {
            _objectPoolTwinCurvesTiles = new ObjectPool<TwinCurvesTile>(
                () => Instantiate(prefabTwinCurvesTile, transform), takenTwinCurvesTile => takenTwinCurvesTile.Take(),
                releasedTwinCurvesTile => releasedTwinCurvesTile.Release(),
                destroyedTwinCurvesTile => Destroy(destroyedTwinCurvesTile.gameObject));
        }

        private void Start()
        {
            var preloadedTwinCurvesTiles = new List<TwinCurvesTile>();

            for (var i = 0; i < preloadTwinCurvesTileCount; i++)
            {
                preloadedTwinCurvesTiles.Add(_objectPoolTwinCurvesTiles.Get());
            }

            foreach (var preloadedTwinCurvesTile in preloadedTwinCurvesTiles)
            {
                _objectPoolTwinCurvesTiles.Release(preloadedTwinCurvesTile);
            }
        }

        public TwinCurvesTile Generate(int tileId)
        {
            var tile = _objectPoolTwinCurvesTiles.Get();
            _twinCurvesTiles.Add(tileId, tile);
            return tile;
        }

        public void ClearAll()
        {
            foreach (var tile in _twinCurvesTiles.Values)
            {
                _objectPoolTwinCurvesTiles.Release(tile);
            }

            _twinCurvesTiles.Clear();
        }
    }
}