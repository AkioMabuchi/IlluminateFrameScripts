using Cysharp.Threading.Tasks;
using Models;
using Processes;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace EventHandlers
{
    public class OnMouseButtonLeftPressedForPutCurrentTile : ITickable
    {
        private readonly SelectedBoardCellModel _selectedBoardCellModel;
        private readonly CurrentTileModel _currentTileModel;
        private readonly TilesModel _tilesModel;
        private readonly ValidCellPositionsModel _validCellPositionsModel;
        
        private readonly MainBoardProcess _mainBoardProcess;
        
        private readonly PutTileOnFrameProcess _putTileOnFrameProcess;


        [Inject]
        public OnMouseButtonLeftPressedForPutCurrentTile(SelectedBoardCellModel selectedBoardCellModel,
            CurrentTileModel currentTileModel, TilesModel tilesModel, ValidCellPositionsModel validCellPositionsModel,
            MainBoardProcess mainBoardProcess, PutTileOnFrameProcess putTileOnFrameProcess)
        {
            _selectedBoardCellModel = selectedBoardCellModel;
            _currentTileModel = currentTileModel;
            _tilesModel = tilesModel;
            _validCellPositionsModel = validCellPositionsModel;
            
            _mainBoardProcess = mainBoardProcess;
            _putTileOnFrameProcess = putTileOnFrameProcess;
        }

        public void Tick()
        {
            if (Mouse.current == null)
            {
                return;
            }
            
            if (Mouse.current.leftButton.wasPressedThisFrame &&
                _selectedBoardCellModel.SelectedBoardCell.HasValue &&
                _currentTileModel.CurrentTileId.HasValue)
            {
                var selectedPanelCellPosition = _selectedBoardCellModel.SelectedBoardCell.Value;
                var currentTileId = _currentTileModel.CurrentTileId.Value;
                var currentTileModel = _tilesModel.GetTileModel(currentTileId);
                var currentTileType = currentTileModel.TileType;
                var currentTileRotateStatus = currentTileModel.RotateStatus;
                if (_validCellPositionsModel.CanPutTile(currentTileType, currentTileRotateStatus,
                        selectedPanelCellPosition))
                {
                    _putTileOnFrameProcess.PutTileOnBoard(selectedPanelCellPosition, currentTileId);
                
                    _currentTileModel.ResetCurrentTileId();
                    _mainBoardProcess.AsyncMainPanelBoardProcess().Forget();
                }
            }
        }
    }
}