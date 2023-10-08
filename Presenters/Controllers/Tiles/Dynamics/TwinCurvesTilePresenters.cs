using System.Collections.Generic;
using Models.Instances.Tiles.Dynamics;
using Presenters.Instances.Tiles.Dynamics;
using Views.Instances.Tiles.Dynamics;

namespace Presenters.Controllers.Tiles.Dynamics
{
    public class TwinCurvesTilePresenters
    {
        private readonly Dictionary<int, TwinCurvesTilePresenter> _twinCurvesTilePresenters = new();

        public void Add(int tileId, TwinCurvesTileModel twinCurvesTileModel, TwinCurvesTile twinCurvesTile)
        {
            _twinCurvesTilePresenters.Add(tileId, new TwinCurvesTilePresenter(twinCurvesTileModel, twinCurvesTile));
        }

        public void Clear()
        {
            foreach (var twinCurvesTilePresenter in _twinCurvesTilePresenters.Values)
            {
                twinCurvesTilePresenter.CompositeDispose();
            }
            
            _twinCurvesTilePresenters.Clear();
        }
    }
}