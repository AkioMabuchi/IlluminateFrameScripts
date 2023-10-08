using System.Collections.Generic;
using Models.Instances.Tiles.Dynamics;
using Presenters.Instances.Tiles.Dynamics;
using Views.Instances.Tiles.Dynamics;

namespace Presenters.Controllers.Tiles.Dynamics
{
    public class FourWayDistributorTilePresenters
    {
        private readonly Dictionary<int, FourWayDistributorPresenter> _fourWayDistributorPresenters = new();

        public void Add(int tileId, FourWayDistributorTileModel fourWayDistributorTileModel,
            FourWayDistributorTile fourWayDistributorTile)
        {
            _fourWayDistributorPresenters.Add(tileId,
                new FourWayDistributorPresenter(fourWayDistributorTileModel, fourWayDistributorTile));
        }

        public void Clear()
        {
            foreach (var fourWayDistributorTilePresenter in _fourWayDistributorPresenters.Values)
            {
                fourWayDistributorTilePresenter.CompositeDispose();
            }
            
            _fourWayDistributorPresenters.Clear();
        }
    }
}