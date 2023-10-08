using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances.Tiles.Terminals;

namespace Views.TileFactories.Terminals
{
    public class MinusTerminalTileFactory : MonoBehaviour
    {
        [SerializeField] private MinusTerminalTile prefabMinusTerminalTIle;
        [SerializeField] private int preloadTerminalTileCount;

        private readonly Dictionary<int, MinusTerminalTile> _terminalTiles = new();
        private ObjectPool<MinusTerminalTile> _objectPoolTerminalTiles;

        private void Awake()
        {
            _objectPoolTerminalTiles = new ObjectPool<MinusTerminalTile>(() => Instantiate(prefabMinusTerminalTIle, transform),
                takenTerminalTile => takenTerminalTile.Take(), releasedTerminalTile => releasedTerminalTile.Release(),
                destroyedTerminalTile => Destroy(destroyedTerminalTile));
        }

        private void Start()
        {
            var preloadedTerminalTiles = new List<MinusTerminalTile>();

            for (var i = 0; i < preloadTerminalTileCount; i++)
            {
                preloadedTerminalTiles.Add(_objectPoolTerminalTiles.Get());
            }

            foreach (var preloadedTerminalTile in preloadedTerminalTiles)
            {
                _objectPoolTerminalTiles.Release(preloadedTerminalTile);
            }
        }

        public MinusTerminalTile Generate(int tileId)
        {
            var tile = _objectPoolTerminalTiles.Get();
            _terminalTiles.Add(tileId, tile);
            return tile;
        }

        public void ClearAll()
        {
            foreach (var tile in _terminalTiles.Values)
            {
                _objectPoolTerminalTiles.Release(tile);
            }
            
            _terminalTiles.Clear();
        }
    }
}