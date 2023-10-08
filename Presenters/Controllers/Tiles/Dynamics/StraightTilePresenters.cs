using System.Collections.Generic;
using Models.Instances.Tiles.Dynamics;
using Presenters.Instances.Tiles.Dynamics;
using Views.Instances.Tiles.Dynamics;

namespace Presenters.Controllers.Tiles.Dynamics
{
    public class StraightTilePresenters
    {
        private readonly Dictionary<int, StraightTilePresenter> _straightTilePresenters = new();

        public void Add(int tileId, StraightTileModel straightTileModel, StraightTile straightTile)
        {
            _straightTilePresenters.Add(tileId, new StraightTilePresenter(straightTileModel, straightTile));
        }

        public void Clear()
        {
            foreach (var straightTilePresenter in _straightTilePresenters.Values)
            {
                straightTilePresenter.CompositeDispose();
            }
            
            _straightTilePresenters.Clear();
        }
    }
}