using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Models;
using Parameters.Enums;
using Parameters.Interfaces;
using Parameters.Structs;
using UnityEngine;
using VContainer;
using IlluminatePath = Parameters.Structs.IlluminatePath;

namespace Processes
{
    public class ConductMainBoardProcess
    {
        private readonly MainBoardModel _mainBoardModel;
        private readonly TilesModel _tilesModel;
        
        private struct Akio
        {
            public ElectricStatus ElectricStatus;
            public LineDirection LineDirectionPower;
            public LineDirection LineDirection;
            public Vector2Int CellPositionPower;
            public Vector2Int CellPosition;
            public List<LineDirectionPair> LineDirectionPairs;
        }

        private struct ShortedAkio
        {
            public LineDirection LineDirection;
            public Vector2Int CellPosition;
        }

        [Inject]
        public ConductMainBoardProcess(MainBoardModel mainBoardModel, TilesModel tilesModel)
        {
            _mainBoardModel = mainBoardModel;
            _tilesModel = tilesModel;
        }

        public  MainBoardConductionResult ConductMainBoard()
        {
            var isCircuitShorted = false;
            var isCircuitClosed = true;
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
                            ElectricStatus = output.ElectricStatus,
                            LineDirectionPower = output.LineDirection,
                            LineDirection = output.LineDirection,
                            CellPositionPower = cellPosition,
                            CellPosition = cellPosition,
                            LineDirectionPairs = new List<LineDirectionPair>
                            {
                                new()
                                {
                                    LineDirectionOutput = output.LineDirection
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

                    if (hashSet.Contains((akio.CellPosition, akio.LineDirection)))
                    {
                        continue;
                    }

                    hashSet.Add((akio.CellPosition, akio.LineDirection));
                    var nextCellPosition = akio.CellPosition + akio.LineDirection switch
                    {
                        LineDirection.Up => new Vector2Int(0, 1),
                        LineDirection.Right => new Vector2Int(1, 0),
                        LineDirection.Down => new Vector2Int(0, -1),
                        LineDirection.Left => new Vector2Int(-1, 0),
                        _ => Vector2Int.zero
                    };


                    // この間にパネルサイズ処理を挟んで！

                    var nullableTileId = _mainBoardModel.GetPutTileId(nextCellPosition);
                    if (nullableTileId.HasValue)
                    {

                        var tileId = nullableTileId.Value;
                        var tileModel = _tilesModel.GetTileModel(tileId);
                        foreach (var output in tileModel.Conduct(akio.ElectricStatus, akio.LineDirection))
                        {
                            var nextPath = new List<LineDirectionPair>(akio.LineDirectionPairs)
                            {
                                new()
                                {
                                    LineDirectionInput = akio.LineDirection,
                                    LineDirectionOutput = output.LineDirection
                                }
                            };

                            if (output.IsShorted)
                            {
                                shortedQueue.Enqueue(new ShortedAkio
                                {
                                    CellPosition = akio.CellPositionPower,
                                    LineDirection = akio.LineDirectionPower
                                });
                            }

                            if (output.TerminalType != TerminalType.None)
                            {
                                illuminatePaths.Add(new IlluminatePath
                                {
                                    ElectricStatus = akio.ElectricStatus,
                                    CellPosition = akio.CellPositionPower,
                                    LineDirectionPairs = nextPath
                                });
                            }

                            queue.Enqueue(new Akio
                            {
                                ElectricStatus = akio.ElectricStatus,
                                LineDirectionPower = akio.LineDirectionPower,
                                LineDirection = output.LineDirection,
                                CellPositionPower = akio.CellPositionPower,
                                CellPosition = nextCellPosition,
                                LineDirectionPairs = nextPath
                            });
                        }
                    }
                    else
                    {
                        isCircuitClosed = false;
                    }
                }
            }

            for (var loopLimit = 0; loopLimit < int.MaxValue && shortedQueue.Count > 0; loopLimit++)
            {
                isCircuitShorted = true;

                var akio = shortedQueue.Dequeue();

                if (shortedHashSet.Contains((akio.CellPosition, akio.LineDirection)))
                {
                    continue;
                }

                shortedHashSet.Add((akio.CellPosition, akio.LineDirection));

                var nextCellPosition = akio.CellPosition + akio.LineDirection switch
                {
                    LineDirection.Up => new Vector2Int(0, 1),
                    LineDirection.Right => new Vector2Int(1, 0),
                    LineDirection.Down => new Vector2Int(0, -1),
                    LineDirection.Left => new Vector2Int(-1, 0),
                    _ => Vector2Int.zero
                };


                // この間にパネルサイズ処理を挟んで！

                var nullableTileId = _mainBoardModel.GetPutTileId(nextCellPosition);
                if (nullableTileId.HasValue)
                {
                    var tileId = nullableTileId.Value;
                    var tileModel = _tilesModel.GetTileModel(tileId);
                    foreach (var output in tileModel.Conduct(ElectricStatus.Shorted, akio.LineDirection))
                    {
                        shortedQueue.Enqueue(new ShortedAkio
                        {
                            LineDirection = output.LineDirection,
                            CellPosition = nextCellPosition,
                        });
                    }
                }
            }

            return new MainBoardConductionResult
            {
                IsCircuitShorted = isCircuitShorted,
                IsCircuitClosed = isCircuitClosed,
                IlluminatePaths = illuminatePaths
            };
        }
    }
}