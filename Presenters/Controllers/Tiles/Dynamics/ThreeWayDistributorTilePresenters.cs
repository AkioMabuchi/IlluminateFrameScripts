using System.Collections.Generic;
using Models.Instances.Tiles.Dynamics;
using Presenters.Instances.Tiles.Dynamics;
using Views.Instances.Tiles.Dynamics;

namespace Presenters.Controllers.Tiles.Dynamics
{
    public class ThreeWayDistributorTilePresenters
    {
        private readonly Dictionary<int, ThreeWayDistributorTilePresenter> _threeWayDistributorTilePresenters = new();

        public void Add(int tileId, ThreeWayDistributorTileModel threeWayDistributorTileModel,
            ThreeWayDistributorTile threeWayDistributorTile)
        {
            _threeWayDistributorTilePresenters.Add(tileId,
                new ThreeWayDistributorTilePresenter(threeWayDistributorTileModel, threeWayDistributorTile));
        }

        public void Clear()
        {
            foreach (var threeWayDistributorTilePresenter in _threeWayDistributorTilePresenters.Values)
            {
                threeWayDistributorTilePresenter.CompositeDispose();
            }
            
            _threeWayDistributorTilePresenters.Clear();
        }
    }
}