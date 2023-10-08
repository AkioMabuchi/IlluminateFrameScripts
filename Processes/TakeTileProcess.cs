using Models;
using VContainer;

namespace Processes
{
    public class TakeTileProcess
    {
        private readonly CurrentTileModel _currentTileModel;

        [Inject]
        public TakeTileProcess(CurrentTileModel currentTileModel)
        {
            _currentTileModel = currentTileModel;
        }

        public void TakeTile(int tileId)
        {
            _currentTileModel.SetCurrentTileId(tileId);
        }
    }
}