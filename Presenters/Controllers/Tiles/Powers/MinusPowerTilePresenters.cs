using System.Collections.Generic;
using Models.Instances.Tiles.Powers;
using Presenters.Instances.Tiles.Powers;
using Views.Instances.Tiles.Powers;

namespace Presenters.Controllers.Tiles.Powers
{
    public class MinusPowerTilePresenters
    {
        private readonly Dictionary<int, MinusPowerTilePresenter> _minusPowerTilePresenters = new();

        public void Add(int tileId, MinusPowerTileModel minusPowerTileModel, MinusPowerTile minusPowerTile)
        {
            _minusPowerTilePresenters.Add(tileId, new MinusPowerTilePresenter(minusPowerTileModel, minusPowerTile));
        }

        public void Clear()
        {
            foreach (var minusPowerTilePresenter in _minusPowerTilePresenters.Values)
            {
                minusPowerTilePresenter.CompositeDispose();
            }
            
            _minusPowerTilePresenters.Clear();
        }
    }
}