using System.Collections.Generic;
using Models.Instances.Tiles.Dynamics;
using Presenters.Instances.Tiles.Dynamics;
using Views.Instances.Tiles.Dynamics;

namespace Presenters.Controllers.Tiles.Dynamics
{
    public class BulbTilePresenters
    {
        private readonly Dictionary<int, BulbTilePresenter> _bulbTilePresenters = new();

        public void Add(int tileId, BulbTileModel bulbTileModel, BulbTile bulbTile)
        {
            _bulbTilePresenters.Add(tileId, new BulbTilePresenter(bulbTileModel, bulbTile));
        }

        public void Clear()
        {
            foreach (var bulbTilePresenter in _bulbTilePresenters.Values)
            {
                bulbTilePresenter.CompositeDispose();
            }
            
            _bulbTilePresenters.Clear();
        }
    }
}