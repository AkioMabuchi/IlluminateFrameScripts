using Models;
using VContainer;

namespace Processes
{
    public class ClearMainGameProcess
    {
        private readonly MainBoardModel _mainBoardModel;
        private readonly ScoreModel _scoreModel;
        private readonly TilesModel _tilesModel;
        private readonly CurrentTileModel _currentTileModel;
        private readonly NextTileModel _nextTileModel;

        [Inject]
        public ClearMainGameProcess(MainBoardModel mainBoardModel, ScoreModel scoreModel, TilesModel tilesModel,
            CurrentTileModel currentTileModel, NextTileModel nextTileModel)
        {
            _mainBoardModel = mainBoardModel;
            _scoreModel = scoreModel;
            _tilesModel = tilesModel;
            _currentTileModel = currentTileModel;
            _nextTileModel = nextTileModel;
        }

        public void ClearMainGame()
        {
            _mainBoardModel.ClearBoard();
            _scoreModel.ResetScore();
            _tilesModel.ClearTiles();
            _currentTileModel.ResetCurrentTileId();
            _nextTileModel.ResetNextTileId();
        }
    }
}