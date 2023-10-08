using Models;
using VContainer;
using Views;
using Views.Controllers;

namespace Processes
{
    public class ResetMainGameProcess
    {
        private readonly CurrentTileModel _currentTileModel;
        private readonly LineCountsModel _lineCountsModel;
        private readonly MainBoardModel _mainBoardModel;
        private readonly MainFrameModel _mainFrameModel;
        private readonly NextTileModel _nextTileModel;
        private readonly ScoreModel _scoreModel;
        private readonly TileDeckModel _tileDeckModel;
        private readonly TileRestAmountModel _tileRestAmountModel;

        [Inject]
        public ResetMainGameProcess(CurrentTileModel currentTileModel, LineCountsModel lineCountsModel,
            MainBoardModel mainBoardModel, MainFrameModel mainFrameModel, NextTileModel nextTileModel,
            ScoreModel scoreModel, TileDeckModel tileDeckModel, TileRestAmountModel tileRestAmountModel)
        {
            _currentTileModel = currentTileModel;
            _lineCountsModel = lineCountsModel;
            _mainBoardModel = mainBoardModel;
            _mainFrameModel = mainFrameModel;
            _nextTileModel = nextTileModel;
            _scoreModel = scoreModel;
            _tileDeckModel = tileDeckModel;
            _tileRestAmountModel = tileRestAmountModel;
        }

        public void ResetMainGame()
        {
            _currentTileModel.Reset();
            _lineCountsModel.Reset();
            _mainBoardModel.Reset();
            _mainFrameModel.FrameModel.Initialize();
            _nextTileModel.Reset();
            _scoreModel.Reset();
            _tileDeckModel.Reset();
            _tileRestAmountModel.Reset();
        }
    }
}