using System.Collections.Generic;
using Enums;
using Models;
using UnityEngine;
using VContainer;

namespace Processes
{
    public class UpdateValidCellPositionsProcess
    {
        private readonly MainBoardModel _mainBoardModel;
        private readonly MainFrameModel _mainFrameModel;
        private readonly TilesModel _tilesModel;
        private readonly ValidCellPositionsModel _validCellPositionsModel;

        [Inject]
        public UpdateValidCellPositionsProcess(MainBoardModel mainBoardModel, MainFrameModel mainFrameModel,
            TilesModel tilesModel, ValidCellPositionsModel validCellPositionsModel)
        {
            _mainBoardModel = mainBoardModel;
            _mainFrameModel = mainFrameModel;
            _tilesModel = tilesModel;
            _validCellPositionsModel = validCellPositionsModel;
        }

        private struct TileDirections
        {
            public TileEdgeType up;
            public TileEdgeType right;
            public TileEdgeType down;
            public TileEdgeType left;

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

                if (IsValid(up, tileDirections.up) &&
                    IsValid(right, tileDirections.right) &&
                    IsValid(down, tileDirections.down) &&
                    IsValid(left, tileDirections.left))
                {
                    validRotateStatuses.Add(RotateStatus.Rotate0);
                }

                if (IsValid(up, tileDirections.right) &&
                    IsValid(right, tileDirections.down) &&
                    IsValid(down, tileDirections.left) &&
                    IsValid(left, tileDirections.up))
                {
                    validRotateStatuses.Add(RotateStatus.Rotate90);
                }

                if (IsValid(up, tileDirections.down) &&
                    IsValid(right, tileDirections.left) &&
                    IsValid(down, tileDirections.up) &&
                    IsValid(left, tileDirections.right))
                {
                    validRotateStatuses.Add(RotateStatus.Rotate180);
                }

                if (IsValid(up, tileDirections.left) &&
                    IsValid(right, tileDirections.up) &&
                    IsValid(down, tileDirections.right) &&
                    IsValid(left, tileDirections.down))
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

                    if (_mainFrameModel.Frame.IsInBoard(nextCellPosition))
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
                            else if (_mainFrameModel.Frame.IsInBoard(nextNextCellPosition))
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
                                    tileDirections.up = tileEdgeType;
                                    break;
                                }
                                case LineDirection.Right:
                                {
                                    tileDirections.right = tileEdgeType;
                                    break;
                                }
                                case LineDirection.Down:
                                {
                                    tileDirections.down = tileEdgeType;
                                    break;
                                }
                                case LineDirection.Left:
                                {
                                    tileDirections.left = tileEdgeType;
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
                        up = TileEdgeType.Solid,
                        right = TileEdgeType.Line,
                        down = TileEdgeType.Solid,
                        left = TileEdgeType.Line,
                    }
                },
                {
                    TileType.Curve, new TileDirections
                    {
                        up = TileEdgeType.Line,
                        right = TileEdgeType.Line,
                        down = TileEdgeType.Solid,
                        left = TileEdgeType.Solid
                    }
                },
                {
                    TileType.TwinCurves, new TileDirections
                    {
                        up = TileEdgeType.Line,
                        right = TileEdgeType.Line,
                        down = TileEdgeType.Line,
                        left = TileEdgeType.Line
                    }
                },
                {
                    TileType.Cross, new TileDirections
                    {
                        up = TileEdgeType.Line,
                        right = TileEdgeType.Line,
                        down = TileEdgeType.Line,
                        left = TileEdgeType.Line
                    }
                },
                {
                    TileType.Distributor3, new TileDirections
                    {
                        up = TileEdgeType.Line,
                        right = TileEdgeType.Line,
                        down = TileEdgeType.Solid,
                        left = TileEdgeType.Line
                    }
                },
                {
                    TileType.Distributor4, new TileDirections
                    {
                        up = TileEdgeType.Line,
                        right = TileEdgeType.Line,
                        down = TileEdgeType.Line,
                        left = TileEdgeType.Line
                    }
                },
                {
                    TileType.Bulb, new TileDirections
                    {
                        up = TileEdgeType.Solid,
                        right = TileEdgeType.Line,
                        down = TileEdgeType.Solid,
                        left = TileEdgeType.Solid
                    }
                }

            };

            foreach (var (tileType, tileDirectionsB)in validTileTypes)
            {
                var dicR = new Dictionary<Vector2Int, HashSet<RotateStatus>>();
                var canBePut = false;
                foreach (var (cellPosition, tileDirectionsA) in dic)
                {
                    var h = tileDirectionsB.ValidRotateStatuses(tileDirectionsA);
                    var validRotations = new HashSet<RotateStatus>();
                    foreach (var akio in h)
                    {
                        canBePut = true;
                        validRotations.Add(akio);
                    }

                    dicR.Add(cellPosition, validRotations);
                }

                if (canBePut)
                {
                    _validCellPositionsModel.SetValidCellPositions(tileType, dicR);
                }
            }
        }
    }
}