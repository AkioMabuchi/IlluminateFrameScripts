using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances.Tiles.Terminals;

namespace Views.TileFactories.Terminals
{
    public class PlusTerminalTileFactory : MonoBehaviour
    {
        [SerializeField] private PlusTerminalTile prefabPlusTerminalTile;
        [SerializeField] private int preloadTerminalTileCount;

        private readonly Dictionary<int, PlusTerminalTile> _terminalTiles = new();
        private ObjectPool<PlusTerminalTile> _objectPoolTerminalTiles;

        private void Awake()
        {
            _objectPoolTerminalTiles = new ObjectPool<PlusTerminalTile>(
                () => Instantiate(prefabPlusTerminalTile, transform),
                takenTerminalTile => takenTerminalTile.Take(), releasedTerminalTile => releasedTerminalTile.Release(),
                destroyedTerminalTile => Destroy(destroyedTerminalTile));
        }

        private void Start()
        {
            var preloadedTerminalTiles = new List<PlusTerminalTile>();

            for (var i = 0; i < preloadTerminalTileCount; i++)
            {
                preloadedTerminalTiles.Add(_objectPoolTerminalTiles.Get());
            }

            foreach (var preloadedTerminalTile in preloadedTerminalTiles)
            {
                _objectPoolTerminalTiles.Release(preloadedTerminalTile);
            }
        }

        public PlusTerminalTile Generate(int tileId)
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