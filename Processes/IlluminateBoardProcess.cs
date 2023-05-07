using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Models;
using Parameters.Enums;
using Parameters.Structs;
using UnityEngine;
using VContainer;

namespace Processes
{
    public class IlluminateBoardProcess
    {
        private readonly MainBoardModel _mainBoardModel;
        private readonly TilesModel _tilesModel;

        [Inject]
        public IlluminateBoardProcess(MainBoardModel mainBoardModel, TilesModel tilesModel)
        {
            _mainBoardModel = mainBoardModel;
            _tilesModel = tilesModel;
        }

        public async UniTask IlluminateBoard(IEnumerable<IlluminatePath> illuminatePaths)
        {
            await UniTask.Delay(TimeSpan.Zero);
            
            foreach (var illuminatePath in illuminatePaths)
            {
                var electricStatus = illuminatePath.ElectricStatus switch
                {
                    ElectricStatus.Normal => ElectricStatus.NormalIlluminate,
                    ElectricStatus.Plus => ElectricStatus.PlusIlluminate,
                    ElectricStatus.Minus => ElectricStatus.MinusIlluminate,
                    ElectricStatus.Alternating => ElectricStatus.AlternatingIlluminate,
                    _ => illuminatePath.ElectricStatus
                };
                var cellPosition = illuminatePath.CellPosition;

                foreach (var lineDirectionPair in illuminatePath.LineDirectionPairs)
                {
                    var nullableTileId = _mainBoardModel.GetPutTileId(cellPosition);
                    if (nullableTileId.HasValue)
                    {
                        var tileId = nullableTileId.Value;
                        var tileModel = _tilesModel.GetTileModel(tileId);


                        tileModel.Illuminate(electricStatus, lineDirectionPair.LineDirectionInput,
                            lineDirectionPair.LineDirectionOutput);

                        await UniTask.Delay(TimeSpan.FromSeconds(0.1));
                    }

                    cellPosition += lineDirectionPair.LineDirectionOutput switch
                    {
                        LineDirection.Up => new Vector2Int(0, 1),
                        LineDirection.Right => new Vector2Int(1, 0),
                        LineDirection.Down => new Vector2Int(0, -1),
                        LineDirection.Left => new Vector2Int(-1, 0),
                        _ => Vector2Int.zero
                    };
                }

                electricStatus = illuminatePath.ElectricStatus;
                cellPosition = illuminatePath.CellPosition;
                
                foreach (var lineDirectionPair in illuminatePath.LineDirectionPairs)
                {
                    var nullableTileId = _mainBoardModel.GetPutTileId(cellPosition);
                    if (nullableTileId.HasValue)
                    {
                        var tileId = nullableTileId.Value;
                        var tileModel = _tilesModel.GetTileModel(tileId);

                        tileModel.Illuminate(electricStatus, lineDirectionPair.LineDirectionInput,
                            lineDirectionPair.LineDirectionOutput);
                    }

                    cellPosition += lineDirectionPair.LineDirectionOutput switch
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

