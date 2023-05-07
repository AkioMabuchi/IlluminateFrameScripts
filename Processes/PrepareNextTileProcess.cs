using Models;
using VContainer;
using Views;

namespace Processes
{
    public class PrepareNextTileProcess
    {
        private readonly NextTileModel _nextTileModel;
        private readonly TileFactory _tileFactory;

        [Inject]
        public PrepareNextTileProcess(NextTileModel nextTileModel, TileFactory tileFactory)
        {
            _nextTileModel = nextTileModel;
            _tileFactory = tileFactory;
        }

        public void PrepareNextTile(int tileId)
        {
            _nextTileModel.SetNextTileTid(tileId);
            _tileFactory.TweenAndMoveNextTile();
        }
    }
}