using System.Collections.Generic;
using Models.Instances.Tiles.Powers;
using Presenters.Instances.Tiles.Powers;
using Views.Instances.Tiles.Powers;

namespace Presenters.Controllers.Tiles.Powers
{
    public class AlternatingPowerTilePresenters
    {
        private readonly Dictionary<int, AlternatingPowerTilePresenter> _alternatingPowerTilePresenters = new();

        public void Add(int tileId, AlternatingPowerTileModel alternatingPowerTileModel,
            AlternatingPowerTile alternatingPowerTile)
        {
            _alternatingPowerTilePresenters.Add(tileId,
                new AlternatingPowerTilePresenter(alternatingPowerTileModel, alternatingPowerTile));
        }

        public void Clear()
        {
            foreach (var alternatingPowerTilePresenter in _alternatingPowerTilePresenters.Values)
            {
                alternatingPowerTilePresenter.CompositeDispose();
            }
            
            _alternatingPowerTilePresenters.Clear();
        }
    }
}