using System.Collections.Generic;
using Models.Instances.Tiles.Terminals;
using Presenters.Instances.Tiles.Terminals;
using Views.Instances.Tiles.Terminals;

namespace Presenters.Controllers.Tiles.Terminals
{
    public class PlusTerminalTilePresenters
    {
        private readonly Dictionary<int, PlusTerminalTilePresenter> _plusTerminalTilePresenters = new();

        public void Add(int tileId, PlusTerminalTileModel plusTerminalTileModel, PlusTerminalTile plusTerminalTile)
        {
            _plusTerminalTilePresenters.Add(tileId,
                new PlusTerminalTilePresenter(plusTerminalTileModel, plusTerminalTile));
        }

        public void Clear()
        {
            foreach (var plusTerminalTilePresenter in _plusTerminalTilePresenters.Values)
            {
                plusTerminalTilePresenter.CompositeDispose();
            }

            _plusTerminalTilePresenters.Clear();
        }
    }
}