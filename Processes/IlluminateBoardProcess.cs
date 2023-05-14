using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Enums;
using Interfaces;
using Models;
using Structs;
using UnityEngine;
using VContainer;
using Views;

namespace Processes
{
    public class IlluminateBoardProcess
    {
        private readonly MainBoardModel _mainBoardModel;
        private readonly TilesModel _tilesModel;

        private readonly TextEffectFactory _textEffectFactory;

        private readonly AddScoreProcess _addScoreProcess;

        [Inject]
        public IlluminateBoardProcess(MainBoardModel mainBoardModel, TilesModel tilesModel,
            TextEffectFactory textEffectFactory, AddScoreProcess addScoreProcess)
        {
            _mainBoardModel = mainBoardModel;
            _tilesModel = tilesModel;

            _textEffectFactory = textEffectFactory;
            _addScoreProcess = addScoreProcess;
        }

        public async UniTask IlluminateBoard(IEnumerable<IlluminatePath> illuminatePaths)
        {

            foreach (var illuminatePath in illuminatePaths)
            {
                var illuminateElectricStatus = illuminatePath.electricStatus switch
                {
                    ElectricStatus.Normal => ElectricStatus.NormalIlluminate,
                    ElectricStatus.Plus => ElectricStatus.PlusIlluminate,
                    ElectricStatus.Minus => ElectricStatus.MinusIlluminate,
                    ElectricStatus.Alternating => ElectricStatus.AlternatingIlluminate,
                    _ => illuminatePath.electricStatus
                };
                var cellPosition = illuminatePath.cellPosition;
                var lineCount = 0;

                foreach (var lineDirectionPair in illuminatePath.lineDirectionPairs)
                {
                    lineCount++;
                    
                    var nullableTileId = _mainBoardModel.GetPutTileId(cellPosition);
                    if (nullableTileId.HasValue)
                    {
                        var tileId = nullableTileId.Value;
                        var tileModel = _tilesModel.GetTileModel(tileId);


                        tileModel.Illuminate(illuminateElectricStatus, lineDirectionPair.lineDirectionInput,
                            lineDirectionPair.lineDirectionOutput);
                    }

                    _textEffectFactory.GenerateTextEffectLineCount(cellPosition, illuminatePath.electricStatus,
                        lineCount);
                    await UniTask.Delay(TimeSpan.FromSeconds(0.1));

                    cellPosition += lineDirectionPair.lineDirectionOutput switch
                    {
                        LineDirection.Up => new Vector2Int(0, 1),
                        LineDirection.Right => new Vector2Int(1, 0),
                        LineDirection.Down => new Vector2Int(0, -1),
                        LineDirection.Left => new Vector2Int(-1, 0),
                        _ => Vector2Int.zero
                    };
                }
                
                await UniTask.Delay(TimeSpan.FromSeconds(0.2));
                var nullableTerminalTileId = _mainBoardModel.GetPutTileId(cellPosition);
                if (nullableTerminalTileId.HasValue)
                {
                    var tileId = nullableTerminalTileId.Value;
                    var tileModel = _tilesModel.GetTileModel(tileId);
                    if (tileModel is ITileBulb bulb)
                    {
                        switch (bulb.GetBulbElectricStatus(illuminatePath.electricStatus))
                        {
                            case BulbElectricStatus.Illuminated:
                            {
                                var score = lineCount * 30;
                                _textEffectFactory.GenerateTextEffectIlluminateScore(cellPosition,
                                    illuminatePath.electricStatus, score, lineCount);
                                _addScoreProcess.AddScore(score);
                                break;
                            }
                        }
                    }

                    if (tileModel is ITileTerminal terminal)
                    {
                        switch (terminal.GetTerminalElectricStatus(illuminatePath.electricStatus))
                        {
                            case TerminalElectricStatus.Correct:
                            {
                                var score = lineCount * 100;
                                _textEffectFactory.GenerateTextEffectIlluminateScoreCorrect(cellPosition,
                                    illuminatePath.electricStatus, score, lineCount);
                                _addScoreProcess.AddScore(score);
                                break;
                            }
                            case TerminalElectricStatus.Different:
                            {
                                var score = lineCount * 60;
                                _textEffectFactory.GenerateTextEffectIlluminateScore(cellPosition,
                                    illuminatePath.electricStatus, score, lineCount);
                                _addScoreProcess.AddScore(score);
                                break;
                            }
                        }
                    }
                }
                _textEffectFactory.GenerateTextEffectLineCount(cellPosition, illuminatePath.electricStatus,
                    lineCount);
                
                
                cellPosition = illuminatePath.cellPosition;
                
                foreach (var lineDirectionPair in illuminatePath.lineDirectionPairs)
                {
                    var nullableTileId = _mainBoardModel.GetPutTileId(cellPosition);
                    if (nullableTileId.HasValue)
                    {
                        var tileId = nullableTileId.Value;
                        var tileModel = _tilesModel.GetTileModel(tileId);

                        tileModel.Illuminate(illuminatePath.electricStatus, lineDirectionPair.lineDirectionInput,
                            lineDirectionPair.lineDirectionOutput);
                    }

                    cellPosition += lineDirectionPair.lineDirectionOutput switch
                    {
                        LineDirection.Up => new Vector2Int(0, 1),
                        LineDirection.Right => new Vector2Int(1, 0),
                        LineDirection.Down => new Vector2Int(0, -1),
                        LineDirection.Left => new Vector2Int(-1, 0),
                        _ => Vector2Int.zero
                    };
                }
            }
        }
    }
}

