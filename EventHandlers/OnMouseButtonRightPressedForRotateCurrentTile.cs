using Models;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnMouseButtonRightPressedForRotateCurrentTile : ITickable
    {
        private readonly CurrentTileModel _currentTileModel;
        private readonly TilesModel _tilesModel;
        private readonly TileFactory _tileFactory;

        [Inject]
        public OnMouseButtonRightPressedForRotateCurrentTile(CurrentTileModel currentTileModel, TilesModel tilesModel,
            TileFactory tileFactory)
        {
            _currentTileModel = currentTileModel;
            _tilesModel = tilesModel;
            _tileFactory = tileFactory;
        }

        public void Tick()
        {
            if (Mouse.current == null)
            {
                return;
            }

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                if (_currentTileModel.CurrentTileId.HasValue)
                {
                    var currentTileId = _currentTileModel.CurrentTileId.Value;

                    _tilesModel.RotateTile(currentTileId);
                }
                
                _tileFactory.RotateCurrentTile();
            }
        }
    }
}
