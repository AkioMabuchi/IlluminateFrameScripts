using Models;
using UnityEngine;
using VContainer;
using Views;

namespace Processes
{
    public class SelectBoardCellProcess
    {
        private readonly SelectedBoardCellModel _selectedBoardCellModel;
        private readonly MainBoardModel _mainBoardModel;
        private readonly CurrentTileModel _currentTileModel;
        private readonly TilesModel _tilesModel;
        private readonly ValidCellPositionsModel _validCellPositionsModel;

        private readonly BoardCellPointer _boardCellPointer;

        [Inject]
        public SelectBoardCellProcess(SelectedBoardCellModel selectedBoardCellModel, MainBoardModel mainBoardModel,
            CurrentTileModel currentTileModel, TilesModel tilesModel, ValidCellPositionsModel validCellPositionsModel,
            BoardCellPointer boardCellPointer)
        {
            _selectedBoardCellModel = selectedBoardCellModel;
            _mainBoardModel = mainBoardModel;
            _currentTileModel = currentTileModel;
            _tilesModel = tilesModel;
            
            _validCellPositionsModel = validCellPositionsModel;
            _boardCellPointer = boardCellPointer;
        }

        public void SelectPanelCell(Vector2Int cellPosition)
        {
            if (_mainBoardModel.GetPutTileId(cellPosition).HasValue)
            {
                _selectedBoardCellModel.SetSelectedPanelCellNull();
                _boardCellPointer.EnableMeshRenderer(false);
            }
            else
            {
                _selectedBoardCellModel.SetSelectedPanelCell(cellPosition);
                
                _boardCellPointer.MoveToCellPosition(cellPosition);

                if (_currentTileModel.CurrentTileId.HasValue)
                {
                    var currentTileId = _currentTileModel.CurrentTileId.Value;
                    var currentTileModel = _tilesModel.GetTileModel(currentTileId);
                    var tileType = currentTileModel.TileType;
                    var rotateStatus = _tilesModel.GetTileModel(currentTileId).RotateStatus;
                    if (_validCellPositionsModel.CanPutTile(tileType, rotateStatus, cellPosition))
                    {
                        _boardCellPointer.EnableMeshRenderer(true);
                    }
                    else
                    {
                        _boardCellPointer.EnableMeshRenderer(false);
                    }

                }
                else
                {
                    _boardCellPointer.EnableMeshRenderer(false);
                }
            }
        }

        public void DeselectPanelCell()
        {
            _selectedBoardCellModel.SetSelectedPanelCellNull();
            _boardCellPointer.EnableMeshRenderer(false);
        }
    }
}