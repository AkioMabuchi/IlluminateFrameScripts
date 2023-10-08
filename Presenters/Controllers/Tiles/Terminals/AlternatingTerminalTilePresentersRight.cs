using System.Collections.Generic;
using Models.Instances.Tiles.Terminals;
using Presenters.Instances.Tiles.Terminals;
using Views.Instances.Tiles.Terminals;

namespace Presenters.Controllers.Tiles.Terminals
{
    public class AlternatingTerminalTilePresentersRight
    {
        private readonly Dictionary<int, AlternatingTerminalTilePresenterRight>
            _alternatingTerminalTilePresentersRight = new();

        public void Add(int tileId, AlternatingTerminalTileModelRight alternatingTerminalTileModelRight,
            AlternatingTerminalTileRight alternatingTerminalTileRight)
        {
            _alternatingTerminalTilePresentersRight.Add(tileId,
                new AlternatingTerminalTilePresenterRight(alternatingTerminalTileModelRight,
                    alternatingTerminalTileRight));
        }

        public void Clear()
        {
            foreach (var alternatingTerminalTilePresenterRight in _alternatingTerminalTilePresentersRight.Values)
            {
                alternatingTerminalTilePresenterRight.CompositeDispose();
            }

            _alternatingTerminalTilePresentersRight.Clear();
        }
    }
}