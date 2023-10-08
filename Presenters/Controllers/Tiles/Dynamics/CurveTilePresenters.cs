using System.Collections.Generic;
using Models.Instances.Tiles.Dynamics;
using Presenters.Instances.Tiles.Dynamics;
using Views.Instances.Tiles.Dynamics;

namespace Presenters.Controllers.Tiles.Dynamics
{
    public class CurveTilePresenters
    {
        private readonly Dictionary<int, CurveTilePresenter> _curveTilePresenters = new();

        public void Add(int tileId, CurveTileModel curveTileModel, CurveTile curveTile)
        {
            _curveTilePresenters.Add(tileId, new CurveTilePresenter(curveTileModel, curveTile));
        }

        public void Clear()
        {
            foreach (var curveTilePresenter in _curveTilePresenters.Values)
            {
                curveTilePresenter.CompositeDispose();
            }
            
            _curveTilePresenters.Clear();
        }
    }
}