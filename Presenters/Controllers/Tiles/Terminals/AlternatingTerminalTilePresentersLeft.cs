using System.Collections.Generic;
using Models.Instances.Tiles.Terminals;
using Presenters.Instances.Tiles.Terminals;
using Views.Instances.Tiles.Terminals;

namespace Presenters.Controllers.Tiles.Terminals
{
    public class AlternatingTerminalTilePresentersLeft
    {
        private readonly Dictionary<int, AlternatingTerminalTilePresenterLeft> _alternatingTerminalTilePresentersLeft =
            new();

        public void Add(int tileId, AlternatingTerminalTileModelLeft alternatingTerminalTileModelLeft,
            AlternatingTerminalTileLeft alternatingTerminalTileLeft)
        {
            _alternatingTerminalTilePresentersLeft.Add(tileId,
                new AlternatingTerminalTilePresenterLeft(alternatingTerminalTileModelLeft, alternatingTerminalTileLeft));
        }

        public void Clear()
        {
            foreach (var alternatingTerminalTilePresenterLeft in _alternatingTerminalTilePresentersLeft.Values)
            {
                alternatingTerminalTilePresenterLeft.CompositeDispose();
            }
            
            _alternatingTerminalTilePresentersLeft.Clear();
        }
    }
}