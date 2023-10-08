using System.Collections.Generic;
using Models.Instances.Tiles.Powers;
using Presenters.Instances.Tiles.Powers;
using Views.Instances.Tiles.Powers;

namespace Presenters.Controllers.Tiles.Powers
{
    public class PlusPowerTilePresenters
    {
        private readonly Dictionary<int, PlusPowerTilePresenter> _plusPowerTilePresenters = new();

        public void Add(int tileId, PlusPowerTileModel plusPowerTileModel, PlusPowerTile plusPowerTile)
        {
            _plusPowerTilePresenters.Add(tileId, new PlusPowerTilePresenter(plusPowerTileModel, plusPowerTile));
        }

        public void Clear()
        {
            foreach (var plusPowerTilePresenter in _plusPowerTilePresenters.Values)
            {
                plusPowerTilePresenter.CompositeDispose();
            }
            
            _plusPowerTilePresenters.Clear();
        }
    }
}