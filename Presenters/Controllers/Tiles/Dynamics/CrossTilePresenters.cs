using System.Collections.Generic;
using Models.Instances.Tiles.Dynamics;
using Presenters.Instances.Tiles.Dynamics;
using Views.Instances.Tiles.Dynamics;

namespace Presenters.Controllers.Tiles.Dynamics
{
    public class CrossTilePresenters
    {
        private readonly Dictionary<int, CrossTilePresenter> _crossTilePresenters = new();

        public void Add(int tileId, CrossTileModel crossTileModel, CrossTile crossTile)
        {
            _crossTilePresenters.Add(tileId, new CrossTilePresenter(crossTileModel, crossTile));
        }

        public void Clear()
        {
            foreach (var crossTilePresenter in _crossTilePresenters.Values)
            {
                crossTilePresenter.CompositeDispose();
            }
            
            _crossTilePresenters.Clear();
        }
    }
}