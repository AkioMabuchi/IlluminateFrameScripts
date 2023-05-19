using System;
using Cysharp.Threading.Tasks;
using Enums;
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

        private readonly Footer _footer;

        private readonly BannerShorted _bannerShorted;
        private readonly BannerClosed _bannerClosed;
        
        private readonly TextEffectFactory _textEffectFactory;
        private readonly TileFactory _tileFactory;
        
        private readonly AddScoreProcess _addScoreProcess;
        private readonly ConductMainBoardProcess _conductMainBoardProcess;
        private readonly DecreaseTileRestAmountProcess _decreaseTileRestAmountProcess;
        private readonly EndMainGameProcess _endMainGameProcess;
        private readonly TakeTileProcess _takeTileProcess;
        private readonly PrepareNextTileProcess _prepareNextTileProcess;
        private readonly UpdateValidCellPositionsProcess _updateValidCellPositionsProcess;
        private readonly IlluminateBoardProcess _illuminateBoardProcess;

        [Inject]
        public MainBoardProcess(TilesModel tilesModel, TileDeckModel tileDeckModel,
            TileRestAmountModel tileRestAmountModel, NextTileModel nextTileModel,
            ValidCellPositionsModel validCellPositionsModel, Footer footer, BannerShorted bannerShorted, BannerClosed bannerClosed,
            TextEffectFactory textEffectFactory, TileFactory tileFactory, AddScoreProcess addScoreProcess,
            ConductMainBoardProcess conductMainBoardProcess,
            DecreaseTileRestAmountProcess decreaseTileRestAmountProcess, EndMainGameProcess endMainGameProcess,
            TakeTileProcess takeTileProcess, PrepareNextTileProcess prepareNextTileProcess,
            IlluminateBoardProcess illuminateBoardProcess,
            UpdateValidCellPositionsProcess updateValidCellPositionsProcess)
        {
            _nextTileModel = nextTileModel;
            _tilesModel = tilesModel;
            _tileDeckModel = tileDeckModel;
            _tileRestAmountModel = tileRestAmountModel;
            _validCellPositionsModel = validCellPositionsModel;

            _footer = footer;
            _bannerShorted = bannerShorted;
            _bannerClosed = bannerClosed;
            _textEffectFactory = textEffectFactory;
            _tileFactory = tileFactory;

            _addScoreProcess = addScoreProcess;
            _conductMainBoardProcess = conductMainBoardProcess;
            _decreaseTileRestAmountProcess = decreaseTileRestAmountProcess;
            _endMainGameProcess = endMainGameProcess;
            
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

            // 短絡で終了
            if (boardConductionResult.isCircuitShorted)
            {
                _footer.ChangeFootingText(FooterFootingText.None);
                _bannerShorted.Show();
                await UniTask.Delay(TimeSpan.FromSeconds(5.0));
                _bannerShorted.FadeOut();
                await UniTask.Delay(TimeSpan.FromSeconds(2.0));
                await _endMainGameProcess.EndMainGame();
                
                return;
            }

            await _illuminateBoardProcess.IlluminateBoard(boardConductionResult.illuminatePaths);

            // タイルがなくなり終了
            if (_tileRestAmountModel.IsRunOUt)
            {
                _footer.ChangeFootingText(FooterFootingText.None);
                await _endMainGameProcess.EndMainGame();
                return;
            }

            // 回路が閉鎖して終了する
            if (boardConductionResult.isCircuitClosed)
            {
                _footer.ChangeFootingText(FooterFootingText.None);
                _bannerClosed.ShowUp();
                await UniTask.Delay(TimeSpan.FromSeconds(7.0));
                _bannerClosed.FadeOut();
                await UniTask.Delay(TimeSpan.FromSeconds(3.0));
                await _endMainGameProcess.EndMainGame();
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
                        // 配置不可能になって、終了
                        _footer.ChangeFootingText(FooterFootingText.None);
                        await _endMainGameProcess.EndMainGame();
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
                    // 残りのタイルも配置不可能で終了
                    else
                    {
                        _footer.ChangeFootingText(FooterFootingText.None);
                        await _endMainGameProcess.EndMainGame();
                    }
                }
            }
        }
    }
}