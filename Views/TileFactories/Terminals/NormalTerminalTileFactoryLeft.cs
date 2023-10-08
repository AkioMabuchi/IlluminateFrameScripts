using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances.Tiles.Terminals;

namespace Views.TileFactories.Terminals
{
    public class NormalTerminalTileFactoryLeft : MonoBehaviour
    {
        [SerializeField] private NormalTerminalTileLeft prefabNormalTerminalTileLeft;
        [SerializeField] private int preloadTerminalTileCount;

        private readonly Dictionary<int, NormalTerminalTileLeft> _terminalTiles = new();
        private ObjectPool<NormalTerminalTileLeft> _objectPoolTerminalTiles;

        private void Awake()
        {
            _objectPoolTerminalTiles = new ObjectPool<NormalTerminalTileLeft>(
                () => Instantiate(prefabNormalTerminalTileLeft, transform),
                takenTerminalTile => takenTerminalTile.Take(), releasedTerminalTile => releasedTerminalTile.Release(),
                destroyedTerminalTile => Destroy(destroyedTerminalTile));
        }

        private void Start()
        {
            var preloadedTerminalTiles = new List<NormalTerminalTileLeft>();

            for (var i = 0; i < preloadTerminalTileCount; i++)
            {
                preloadedTerminalTiles.Add(_objectPoolTerminalTiles.Get());
            }

            foreach (var preloadedTerminalTile in preloadedTerminalTiles)
            {
                _objectPoolTerminalTiles.Release(preloadedTerminalTile);
            }
        }

        public NormalTerminalTileLeft Generate(int tileId)
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