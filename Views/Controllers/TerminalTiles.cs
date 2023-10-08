using System.Collections.Generic;
using Interfaces.Tiles;

namespace Views.Controllers
{
    public class TerminalTiles
    {
        private readonly Dictionary<int, ITerminalTile> _terminalTiles = new();

        public void AddTile(int tileId, ITerminalTile terminalTile)
        {
            _terminalTiles.Add(tileId, terminalTile);
        }

        public void IlluminateTileRadiantly(int tileId)
        {
            if (_terminalTiles.TryGetValue(tileId, out var terminalTile))
            {
                terminalTile.IlluminateRadiantly();
            }
        }

        public void IlluminateTile(int tileId)
        {
            if (_terminalTiles.TryGetValue(tileId, out var terminalTile))
            {
                terminalTile.Illuminate();
            }
        }

        public void ClearAllTiles()
        {
            _terminalTiles.Clear();
        }
    }
}