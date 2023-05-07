using System.Collections.Generic;
using Models;
using Parameters.Enums;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

namespace Processes
{
    public class UpdateValidCellPositionsProcess
    {
        private readonly MainBoardModel _mainBoardModel;
        private readonly TilesModel _tilesModel;
        private readonly ValidCellPositionsModel _validCellPositionsModel;

        [Inject]
        public UpdateValidCellPositionsProcess(MainBoardModel mainBoardModel, TilesModel tilesModel,
            ValidCellPositionsModel validCellPositionsModel)
        {
            _mainBoardModel = mainBoardModel;
            _tilesModel = tilesModel;
            _validCellPositionsModel = validCellPositionsModel;
        }

        private struct TileDirections
        {
            public TileEdgeType Up;
            public TileEdgeType Right;
            public TileEdgeType Down;
            public TileEdgeType Left;

            public IEnumerable<RotateStatus> ValidRotateStatuses(TileDirections tileDirections)
            {
                bool IsValid(TileEdgeType tileEdgeTypeA, TileEdgeType tileEdgeTypeB)
                {
                    if (tileEdgeTypeA == TileEdgeType.Line && tileEdgeTypeB == TileEdgeType.Solid)
                    {
                        return false;
                    }

                    if (tileEdgeTypeA == TileEdgeType.Solid && tileEdgeTypeB == TileEdgeType.Line)
                    {
                        return false;
                    }

                    return true;
                }

                var validRotateStatuses = new List<RotateStatus>();

                if (IsValid(Up, tileDirections.Up) &&
                    IsValid(Right, tileDirections.Right) &&
                    IsValid(Down, tileDirections.Down) &&
                    IsValid(Left, tileDirections.Left))
                {
                    validRotateStatuses.Add(RotateStatus.Rotate0);
                }

                if (IsValid(Up, tileDirections.Right) &&
                    IsValid(Right, tileDirections.Down) &&
                    IsValid(Down, tileDirections.Left) &&
                    IsValid(Left, tileDirections.Up))
                {
                    validRotateStatuses.Add(RotateStatus.Rotate90);
                }

                if (IsValid(Up, tileDirections.Down) &&
                    IsValid(Right, tileDirections.Left) &&
                    IsValid(Down, tileDirections.Up) &&
                    IsValid(Left, tileDirections.Right))
                {
                    validRotateStatuses.Add(RotateStatus.Rotate180);
                }

                if (IsValid(Up, tileDirections.Left) &&
                    IsValid(Right, tileDirections.Up) &&
                    IsValid(Down, tileDirections.Right) &&
                    IsValid(Left, tileDirections.Down))
                {
                    validRotateStatuses.Add(RotateStatus.Rotate270);
                }

                return validRotateStatuses;
            }
        }

        public void UpdateValidCellPositions()
        {
            _validCellPositionsModel.ClearValidCellPositions();

            var nextPositions = new Dictionary<LineDirection, Vector2Int>()
            {
                {LineDirection.Up, new Vector2Int(0, 1)},
                {LineDirection.Right, new Vector2Int(1, 0)},
                {LineDirection.Down, new Vector2Int(0, -1)},
                {LineDirection.Left, new Vector2Int(-1, 0)}
            };

            var dic = new Dictionary<Vector2Int, TileDirections>();

            foreach (var cellPosition in _mainBoardModel.CellPositions)
            {
                foreach (var offset in nextPositions.Values)
                {
                    var nextCellPosition = cellPosition + offset;
                    if (_mainBoardModel.GetPutTileId(nextCellPosition).HasValue || dic.ContainsKey(nextCellPosition))
                    {
                        continue;
                    }

                    if (IsInTheBoard(nextCellPosition))
                    {
                        var tileDirections = new TileDirections();

                        foreach (var (nextDirection, nextOffset)in nextPositions)
                        {
                            TileEdgeType tileEdgeType;
                            var nextNextCellPosition = nextCellPosition + nextOffset;
                            var nullableNextTileId = _mainBoardModel.GetPutTileId(nextNextCellPosition);
                            if (nullableNextTileId.HasValue)
                            {
                                var nextTileId = nullableNextTileId.Value;
                                var tileModel = _tilesModel.GetTileModel(nextTileId);
                                tileEdgeType = tileModel.GetTileEdgeType(nextDirection);
                            }
                            else if (IsInTheBoard(nextNextCellPosition))
                            {
                                tileEdgeType = TileEdgeType.Free;
                            }
                            else
                            {
                                tileEdgeType = TileEdgeType.Solid;
                            }

                            switch (nextDirection)
                            {
                                case LineDirection.Up:
                                {
                                    tileDirections.Up = tileEdgeType;
                                    break;
                                }
                                case LineDirection.Right:
                                {
                                    tileDirections.Right = tileEdgeType;
                                    break;
                                }
                                case LineDirection.Down:
                                {
                                    tileDirections.Down = tileEdgeType;
                                    break;
                                }
                                case LineDirection.Left:
                                {
                                    tileDirections.Left = tileEdgeType;
                                    break;
                                }
                            }
                        }

                        dic.Add(nextCellPosition, tileDirections);
                    }
                }
            }

            var validTileTypes = new Dictionary<TileType, TileDirections>()
            {
                {
                    TileType.Straight, new TileDirections
                    {
                        Up = TileEdgeType.Solid,
                        Right = TileEdgeType.Line,
                        Down = TileEdgeType.Solid,
                        Left = TileEdgeType.Line,
                    }
                },
                {
                    TileType.Curve, new TileDirections
                    {
                        Up = TileEdgeType.Line,
                        Right = TileEdgeType.Line,
                        Down = TileEdgeType.Solid,
                        Left = TileEdgeType.Solid
                    }
                },
                {
                    TileType.TwinCurves, new TileDirections
                    {
                        Up = TileEdgeType.Line,
                        Right = TileEdgeType.Line,
                        Down = TileEdgeType.Line,
                        Left = TileEdgeType.Line
                    }
                },
                {
                    TileType.Cross, new TileDirections
                    {
                        Up = TileEdgeType.Line,
                        Right = TileEdgeType.Line,
                        Down = TileEdgeType.Line,
                        Left = TileEdgeType.Line
                    }
                },
                {
                    TileType.Distributor3, new TileDirections
                    {
                        Up = TileEdgeType.Line,
                        Right = TileEdgeType.Line,
                        Down = TileEdgeType.Solid,
                        Left = TileEdgeType.Line
                    }
                },
                {
                    TileType.Distributor4, new TileDirections
                    {
                        Up = TileEdgeType.Line,
                        Right = TileEdgeType.Line,
                        Down = TileEdgeType.Line,
                        Left = TileEdgeType.Line
                    }
                },
                {
                    TileType.Bulb, new TileDirections
                    {
                        Up = TileEdgeType.Solid,
                        Right = TileEdgeType.Line,
                        Down = TileEdgeType.Solid,
                        Left = TileEdgeType.Solid
                    }
                }

            };

            foreach (var (tileType, tileDirectionsB)in validTileTypes)
            {
                var dicR = new Dictionary<Vector2Int, HashSet<RotateStatus>>();
                foreach (var (cellPosition, tileDirectionsA) in dic)
                {
                    var h = tileDirectionsB.ValidRotateStatuses(tileDirectionsA);
                    var validRotations = new HashSet<RotateStatus>();
                    foreach (var akio in h)
                    {
                        validRotations.Add(akio);
                    }

                    dicR.Add(cellPosition, validRotations);
                }

                _validCellPositionsModel.SetValidCellPositions(tileType, dicR);
            }
        }

        private bool IsInTheBoard(Vector2Int cellPosition)
        {
            return Mathf.Abs(cellPosition.x) <= 7 && Mathf.Abs(cellPosition.y) <= 6;
        }
    }
}