using System.Collections.Generic;
using Models.Instances.Tiles.Terminals;
using Presenters.Instances.Tiles.Terminals;
using Views.Instances.Tiles.Terminals;

namespace Presenters.Controllers.Tiles.Terminals
{
    public class MinusTerminalTilePresenters
    {
        private readonly Dictionary<int, MinusTerminalTilePresenter> _minusTerminalTilePresenters = new();

        public void Add(int tileId, MinusTerminalTileModel minusTerminalTileModel, MinusTerminalTile minusTerminalTile)
        {
            _minusTerminalTilePresenters.Add(tileId,
                new MinusTerminalTilePresenter(minusTerminalTileModel, minusTerminalTile));
        }

        public void Clear()
        {
            foreach (var minusTerminalTilePresenter in _minusTerminalTilePresenters.Values)
            {
                minusTerminalTilePresenter.CompositeDispose();
            }

            _minusTerminalTilePresenters.Clear();
        }
    }
}