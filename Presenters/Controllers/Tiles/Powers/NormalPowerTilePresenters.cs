using System.Collections.Generic;
using Models.Instances.Tiles.Powers;
using Presenters.Instances.Tiles.Powers;
using Views.Instances.Tiles.Powers;

namespace Presenters.Controllers.Tiles.Powers
{
    public class NormalPowerTilePresenters
    {
        private readonly Dictionary<int, NormalPowerTilePresenter> _normalPowerTilePresenters = new();

        public void Add(int tileId, NormalPowerTileModel normalPowerTileModel, NormalPowerTile normalPowerTile)
        {
            _normalPowerTilePresenters.Add(tileId, new NormalPowerTilePresenter(normalPowerTileModel, normalPowerTile));
        }

        public void Clear()
        {
            foreach (var normalPowerTilePresenter in _normalPowerTilePresenters.Values)
            {
                normalPowerTilePresenter.CompositeDispose();
            }
            
            _normalPowerTilePresenters.Clear();
        }
    }
}