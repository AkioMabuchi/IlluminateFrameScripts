using System.Collections.Generic;
using Enums;
using Interfaces;
using Models;
using Structs;
using UnityEngine;
using VContainer;
using IlluminatePath = Structs.IlluminatePath;

namespace Processes
{
    public class ConductMainBoardProcess
    {
        private readonly MainBoardModel _mainBoardModel;
        private readonly MainFrameModel _mainFrameModel;
        private readonly TilesModel _tilesModel;
        
        private struct Akio
        {
            public ElectricStatus electricStatus;
            public LineDirection lineDirectionPower;
            public LineDirection lineDirection;
            public Vector2Int cellPositionPower;
            public Vector2Int cellPosition;
            public List<LineDirectionPair> lineDirectionPairs;
        }

        private struct ShortedAkio
        {
            public LineDirection lineDirection;
            public Vector2Int cellPosition;
        }

        [Inject]
        public ConductMainBoardProcess(MainBoardModel mainBoardModel, MainFrameModel mainFrameModel, TilesModel tilesModel)
        {
            _mainBoardModel = mainBoardModel;
            _mainFrameModel = mainFrameModel;
            _tilesModel = tilesModel;
        }

        public  MainBoardConductionResult ConductMainBoard()
        {
            var isCircuitShorted = false;
            var isCircuitClosed = true;
            var scoredTiles = new List<ScoredTile>();
            var illuminatePaths = new List<IlluminatePath>();

            var queuePower = new Queue<Akio>();
            var queue = new Queue<Akio>();
            var hashSet = new HashSet<(Vector2Int, LineDirection)>();

            var shortedQueue = new Queue<ShortedAkio>();
            var shortedHashSet = new HashSet<(Vector2Int, LineDirection)>();
            
            foreach (var (cellPosition, tileId) in _mainBoardModel.CellPositionsAndTileIds)
            {
                var tileModel = _tilesModel.GetTileModel(tileId);

                if (tileModel is ITilePower tilePower)
                {
                    foreach (var output in tilePower.PowerOutputs)
                    {
                        queuePower.Enqueue(new Akio
                        {
                            electricStatus = output.electricStatus,
                            lineDirectionPower = output.lineDirection,
                            lineDirection = output.lineDirection,
                            cellPositionPower = cellPosition,
                            cellPosition = cellPosition,
                            lineDirectionPairs = new List<LineDirectionPair>
                            {
                                new()
                                {
                                    lineDirectionOutput = output.lineDirection
                                }
                            }
                        });
                    }
                }
            }

            while (queuePower.Count > 0)
            {
                hashSet.Clear();
                queue.Enqueue(queuePower.Dequeue());
                while (queue.Count > 0)
                {
                    var akio = queue.Dequeue();

                    if (hashSet.Contains((akio.cellPosition, akio.lineDirection)))
                    {
                        continue;
                    }

                    hashSet.Add((akio.cellPosition, akio.lineDirection));
                    var nextCellPosition = akio.cellPosition + akio.lineDirection switch
                    {
                        LineDirection.Up => new Vector2Int(0, 1),
                        LineDirection.Right => new Vector2Int(1, 0),
                        LineDirection.Down => new Vector2Int(0, -1),
                        LineDirection.Left => new Vector2Int(-1, 0),
                        _ => Vector2Int.zero
                    };
                    
                    if (_mainFrameModel.Frame.IsInBoard(nextCellPosition))
                    {

                        var nullableTileId = _mainBoardModel.GetPutTileId(nextCellPosition);
                        if (nullableTileId.HasValue)
                        {

                            var tileId = nullableTileId.Value;
                            var tileModel = _tilesModel.GetTileModel(tileId);
                            foreach (var output in tileModel.Conduct(akio.electricStatus, akio.lineDirection))
                            {
                                var nextPath = new List<LineDirectionPair>(akio.lineDirectionPairs)
                                {
                                    new()
                                    {
                                        lineDirectionInput = akio.lineDirection,
                                        lineDirectionOutput = output.lineDirection
                                    }
                                };

                                if (output.score > 0)
                                {
                                    scoredTiles.Add(new ScoredTile
                                    {
                                        cellPosition = nextCellPosition,
                                        electricStatus = akio.electricStatus,
                                        score = output.score
                                    });
                                }

                                if (output.isShorted)
                                {
                                    shortedQueue.Enqueue(new ShortedAkio
                                    {
                                        cellPosition = akio.cellPositionPower,
                                        lineDirection = akio.lineDirectionPower
                                    });
                                }

                                if (output.terminalType != TerminalType.None)
                                {
                                    illuminatePaths.Add(new IlluminatePath
                                    {
                                        electricStatus = akio.electricStatus,
                                        cellPosition = akio.cellPositionPower,
                                        lineDirectionPairs = nextPath
                                    });
                                }

                                queue.Enqueue(new Akio
                                {
                                    electricStatus = akio.electricStatus,
                                    lineDirectionPower = akio.lineDirectionPower,
                                    lineDirection = output.lineDirection,
                                    cellPositionPower = akio.cellPositionPower,
                                    cellPosition = nextCellPosition,
                                    lineDirectionPairs = nextPath
                                });
                            }
                        }
                        else
                        {
                            isCircuitClosed = false;
                        }
                    }
                }
            }

            for (var loopLimit = 0; loopLimit < int.MaxValue && shortedQueue.Count > 0; loopLimit++)
            {
                isCircuitShorted = true;

                var akio = shortedQueue.Dequeue();

                if (shortedHashSet.Contains((akio.cellPosition, akio.lineDirection)))
                {
                    continue;
                }

                shortedHashSet.Add((akio.cellPosition, akio.lineDirection));

                var nextCellPosition = akio.cellPosition + akio.lineDirection switch
                {
                    LineDirection.Up => new Vector2Int(0, 1),
                    LineDirection.Right => new Vector2Int(1, 0),
                    LineDirection.Down => new Vector2Int(0, -1),
                    LineDirection.Left => new Vector2Int(-1, 0),
                    _ => Vector2Int.zero
                };

                if (_mainFrameModel.Frame.IsInBoard(nextCellPosition))
                {
                    var nullableTileId = _mainBoardModel.GetPutTileId(nextCellPosition);
                    if (nullableTileId.HasValue)
                    {
                        var tileId = nullableTileId.Value;
                        var tileModel = _tilesModel.GetTileModel(tileId);
                        foreach (var output in tileModel.Conduct(ElectricStatus.Shorted, akio.lineDirection))
                        {
                            shortedQueue.Enqueue(new ShortedAkio
                            {
                                lineDirection = output.lineDirection,
                                cellPosition = nextCellPosition,
                            });
                        }
                    }
                }
            }

            return new MainBoardConductionResult
            {
                isCircuitShorted = isCircuitShorted,
                isCircuitClosed = isCircuitClosed,
                scoredTiles = scoredTiles,
                illuminatePaths = illuminatePaths
            };
        }
    }
}