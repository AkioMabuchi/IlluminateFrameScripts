using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Enums;
using Interfaces.Processes;
using Models;
using UnityEngine;
using VContainer;
using Views;
using Views.Banners;
using Views.Controllers;
using Views.Screens.Prior;
using Views.TextEffectFactories;

namespace Processes
{
    public class PutCurrentTileProcess
    {
        private readonly GameStateModel _gameStateModel;

        private readonly CurrentTileModel _currentTileModel;
        private readonly ScoreModel _scoreModel;
        private readonly SelectedBoardCellModel _selectedBoardCellModel;
        private readonly TileDeckModel _tileDeckModel;
        private readonly TileRestAmountModel _tileRestAmountModel;

        private readonly LineCountsModel _lineCountsModel;
        private readonly MainBoardModel _mainBoardModel;
        private readonly MainFrameModel _mainFrameModel;

        private readonly NextTileModel _nextTileModel;
        private readonly ValidCellPositionsModel _validCellPositionsModel;

        private readonly Desk _desk;
        private readonly ExplosionFactory _explosionFactory;
        private readonly Footer _footer;

        private readonly ClosedBanner _closedBanner;
        private readonly ExterminatedBanner _exterminatedBanner;
        private readonly FinishedBanner _finishedBanner;
        private readonly ShortedBanner _shortedBanner;

        private readonly ConductScoreTextEffectFactory _conductScoreTextEffectFactory;
        private readonly IlluminateScoreTextEffectFactory _illuminateScoreTextEffectFactory;
        private readonly LineCountTextEffectFactory _lineCountTextEffectFactory;
        private readonly LineScoreTextEffectFactory _lineScoreTextEffectFactory;
        private readonly NowhereTextEffectFactory _nowhereTextEffectFactory;

        private readonly BoardTilePositioner _boardTilePositioner;
        private readonly CurrentTilePositioner _currentTilePositioner;
        private readonly NextTilePositioner _nextTilePositioner;

        private readonly BulbTiles _bulbTiles;
        private readonly TerminalTiles _terminalTiles;
        
        private readonly TileMover _tileMover;
        private readonly TileRenderer _tileRenderer;
        private readonly TileThrower _tileThrower;

        private readonly MusicPlayer _musicPlayer;
        private readonly SoundPlayer _soundPlayer;

        private readonly AddTileProcess _addTileProcess;
        private readonly IEndMainGameProcess _endMainGameProcess;

        [Inject]
        public PutCurrentTileProcess(GameStateModel gameStateModel, CurrentTileModel currentTileModel,
            ScoreModel scoreModel, SelectedBoardCellModel selectedBoardCellModel, TileDeckModel tileDeckModel,
            TileRestAmountModel tileRestAmountModel, LineCountsModel lineCountsModel, MainBoardModel mainBoardModel,
            MainFrameModel mainFrameModel, NextTileModel nextTileModel, ValidCellPositionsModel validCellPositionsModel,
            Desk desk, ExplosionFactory explosionFactory, Footer footer, ClosedBanner closedBanner, ExterminatedBanner exterminatedBanner,
            FinishedBanner finishedBanner, ShortedBanner shortedBanner,
            ConductScoreTextEffectFactory conductScoreTextEffectFactory,
            IlluminateScoreTextEffectFactory illuminateScoreTextEffectFactory,
            LineCountTextEffectFactory lineCountTextEffectFactory,
            LineScoreTextEffectFactory lineScoreTextEffectFactory, NowhereTextEffectFactory nowhereTextEffectFactory,
            BoardTilePositioner boardTilePositioner, CurrentTilePositioner currentTilePositioner,
            NextTilePositioner nextTilePositioner, BulbTiles bulbTiles, TerminalTiles terminalTiles,
            TileMover tileMover, TileRenderer tileRenderer, TileThrower tileThrower, MusicPlayer musicPlayer,
            SoundPlayer soundPlayer, AddTileProcess addTileProcess, IEndMainGameProcess endMainGameProcess)
        {
            _gameStateModel = gameStateModel;

            _currentTileModel = currentTileModel;
            _scoreModel = scoreModel;
            _selectedBoardCellModel = selectedBoardCellModel;
            _tileDeckModel = tileDeckModel;
            _tileRestAmountModel = tileRestAmountModel;
            _lineCountsModel = lineCountsModel;
            _mainBoardModel = mainBoardModel;
            _mainFrameModel = mainFrameModel;
            _nextTileModel = nextTileModel;
            _validCellPositionsModel = validCellPositionsModel;
            _desk = desk;
            _explosionFactory = explosionFactory;
            _footer = footer;

            _closedBanner = closedBanner;
            _exterminatedBanner = exterminatedBanner;
            _finishedBanner = finishedBanner;
            _shortedBanner = shortedBanner;

            _conductScoreTextEffectFactory = conductScoreTextEffectFactory;
            _illuminateScoreTextEffectFactory = illuminateScoreTextEffectFactory;
            _lineCountTextEffectFactory = lineCountTextEffectFactory;
            _lineScoreTextEffectFactory = lineScoreTextEffectFactory;
            _nowhereTextEffectFactory = nowhereTextEffectFactory;

            _boardTilePositioner = boardTilePositioner;
            _currentTilePositioner = currentTilePositioner;
            _nextTilePositioner = nextTilePositioner;

            _bulbTiles = bulbTiles;
            _terminalTiles = terminalTiles;

            _tileMover = tileMover;
            _tileRenderer = tileRenderer;
            _tileThrower = tileThrower;

            _musicPlayer = musicPlayer;
            _soundPlayer = soundPlayer;
            _addTileProcess = addTileProcess;
            _endMainGameProcess = endMainGameProcess;
        }

        public async UniTask AsyncPutCurrentTile()
        {
            var tileModel = _currentTileModel.TileModel;
            if (tileModel == null)
            {
                return;
            }

            if (_currentTileModel.TryGetCurrentTileId(out var tileId) &&
                _selectedBoardCellModel.TryGetSelectedPanelCell(out var cellPosition))
            {
                _gameStateModel.SetGameStateName(GameStateName.None);

                _soundPlayer.PlayPutTileSound();
                _tileMover.MoveTile(tileId, _boardTilePositioner.GetPosition(cellPosition));
                _selectedBoardCellModel.Reset();
                _currentTileModel.Reset();

                _mainBoardModel.PutTile(cellPosition, tileId, tileModel);


                _validCellPositionsModel.SetValidCellPositions(_mainBoardModel.ValidCellPositions);

                var conductBoardResult = _mainBoardModel.Conduct();
                _tileRenderer.RenderAllTiles();

                // 短絡で終了
                if (conductBoardResult.isCircuitShorted)
                {
                    _mainFrameModel.FrameModel.DarkenIllumination();

                    var hashSetShortedElectricStatus = new HashSet<ElectricStatus>();
                    foreach (var shortedCircuitPath in conductBoardResult.shortedCircuitPaths)
                    {
                        hashSetShortedElectricStatus.Add(shortedCircuitPath.electricStatus);
                    }

                    _tileRenderer.RenderDarkenAllTiles(hashSetShortedElectricStatus);

                    var shortedStatus = hashSetShortedElectricStatus.Count >= 3
                        ? ShortedStatus.Fatal
                        : ShortedStatus.Shorted;

                    foreach (var shortedCircuitPath in conductBoardResult.shortedCircuitPaths)
                    {
                        var shortedCellPosition = shortedCircuitPath.cellPosition;
                        foreach (var lineDirectionPair in shortedCircuitPath.lineDirectionPairs)
                        {
                            if (_mainBoardModel.TryGetPutTileId(shortedCellPosition, out var shortedCircuitTileId))
                            {
                                _tileRenderer.RenderShortTile(shortedCircuitTileId, shortedStatus,
                                    lineDirectionPair.lineDirectionInput,
                                    lineDirectionPair.lineDirectionOutput);
                            }

                            shortedCellPosition += lineDirectionPair.lineDirectionOutput switch
                            {
                                LineDirection.Up => Vector2Int.up,
                                LineDirection.Right => Vector2Int.right,
                                LineDirection.Down => Vector2Int.down,
                                LineDirection.Left => Vector2Int.left,
                                _ => Vector2Int.zero
                            };
                        }
                    }

                    switch (shortedStatus)
                    {
                        case ShortedStatus.Shorted:
                        {
                            _soundPlayer.PlayShortedSound();
                            _soundPlayer.PlayBadElectricSound();
                            break;
                        }
                        case ShortedStatus.Fatal:
                        {
                            _soundPlayer.PlayFatalSound();
                            _soundPlayer.PlayBadElectricSound();
                            break;
                        }
                    }

                    _footer.RenderText();
                    _shortedBanner.Show(shortedStatus);
                    _explosionFactory.GenerateExplosion();
                    await UniTask.Delay(TimeSpan.FromSeconds(5.0));
                    _shortedBanner.FadeOut();
                    await UniTask.Delay(TimeSpan.FromSeconds(2.0));
                    await _endMainGameProcess.EndMainGame();

                    return;
                }
                
                // ------------------------------ 通電 ------------------------------

                if (conductBoardResult.scoredTiles.Count > 0)
                {
                    var totalScore = 0;
                    foreach (var (scoredCellPosition, scoredTile)in conductBoardResult.scoredTiles)
                    {
                        _lineScoreTextEffectFactory.GenerateTextEffect(scoredCellPosition, scoredTile.score,
                            scoredTile.textEffectMaterialType);
                        totalScore += scoredTile.score;
                    }

                    _soundPlayer.PlayConductLineSound();
                    _scoreModel.AddScore(totalScore);
                    _desk.ValueDisplayScore.Draw();
                }

                foreach (var illuminatePath in conductBoardResult.illuminatedCircuitPaths)
                {
                    var illuminateCellPosition = illuminatePath.cellPosition;
                    var lineCount = 0;

                    foreach (var lineDirectionPair in illuminatePath.lineDirectionPairs)
                    {
                        lineCount++;

                        if (_mainBoardModel.TryGetPutTileId(illuminateCellPosition, out var illuminatedCircuitTileId))
                        {
                            _tileRenderer.RenderIlluminateTile(illuminatedCircuitTileId,
                                lineDirectionPair.lineDirectionInput,
                                lineDirectionPair.lineDirectionOutput);
                        }

                        _lineCountTextEffectFactory.GenerateTextEffect(illuminateCellPosition, lineCount,
                            illuminatePath.electricStatus);
                        _soundPlayer.PlayIlluminateLineSound(lineCount);

                        await UniTask.Delay(TimeSpan.FromSeconds(0.1));

                        illuminateCellPosition += lineDirectionPair.lineDirectionOutput switch
                        {
                            LineDirection.Up => Vector2Int.up,
                            LineDirection.Right => Vector2Int.right,
                            LineDirection.Down => Vector2Int.down,
                            LineDirection.Left => Vector2Int.left,
                            _ => Vector2Int.zero
                        };
                    }

                    await UniTask.Delay(TimeSpan.FromSeconds(0.2));

                    switch (_mainBoardModel.IlluminateBulbOrTerminal(illuminateCellPosition,
                                illuminatePath.electricStatus))
                    {
                        case IlluminateBulbOrTerminalResult.Bulb:
                        {
                            var score = lineCount * 30;
                            _lineCountsModel.AddLineCount(LineStatus.Bulb, lineCount);
                            _scoreModel.AddScore(score);
                            _desk.ValueDisplayScore.Draw();
                            _conductScoreTextEffectFactory.GenerateTextEffect(illuminateCellPosition, score, lineCount,
                                illuminatePath.electricStatus, ConductScoreTextEffectMode.Bulb);
                            if (_mainBoardModel.TryGetPutTileId(illuminateCellPosition, out var bulbTileId))
                            {
                                _bulbTiles.IlluminateTile(bulbTileId);
                            }

                            _soundPlayer.PlayIlluminateBulbSound();
                            break;
                        }
                        case IlluminateBulbOrTerminalResult.TerminalNormal:
                        {
                            var score = lineCount * 100;
                            _lineCountsModel.AddLineCount(LineStatus.Correct, lineCount);
                            _scoreModel.AddScore(score);
                            _desk.ValueDisplayScore.Draw();
                            _conductScoreTextEffectFactory.GenerateTextEffect(illuminateCellPosition, score, lineCount,
                                illuminatePath.electricStatus, ConductScoreTextEffectMode.Normal);
                            if (_mainBoardModel.TryGetPutTileId(illuminateCellPosition, out var terminalTileId))
                            {
                                _terminalTiles.IlluminateTileRadiantly(terminalTileId);
                            }

                            _mainFrameModel.FrameModel.IlluminateTerminal(illuminateCellPosition);
                            _soundPlayer.PlayIlluminateTerminalSound();
                            break;
                        }
                        case IlluminateBulbOrTerminalResult.TerminalCorrect:
                        {
                            var score = lineCount * 100;
                            _lineCountsModel.AddLineCount(LineStatus.Correct, lineCount);
                            _scoreModel.AddScore(score);
                            _desk.ValueDisplayScore.Draw();
                            _conductScoreTextEffectFactory.GenerateTextEffect(illuminateCellPosition, score, lineCount,
                                illuminatePath.electricStatus, ConductScoreTextEffectMode.Correct);
                            if (_mainBoardModel.TryGetPutTileId(illuminateCellPosition, out var terminalTileId))
                            {
                                _terminalTiles.IlluminateTileRadiantly(terminalTileId);
                            }

                            _mainFrameModel.FrameModel.IlluminateTerminal(illuminateCellPosition);
                            _soundPlayer.PlayIlluminateTerminalSound();
                            break;
                        }
                        case IlluminateBulbOrTerminalResult.TerminalIncorrect:
                        {
                            var score = lineCount * 60;
                            _lineCountsModel.AddLineCount(LineStatus.Incorrect, lineCount);
                            _scoreModel.AddScore(score);
                            _desk.ValueDisplayScore.Draw();
                            _conductScoreTextEffectFactory.GenerateTextEffect(illuminateCellPosition, score, lineCount,
                                illuminatePath.electricStatus, ConductScoreTextEffectMode.Incorrect);
                            if (_mainBoardModel.TryGetPutTileId(illuminateCellPosition, out var terminalTileId))
                            {
                                _terminalTiles.IlluminateTile(terminalTileId);
                            }

                            _soundPlayer.PlayIlluminateBulbSound();
                            break;
                        }
                    }

                    _tileRenderer.RenderAllTiles();
                }

                if (_mainFrameModel.FrameModel.CanIlluminate)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(0.3));
                    var score = _mainFrameModel.FrameModel.IlluminateScore;
                    _scoreModel.AddScore(score);
                    _desk.ValueDisplayScore.Draw();
                    _mainFrameModel.FrameModel.IlluminateIllumination();
                    _illuminateScoreTextEffectFactory.GenerateTextEffect(score);
                    _musicPlayer.MuteIlluminated(false);
                    _soundPlayer.PlayIlluminateFrameSound();
                }

                if (_tileRestAmountModel.IsRunOUt)
                {
                    _footer.RenderText();
                    _finishedBanner.ShowUp();
                    await UniTask.Delay(TimeSpan.FromSeconds(7.0));
                    _finishedBanner.FadeOut();
                    await UniTask.Delay(TimeSpan.FromSeconds(3.0));
                    await _endMainGameProcess.EndMainGame();

                    return;
                }

                if (conductBoardResult.isCircuitClosed)
                {
                    _footer.RenderText();
                    _closedBanner.ShowUp();
                    await UniTask.Delay(TimeSpan.FromSeconds(7.0));
                    _closedBanner.FadeOut();
                    await UniTask.Delay(TimeSpan.FromSeconds(3.0));
                    await _endMainGameProcess.EndMainGame();
                    return;
                }

                if (_validCellPositionsModel.CanPutTileType(_nextTileModel.TileModel.TileType))
                {
                    if (_nextTileModel.TryGetTileId(out var nextTileId))
                    {
                        _currentTileModel.SetCurrentTileId(nextTileId);
                        _currentTileModel.SetCurrentTileModel(_nextTileModel.TileModel);

                        _currentTilePositioner.SetStartPosition(_nextTilePositioner.CurrentPosition);
                        _currentTilePositioner.StartLerp();

                        _tileRestAmountModel.DecreaseTileRestAmount();
                        _desk.ValueDisplayTileRestAmount.DrawImmediate();

                        if (_tileRestAmountModel.IsRunOUt)
                        {
                            _nextTileModel.Reset();
                        }
                        else
                        {
                            var (nextNextTileId, nextNextTileModel) =
                                _addTileProcess.AddTile(_tileDeckModel.TakeTile());
                            _nextTileModel.SetTileId(nextNextTileId);
                            _nextTileModel.SetTileModel(nextNextTileModel);

                            _nextTilePositioner.StartLerp();
                        }

                        _gameStateModel.SetGameStateName(GameStateName.Main);
                    }
                }
                else // タイルの配置ができない場合
                {
                    if (_nextTileModel.TryGetTileId(out var nextTileId))
                    {
                        _tileThrower.ThrowTile(nextTileId, new Vector3(0.0f, 2.0f, -2.0f),
                            new Vector3(-1000.0f, 0.0f, 0.0f));
                    }

                    _nextTileModel.Reset();
                    _nowhereTextEffectFactory.GenerateTextEffect();

                    _tileRestAmountModel.DecreaseTileRestAmount();
                    _desk.ValueDisplayTileRestAmount.DrawImmediate();

                    if (_tileRestAmountModel.IsRunOUt)
                    {
                        _footer.RenderText();
                        _finishedBanner.ShowUp();
                        await UniTask.Delay(TimeSpan.FromSeconds(7.0));
                        _finishedBanner.FadeOut();
                        await UniTask.Delay(TimeSpan.FromSeconds(3.0));
                        await _endMainGameProcess.EndMainGame();

                        return;
                    }


                    var validTileTypes = new List<TileType>(_validCellPositionsModel.ValidTiles);


                    if (_tileDeckModel.TryTakeValidTile(validTileTypes, out var tileType))
                    {
                        var (validTileId, validTileModel) = _addTileProcess.AddTile(tileType);

                        _currentTileModel.SetCurrentTileId(validTileId);
                        _currentTileModel.SetCurrentTileModel(validTileModel);

                        _currentTilePositioner.SetStartPosition(_nextTilePositioner.StartPosition);
                        _currentTilePositioner.StartLerp();

                        _tileRestAmountModel.DecreaseTileRestAmount();
                        _desk.ValueDisplayTileRestAmount.DrawImmediate();

                        if (_tileRestAmountModel.IsRunOUt)
                        {
                            _nextTileModel.Reset();
                        }
                        else
                        {
                            var (nextNextTileId, nextNextTileModel) =
                                _addTileProcess.AddTile(_tileDeckModel.TakeTile());
                            _nextTileModel.SetTileId(nextNextTileId);
                            _nextTileModel.SetTileModel(nextNextTileModel);

                            _nextTilePositioner.StartLerp();
                        }

                        _gameStateModel.SetGameStateName(GameStateName.Main);
                    }
                    else
                    {
                        _footer.RenderText();
                        _exterminatedBanner.ShowUp();
                        await UniTask.Delay(TimeSpan.FromSeconds(7.0));
                        _exterminatedBanner.FadeOut();
                        await UniTask.Delay(TimeSpan.FromSeconds(3.0));
                        await _endMainGameProcess.EndMainGame();
                    }
                }
            }
        }
    }
}