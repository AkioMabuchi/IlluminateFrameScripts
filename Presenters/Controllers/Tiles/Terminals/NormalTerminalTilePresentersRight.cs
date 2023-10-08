using System.Collections.Generic;
using Models.Instances.Tiles.Terminals;
using Presenters.Instances.Tiles.Terminals;
using Views.Instances.Tiles.Terminals;

namespace Presenters.Controllers.Tiles.Terminals
{
    public class NormalTerminalTilePresentersRight
    {
        private readonly Dictionary<int, NormalTerminalTilePresenterRight> _normalTerminalTilePresentersRight = new();

        public void Add(int tileId, NormalTerminalTileModelRight normalTerminalTileModelRight,
            NormalTerminalTileRight normalTerminalTileRight)
        {
            _normalTerminalTilePresentersRight.Add(tileId,
                new NormalTerminalTilePresenterRight(normalTerminalTileModelRight, normalTerminalTileRight));
        }

        public void Clear()
        {
            foreach (var normalTerminalTilePresenterRight in _normalTerminalTilePresentersRight.Values)
            {
                normalTerminalTilePresenterRight.CompositeDispose();
            }

            _normalTerminalTilePresentersRight.Clear();
        }
    }
}