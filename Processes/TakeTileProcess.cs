using Models;
using VContainer;
using Views;

namespace Processes
{
    public class TakeTileProcess
    {
        private readonly CurrentTileModel _currentTileModel;
        private readonly TileFactory _tileFactory;

        [Inject]
        public TakeTileProcess(CurrentTileModel currentTileModel, TileFactory tileFactory)
        {
            _currentTileModel = currentTileModel;
            _tileFactory = tileFactory;
        }

        public void TakeTile(int tileId)
        {
            _currentTileModel.SetCurrentTileId(tileId);
            _tileFactory.SetBaseTilePosition();
            _tileFactory.TweenCurrentTile();
        }
    }
}