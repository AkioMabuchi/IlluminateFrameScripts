using Models;
using VContainer;

namespace Processes
{
    public class PrepareNextTileProcess
    {
        private readonly NextTileModel _nextTileModel;

        [Inject]
        public PrepareNextTileProcess(NextTileModel nextTileModel)
        {
            _nextTileModel = nextTileModel;
        }

        public void PrepareNextTile(int tileId)
        {
            _nextTileModel.SetTileId(tileId);
        }
    }
}