using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Enums;
using Models;
using UnityEngine;
using VContainer;
using Views;
using Views.Banners;
using Views.Controllers;
using Views.TextEffectFactories;

namespace Processes
{
    public class TutorialProcess
    {
        private readonly TutorialStateModel _tutorialStateModel;

        private readonly CurrentTileModel _currentTileModel;
        private readonly MainBoardModel _mainBoardModel;
        private readonly MainFrameModel _mainFrameModel;
        private readonly NextTileModel _nextTileModel;
        private readonly ScoreModel _scoreModel;
        private readonly SelectedBoardCellModel _selectedBoardCellModel;
        private readonly TileRestAmountModel _tileRestAmountModel;
        private readonly ValidCellPositionsModel _validCellPositionsModel;
        
        private readonly MainCamera _mainCamera;
        private readonly TutorialBanner _tutorialBanner;

        private readonly TutorialGuides _tutorialGuides;

        private readonly Desk _desk;

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

        private readonly SoundPlayer _soundPlayer;

        private readonly InitializeMainGameProcess _initializeMainGameProcess;
        private readonly ResetMainGameProcess _resetMainGameProcess;
        private readonly ClearMainGameProcess _clearMainGameProcess;

        private readonly AddTileProcess _addTileProcess;

        [Inject]
        public TutorialProcess(TutorialStateModel tutorialStateModel, CurrentTileModel currentTileModel,
            MainBoardModel mainBoardModel, MainFrameModel mainFrameModel, NextTileModel nextTileModel,
            ScoreModel scoreModel, SelectedBoardCellModel selectedBoardCellModel,
            TileRestAmountModel tileRestAmountModel, ValidCellPositionsModel validCellPositionsModel,
            MainCamera mainCamera, TutorialBanner tutorialBanner, TutorialGuides tutorialGuides, Desk desk,
            ConductScoreTextEffectFactory conductScoreTextEffectFactory,
            IlluminateScoreTextEffectFactory illuminateScoreTextEffectFactory,
            LineCountTextEffectFactory lineCountTextEffectFactory,
            LineScoreTextEffectFactory lineScoreTextEffectFactory, NowhereTextEffectFactory nowhereTextEffectFactory,
            BoardTilePositioner boardTilePositioner, CurrentTilePositioner currentTilePositioner,
            NextTilePositioner nextTilePositioner, BulbTiles bulbTiles, TerminalTiles terminalTiles,
            TileMover tileMover, TileRenderer tileRenderer, TileThrower tileThrower, SoundPlayer soundPlayer,
            InitializeMainGameProcess initializeMainGameProcess, ResetMainGameProcess resetMainGameProcess,
            ClearMainGameProcess clearMainGameProcess, AddTileProcess addTileProcess)
        {
            _tutorialStateModel = tutorialStateModel;

            _currentTileModel = currentTileModel;
            _mainBoardModel = mainBoardModel;
            _mainFrameModel = mainFrameModel;
            _nextTileModel = nextTileModel;
            _scoreModel = scoreModel;
            _selectedBoardCellModel = selectedBoardCellModel;
            _tileRestAmountModel = tileRestAmountModel;
            _validCellPositionsModel = validCellPositionsModel;

            _mainCamera = mainCamera;
            _tutorialBanner = tutorialBanner;

            _tutorialGuides = tutorialGuides;

            _desk = desk;

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

            _soundPlayer = soundPlayer;

            _initializeMainGameProcess = initializeMainGameProcess;
            _resetMainGameProcess = resetMainGameProcess;
            _clearMainGameProcess = clearMainGameProcess;

            _addTileProcess = addTileProcess;
        }

        public async UniTask AsyncStartTutorial()
        {
            _clearMainGameProcess.ClearMainGame();
            _initializeMainGameProcess.InitializeMainGame(FrameSize.Medium);
            _resetMainGameProcess.ResetMainGame();

            _desk.ValueDisplayScore.DrawImmediate();
            _desk.ValueDisplayTileRestAmount.DrawImmediate();   
            
            _tileRenderer.RenderAllTiles();
            
            _mainCamera.LookMainBoard();
            _tutorialBanner.ChangeMessageNone();
            _tutorialBanner.Show();

            await UniTask.Delay(TimeSpan.FromSeconds(4.0));

            for (var i = 0; i < 7; i++)
            {
                _tutorialBanner.ChangeMessageIntroduction(i);
                switch (i)
                {
                    case 3:
                    {
                        _tutorialGuides.ShowCellPointersA();
                        break;
                    }
                    case 4:
                    {
                        _tutorialGuides.ShowTiles();
                        break;
                    }
                }

                await UniTask.Delay(TimeSpan.FromSeconds(7.0));
                _tutorialBanner.ChangeMessageNone();
                await UniTask.Delay(TimeSpan.FromSeconds(0.1));
            }
            
            var (tileIdCurrent, tileModelCurrent) = _addTileProcess.AddTile(TileType.Straight);
            var (tileIdNext, tileModelNext) = _addTileProcess.AddTile(TileType.Curve);

            _currentTileModel.SetCurrentTileId(tileIdCurrent);
            _currentTileModel.SetCurrentTileModel(tileModelCurrent);
            
            _nextTileModel.SetTileId(tileIdNext);
            _nextTileModel.SetTileModel(tileModelNext);
            
            _currentTilePositioner.SetStartPosition(_nextTilePositioner.StartPosition);
            
            _currentTilePositioner.StartLerp();
            _nextTilePositioner.StartLerp();
            _tileRenderer.RenderAllTiles();

            _validCellPositionsModel.SetValidCellPositions(_mainBoardModel.ValidCellPositions);
            _tutorialStateModel.ChangeTutorialState(TutorialStateName.First);
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
                var tutorialConductResult = _mainBoardModel.TutorialConduct(cellPosition, tileModel);

                switch (tutorialConductResult)
                {
                    case TutorialConductResult.Shorted:
                    {
                        _tutorialBanner.ChangeMessageWarningShorted();
                        return;
                    }
                    case TutorialConductResult.Closed:
                    {
                        _tutorialBanner.ChangeMessageWarningClosed();
                        return;
                    }
                }
                
                switch (_tutorialStateModel.TutorialStateName)
                {
                    case TutorialStateName.Bulb:
                    {
                        switch (tutorialConductResult)
                        {
                            case TutorialConductResult.None:
                            {
                                _tutorialBanner.ChangeMessageBulbNotice();
                                return;
                            }
                        }

                        break;
                    }
                }


                _tutorialStateModel.SetCanPut(false);
                _tutorialStateModel.ChangeTutorialState(_tutorialStateModel.TutorialStateName switch
                {
                    TutorialStateName.First => TutorialStateName.Curve,
                    TutorialStateName.Curve => TutorialStateName.TwinCurves,
                    TutorialStateName.TwinCurves => TutorialStateName.Cross,
                    TutorialStateName.Cross => TutorialStateName.ThreeWayDistributor,
                    TutorialStateName.ThreeWayDistributor => TutorialStateName.FourWayDistributor,
                    TutorialStateName.FourWayDistributor => TutorialStateName.Bulb,
                    TutorialStateName.Bulb => TutorialStateName.Final,
                    _ => _tutorialStateModel.TutorialStateName
                });
                
                _soundPlayer.PlayPutTileSound();
                _tileMover.MoveTile(tileId, _boardTilePositioner.GetPosition(cellPosition));
                _selectedBoardCellModel.Reset();
                _currentTileModel.Reset();
                _mainBoardModel.PutTile(cellPosition, tileId, tileModel);
                _validCellPositionsModel.SetValidCellPositions(_mainBoardModel.ValidCellPositions);
                
                var conductBoardResult = _mainBoardModel.Conduct();
                _tileRenderer.RenderAllTiles();
                
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

                var isComplete = false;
                
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
                            _scoreModel.AddScore(score);
                            _desk.ValueDisplayScore.Draw();
                            _conductScoreTextEffectFactory.GenerateTextEffect(illuminateCellPosition, score, lineCount,
                                illuminatePath.electricStatus, ConductScoreTextEffectMode.Normal);
                            if (_mainBoardModel.TryGetPutTileId(illuminateCellPosition, out var terminalTileId))
                            {
                                _terminalTiles.IlluminateTileRadiantly(terminalTileId);
                            }

                            _soundPlayer.PlayIlluminateTerminalSound();

                            isComplete = true;
                            break;
                        }
                        case IlluminateBulbOrTerminalResult.TerminalCorrect:
                        {
                            var score = lineCount * 100;
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

                            isComplete = true;
                            break;
                        }
                        case IlluminateBulbOrTerminalResult.TerminalIncorrect:
                        {
                            var score = lineCount * 60;
                            _scoreModel.AddScore(score);
                            _desk.ValueDisplayScore.Draw();
                            _conductScoreTextEffectFactory.GenerateTextEffect(illuminateCellPosition, score, lineCount,
                                illuminatePath.electricStatus, ConductScoreTextEffectMode.Incorrect);
                            if (_mainBoardModel.TryGetPutTileId(illuminateCellPosition, out var terminalTileId))
                            {
                                _terminalTiles.IlluminateTile(terminalTileId);
                            }

                            _soundPlayer.PlayIlluminateBulbSound();

                            isComplete = true;
                            break;
                        }
                    }

                    _tileRenderer.RenderAllTiles();
                }

                if (isComplete)
                {
                    
                }
                else if (_validCellPositionsModel.CanPutTileType(_nextTileModel.TileModel.TileType))
                {
                    if (_nextTileModel.TryGetTileId(out var nextTileId))
                    {
                        _currentTileModel.SetCurrentTileId(nextTileId);
                        _currentTileModel.SetCurrentTileModel(_nextTileModel.TileModel);
                        _currentTilePositioner.SetStartPosition(_nextTilePositioner.CurrentPosition);
                        _currentTilePositioner.StartLerp();

                        var tileTypes = new List<TileType>
                        {
                            TileType.Straight,
                            TileType.Straight,
                            TileType.Straight,
                            TileType.Straight,
                            TileType.Curve,
                            TileType.Curve,
                            TileType.Curve,
                            TileType.Curve,
                            TileType.Curve,
                            TileType.TwinCurves,
                            TileType.TwinCurves,
                            TileType.TwinCurves,
                            TileType.Cross,
                            TileType.Cross,
                            TileType.Cross,
                            TileType.ThreeWayDistributor,
                            TileType.ThreeWayDistributor,
                            TileType.FourWayDistributor,
                            TileType.Bulb
                        };
                        var tileType = _tutorialStateModel.TutorialStateName switch
                        {
                            TutorialStateName.Curve => TileType.TwinCurves,
                            TutorialStateName.TwinCurves => TileType.Cross,
                            TutorialStateName.Cross => TileType.ThreeWayDistributor,
                            TutorialStateName.ThreeWayDistributor => TileType.FourWayDistributor,
                            TutorialStateName.FourWayDistributor => TileType.Bulb,
                            _ => tileTypes[UnityEngine.Random.Range(0, tileTypes.Count)]
                        };

                        var (nextNextTileId, nextNextTileModel) = _addTileProcess.AddTile(tileType);
                        _nextTileModel.SetTileId(nextNextTileId);
                        _nextTileModel.SetTileModel(nextNextTileModel);

                        _nextTilePositioner.StartLerp();
                    }
                }
                else
                {
                    if (_nextTileModel.TryGetTileId(out var nextTileId))
                    {
                        _tileThrower.ThrowTile(nextTileId, new Vector3(0.0f, 2.0f, -2.0f),
                            new Vector3(-1000.0f, 0.0f, 0.0f));
                    }
                    
                    _nextTileModel.Reset();
                    _nowhereTextEffectFactory.GenerateTextEffect();

                    var validTileTypes = new List<TileType>(_validCellPositionsModel.ValidTiles);

                    var validTileType = validTileTypes[UnityEngine.Random.Range(0, validTileTypes.Count)];

                    var (validTileId, validTileModel) = _addTileProcess.AddTile(validTileType);
                    _currentTileModel.SetCurrentTileId(validTileId);
                    _currentTileModel.SetCurrentTileModel(validTileModel);

                    _currentTilePositioner.SetStartPosition(_nextTilePositioner.StartPosition);
                    _currentTilePositioner.StartLerp();
                }
            }
        }
    }
}