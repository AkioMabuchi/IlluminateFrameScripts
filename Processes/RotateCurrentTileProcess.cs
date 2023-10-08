using Models;
using VContainer;
using Views;
using Views.Controllers;

namespace Processes
{
    public class RotateCurrentTileProcess
    {
        private readonly CurrentTileModel _currentTileModel;
        private readonly TileRotator _tileRotator;

        private readonly SoundPlayer _soundPlayer;

        [Inject]
        public RotateCurrentTileProcess(CurrentTileModel currentTileModel, TileRotator tileRotator,
            SoundPlayer soundPlayer)
        {
            _currentTileModel = currentTileModel;
            _tileRotator = tileRotator;

            _soundPlayer = soundPlayer;
        }

        public void RotateCurrentTile()
        {
            _currentTileModel.Rotate();
            if (_currentTileModel.TryGetCurrentTileId(out var tileId))
            {
                _tileRotator.RotateTile(tileId);
                _soundPlayer.PlayRotateTileSound();
            }
        }
    }
}
