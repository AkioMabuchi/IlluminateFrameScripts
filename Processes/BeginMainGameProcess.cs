using Enums;
using Models;
using Views;
using Views.Controllers;
using Views.Screens.Prior;

namespace Processes
{
    public class BeginMainGameProcess
    {
        private readonly GameStateModel _gameStateModel;

        private readonly MainBoardModel _mainBoardModel;

        private readonly TileDeckModel _tileDeckModel;
        private readonly TileRestAmountModel _tileRestAmountModel;

        private readonly ValidCellPositionsModel _validCellPositionsModel;

        private readonly CurrentTileModel _currentTileModel;
        private readonly NextTileModel _nextTileModel;

        private readonly CurrentTilePositioner _currentTilePositioner;
        private readonly NextTilePositioner _nextTilePositioner;
        private readonly TileRenderer _tileRenderer;
        
        private readonly Footer _footer;

        private readonly Desk _desk;

        private readonly AddTileProcess _addTileProcess;

        public BeginMainGameProcess(GameStateModel gameStateModel, MainBoardModel mainBoardModel,
            TileDeckModel tileDeckModel, TileRestAmountModel tileRestAmountModel,
            ValidCellPositionsModel validCellPositionsModel, CurrentTileModel currentTileModel,
            NextTileModel nextTileModel, CurrentTilePositioner currentTilePositioner,
            NextTilePositioner nextTilePositioner, TileRenderer tileRenderer, Footer footer, Desk desk,
            AddTileProcess addTileProcess)
        {
            _gameStateModel = gameStateModel;

            _mainBoardModel = mainBoardModel;

            _tileDeckModel = tileDeckModel;
            _tileRestAmountModel = tileRestAmountModel;

            _validCellPositionsModel = validCellPositionsModel;

            _currentTileModel = currentTileModel;
            _nextTileModel = nextTileModel;

            _currentTilePositioner = currentTilePositioner;
            _nextTilePositioner = nextTilePositioner;

            _tileRenderer = tileRenderer;
            _footer = footer;

            _desk = desk;

            _addTileProcess = addTileProcess;
        }

        public void BeginMainGame()
        {
            _gameStateModel.SetGameStateName(GameStateName.Main);
            _footer.RenderText();

            _tileRestAmountModel.DecreaseTileRestAmount();
            _desk.ValueDisplayScore.DrawImmediate();
            _desk.ValueDisplayTileRestAmount.DrawImmediate();

            _validCellPositionsModel.SetValidCellPositions(_mainBoardModel.ValidCellPositions);
            
            var (tileIdCurrent, tileModelCurrent) = _addTileProcess.AddTile(_tileDeckModel.TakeTile());
            var (tileIdNext, tileModelNext) = _addTileProcess.AddTile(_tileDeckModel.TakeTile());

            _currentTileModel.SetCurrentTileId(tileIdCurrent);
            _currentTileModel.SetCurrentTileModel(tileModelCurrent);
            
            _nextTileModel.SetTileId(tileIdNext);
            _nextTileModel.SetTileModel(tileModelNext);
            
            _currentTilePositioner.SetStartPosition(_nextTilePositioner.StartPosition);
            
            _currentTilePositioner.StartLerp();
            _nextTilePositioner.StartLerp();
            _tileRenderer.RenderAllTiles();
        }
    }
}