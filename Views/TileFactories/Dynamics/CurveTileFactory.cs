using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances.Tiles.Dynamics;

namespace Views.TileFactories.Dynamics
{
    public class CurveTileFactory : MonoBehaviour
    {
        [SerializeField] private CurveTile prefabCurveTile;
        [SerializeField] private int preloadCurveTileCount;

        private readonly Dictionary<int, CurveTile> _curveTiles = new();
        private ObjectPool<CurveTile> _objectPoolCurveTiles;

        private void Awake()
        {
            _objectPoolCurveTiles = new ObjectPool<CurveTile>(() => Instantiate(prefabCurveTile, transform),
                takenCurveTile => takenCurveTile.Take(), releasedCurveTile => releasedCurveTile.Release(),
                destroyedCurveTile => Destroy(destroyedCurveTile.gameObject));
        }

        private void Start()
        {
            var preloadedCurveTiles = new List<CurveTile>();

            for (var i = 0; i < preloadCurveTileCount; i++)
            {
                preloadedCurveTiles.Add(_objectPoolCurveTiles.Get());
            }

            foreach (var preloadedCurveTile in preloadedCurveTiles)
            {
                _objectPoolCurveTiles.Release(preloadedCurveTile);
            }
        }

        public CurveTile Generate(int tileId)
        {
            var tile = _objectPoolCurveTiles.Get();
            _curveTiles.Add(tileId, tile);
            return tile;
        }

        public void ClearAll()
        {
            foreach (var curveTile in _curveTiles.Values)
            {
                _objectPoolCurveTiles.Release(curveTile);
            }

            _curveTiles.Clear();
        }
    }
}