using System.Collections.Generic;
using Models.Instances.Tiles.Terminals;
using Presenters.Instances.Tiles.Terminals;
using Views.Instances.Tiles.Terminals;

namespace Presenters.Controllers.Tiles.Terminals
{
    public class NormalTerminalTilePresentersLeft
    {
        private readonly Dictionary<int, NormalTerminalTilePresenterLeft> _normalTerminalTilePresentersLeft = new();

        public void Add(int tileId, NormalTerminalTileModelLeft normalTerminalTileModelLeft,
            NormalTerminalTileLeft normalTerminalTileLeft)
        {
            _normalTerminalTilePresentersLeft.Add(tileId,
                new NormalTerminalTilePresenterLeft(normalTerminalTileModelLeft, normalTerminalTileLeft));
        }

        public void Clear()
        {
            foreach (var normalTerminalTilePresenterLeft in _normalTerminalTilePresentersLeft.Values)
            {
                normalTerminalTilePresenterLeft.CompositeDispose();
            }

            _normalTerminalTilePresentersLeft.Clear();
        }
    }
}