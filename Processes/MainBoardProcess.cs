using System;
using Cysharp.Threading.Tasks;
using Models;
using UnityEngine;
using VContainer;
using Views;

namespace Processes
{
    public class MainBoardProcess
    {
        private readonly TilesModel _tilesModel;
        private readonly TileDeckModel _tileDeckModel;
        private readonly TileRestAmountModel _tileRestAmountModel;
        private readonly NextTileModel _nextTileModel;
        private readonly ValidCellPositionsModel _validCellPositionsModel;

        private readonly BannerShorted _bannerShorted;
        private readonly BannerClosed _bannerClosed;
        
        private readonly TextEffectFactory _textEffectFactory;
        private readonly TileFactory _tileFactory;
        
        private readonly AddScoreProcess _addScoreProcess;
        private readonly ConductMainBoardProcess _conductMainBoardProcess;
        private readonly DecreaseTileRestAmountProcess _decreaseTileRestAmountProcess;
        private readonly TakeTileProcess _takeTileProcess;
        private readonly PrepareNextTileProcess _prepareNextTileProcess;
        private readonly UpdateValidCellPositionsProcess _updateValidCellPositionsProcess;
        private readonly IlluminateBoardProcess _illuminateBoardProcess;

        [Inject]
        public MainBoardProcess(TilesModel tilesModel, TileDeckModel tileDeckModel,
            TileRestAmountModel tileRestAmountModel,
            NextTileModel nextTileModel, ValidCellPositionsModel validCellPositionsModel, BannerShorted bannerShorted,
            BannerClosed bannerClosed, TextEffectFactory textEffectFactory, TileFactory tileFactory,
            AddScoreProcess addScoreProcess, ConductMainBoardProcess conductMainBoardProcess,
            DecreaseTileRestAmountProcess decreaseTileRestAmountProcess, TakeTileProcess takeTileProcess,
            PrepareNextTileProcess prepareNextTileProcess, IlluminateBoardProcess illuminateBoardProcess,
            UpdateValidCellPositionsProcess updateValidCellPositionsProcess)
        {
            _nextTileModel = nextTileModel;
            _tilesModel = tilesModel;
            _tileDeckModel = tileDeckModel;
            _tileRestAmountModel = tileRestAmountModel;
            _validCellPositionsModel = validCellPositionsModel;

            _bannerShorted = bannerShorted;
            _bannerClosed = bannerClosed;
            _textEffectFactory = textEffectFactory;
            _tileFactory = tileFactory;

            _addScoreProcess = addScoreProcess;
            _conductMainBoardProcess = conductMainBoardProcess;
            _decreaseTileRestAmountProcess = decreaseTileRestAmountProcess;
            _takeTileProcess = takeTileProcess;
            _illuminateBoardProcess = illuminateBoardProcess;
            _prepareNextTileProcess = prepareNextTileProcess;
            _updateValidCellPositionsProcess = updateValidCellPositionsProcess;
        }

        public async UniTask AsyncMainPanelBoardProcess()
        {
            var boardConductionResult = _conductMainBoardProcess.ConductMainBoard();

            var totalScore = 0;
            foreach (var scoredTile in boardConductionResult.scoredTiles)
            {
                _textEffectFactory.GenerateTextEffectLineScore(scoredTile.cellPosition, scoredTile.electricStatus,
                    scoredTile.score);
                totalScore += scoredTile.score;
            }

            if (totalScore > 0)
            {
                _addScoreProcess.AddScore(totalScore);
            }

            if (boardConductionResult.isCircuitShorted)
            {
                _bannerShorted.Show();
                await UniTask.Delay(TimeSpan.FromSeconds(5.0));
                _bannerShorted.FadeOut();
                return;
            }

            await _illuminateBoardProcess.IlluminateBoard(boardConductionResult.illuminatePaths);

            if (_tileRestAmountModel.IsRunOUt)
            {
                Debug.Log("終了");
                return;
            }

            if (boardConductionResult.isCircuitClosed)
            {
                _bannerClosed.ShowUp();
                await UniTask.Delay(TimeSpan.FromSeconds(7.0));
                _bannerClosed.FadeOut();
                return;
            }

            _decreaseTileRestAmountProcess.DecreaseTileRestAmount();
            _updateValidCellPositionsProcess.UpdateValidCellPositions();

            if (_nextTileModel.NextTileId.HasValue)
            {
                var nextTileId = _nextTileModel.NextTileId.Value;
                if (_validCellPositionsModel.CanPutTileType(_tilesModel.GetTileModel(nextTileId).TileType))
                {
                    _takeTileProcess.TakeTile(nextTileId);
                    if (!_tileRestAmountModel.IsRunOUt)
                    {
                        _prepareNextTileProcess.PrepareNextTile(_tilesModel.AddTile(_tileDeckModel.TakeTile()));
                    }
                }
                else
                {
                    _textEffectFactory.GenerateTextEffectNoPlace();
                    _tileFactory.ThrowNextTile();
                    if (_tileRestAmountModel.IsRunOUt)
                    {
                        Debug.Log("最後が配置不能タイルだったため、終了");
                        return;
                    }

                    var nullable = _tileDeckModel.TakeValidTile(_validCellPositionsModel.ValidTiles);

                    if (nullable.HasValue)
                    {
                        var tileType = nullable.Value;
                        _decreaseTileRestAmountProcess.DecreaseTileRestAmount();
                        
                        _takeTileProcess.TakeTile(_tilesModel.AddTile(tileType));
                        if (!_tileRestAmountModel.IsRunOUt)
                        {
                            _prepareNextTileProcess.PrepareNextTile(_tilesModel.AddTile(_tileDeckModel.TakeTile()));
                        }
                    }
                    else
                    {
                        Debug.Log("残りのも配置不可能");
                    }
                }
            }
        }
    }
}