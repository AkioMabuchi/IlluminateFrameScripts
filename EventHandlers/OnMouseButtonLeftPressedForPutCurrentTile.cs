using Cysharp.Threading.Tasks;
using Models;
using Parameters.Classes.Statics;
using Processes;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnMouseButtonLeftPressedForPutCurrentTile : ITickable
    {
        private readonly SelectedBoardCellModel _selectedBoardCellModel;
        private readonly CurrentTileModel _currentTileModel;
        private readonly TilesModel _tilesModel;
        private readonly ValidCellPositionsModel _validCellPositionsModel;
        
        private readonly MainPanelBoardProcess _mainPanelBoardProcess;
        
        private readonly PutTileOnThePanelProcess _putTileOnThePanelProcess;


        [Inject]
        public OnMouseButtonLeftPressedForPutCurrentTile(SelectedBoardCellModel selectedBoardCellModel,
            CurrentTileModel currentTileModel, TilesModel tilesModel, ValidCellPositionsModel validCellPositionsModel,
            MainPanelBoardProcess mainPanelBoardProcess, PutTileOnThePanelProcess putTileOnThePanelProcess)
        {
            _selectedBoardCellModel = selectedBoardCellModel;
            _currentTileModel = currentTileModel;
            _tilesModel = tilesModel;
            _validCellPositionsModel = validCellPositionsModel;
            
            _mainPanelBoardProcess = mainPanelBoardProcess;
            _putTileOnThePanelProcess = putTileOnThePanelProcess;
        }

        public void Tick()
        {
            if (Mouse.current == null)
            {
                return;
            }
            
            if (Mouse.current.leftButton.wasPressedThisFrame &&
                _selectedBoardCellModel.SelectedPanelCell.HasValue &&
                _currentTileModel.CurrentTileId.HasValue)
            {
                var selectedPanelCellPosition = _selectedBoardCellModel.SelectedPanelCell.Value;
                var currentTileId = _currentTileModel.CurrentTileId.Value;
                var currentTileModel = _tilesModel.GetTileModel(currentTileId);
                var currentTileType = currentTileModel.TileType;
                var currentTileRotateStatus = currentTileModel.RotateStatus;
                if (_validCellPositionsModel.CanPutTile(currentTileType, currentTileRotateStatus,
                        selectedPanelCellPosition))
                {
                    _putTileOnThePanelProcess.PutTileOnThePanel(selectedPanelCellPosition, currentTileId);
                
                    _currentTileModel.ResetCurrentTileId();
                    _mainPanelBoardProcess.AsyncMainPanelBoardProcess().Forget();
                }
            }
        }
    }
}