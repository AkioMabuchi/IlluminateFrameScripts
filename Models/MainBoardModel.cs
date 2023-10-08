using System.Collections.Generic;
using Enums;
using Interfaces.TileModels;
using Models.Instances.Tiles;
using Structs;
using UniRx;
using UnityEngine;

namespace Models
{
    public class MainBoardModel
    {
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

        private struct ConductQueueParams
        {
            public ElectricStatus electricStatus;
            public LineDirection lineDirectionPower;
            public LineDirection lineDirection;
            public Vector2Int cellPositionPower;
            public Vector2Int cellPosition;
            public List<LineDirectionPair> lineDirectionPairs;
        }

        private readonly ReactiveDictionary<Vector2Int, int> _reactiveDictionaryTileIds = new();

        private readonly ReactiveDictionary<Vector2Int, ITileModel>
            _reactiveDictionaryTileModels = new();

        private readonly ReactiveDictionary<Vector2Int, IResettableTileModel>
            _reactiveDictionaryResettableTileModels = new();

        private readonly ReactiveDictionary<Vector2Int, IPowerTileModel>
            _reactiveDictionaryPowerTileModels = new();

        private readonly ReactiveDictionary<Vector2Int, IConductiveTileModel>
            _reactiveDictionaryConductiveTileModels = new();

        private readonly ReactiveDictionary<Vector2Int, IBulbOrTerminalTileModel>
            _reactiveDictionaryBulbOrTerminalTileModels = new();

        private readonly ReactiveDictionary<Vector2Int, IRadiantTileModel>
            _reactiveDictionaryRadiantTileModels = new();

        private readonly ReactiveProperty<int> _reactivePropertyCellPositionMinX = new(0);
        private readonly ReactiveProperty<int> _reactivePropertyCellPositionMaxX = new(0);
        private readonly ReactiveProperty<int> _reactivePropertyCellPositionMinY = new(0);
        private readonly ReactiveProperty<int> _reactivePropertyCellPositionMaxY = new(0);
        private readonly HashSet<Vector2Int> _hashSetInitialTileCellPositions = new();

        public IEnumerable<int> AddedTileIds
        {
            get
            {
                var addedTileIds = new List<int>();

                for (var y = _reactivePropertyCellPositionMinY.Value;
                     y <= _reactivePropertyCellPositionMaxY.Value;
                     y++)
                {
                    for (var x = _reactivePropertyCellPositionMinX.Value;
                         x <= _reactivePropertyCellPositionMaxX.Value;
                         x++)
                    {
                        var cellPosition = new Vector2Int(x, y);
                        if (_hashSetInitialTileCellPositions.Contains(cellPosition))
                        {
                            continue;
                        }

                        if (_reactiveDictionaryTileIds.TryGetValue(cellPosition, out var tileId))
                        {
                            addedTileIds.Add(tileId);
                        }
                    }
                }

                return addedTileIds;
            }
        }

        public IReadOnlyDictionary<TileType, IReadOnlyDictionary<Vector2Int, IEnumerable<RotateStatus>>>
            ValidCellPositions
        {
            get
            {
                bool IsInBoard(Vector2Int cellPosition)
                {
                    return _reactivePropertyCellPositionMinX.Value <= cellPosition.x &&
                           cellPosition.x <= _reactivePropertyCellPositionMaxX.Value &&
                           _reactivePropertyCellPositionMinY.Value <= cellPosition.y &&
                           cellPosition.y <= _reactivePropertyCellPositionMaxY.Value;
                }



                var nextPositions = new Dictionary<LineDirection, Vector2Int>
                {
                    {LineDirection.Up, Vector2Int.up},
                    {LineDirection.Right, Vector2Int.right},
                    {LineDirection.Down, Vector2Int.down},
                    {LineDirection.Left, Vector2Int.left}
                };

                var dictionaryTileDirections = new Dictionary<Vector2Int, TileDirections>();

                foreach (var cellPosition in _reactiveDictionaryTileModels.Keys)
                {
                    foreach (var offset in nextPositions.Values)
                    {
                        var nextCellPosition = cellPosition + offset;
                        if (_reactiveDictionaryTileModels.ContainsKey(nextCellPosition) ||
                            dictionaryTileDirections.ContainsKey(nextCellPosition))
                        {
                            continue;
                        }

                        if (IsInBoard(cellPosition))
                        {
                            var tileDirections = new TileDirections();

                            foreach (var (nextDirection, nextOffset)in nextPositions)
                            {
                                TileEdgeType tileEdgeType;
                                var nextNextCellPosition = nextCellPosition + nextOffset;
                                if (_reactiveDictionaryTileModels.TryGetValue(nextNextCellPosition,
                                        out var hasEdgeTypeTileModel))
                                {
                                    tileEdgeType = hasEdgeTypeTileModel.GetTileEdgeType(nextDirection);
                                }
                                else if (IsInBoard(nextNextCellPosition))
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

                            dictionaryTileDirections.Add(nextCellPosition, tileDirections);
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
                        TileType.ThreeWayDistributor, new TileDirections
                        {
                            up = TileEdgeType.Line,
                            right = TileEdgeType.Line,
                            down = TileEdgeType.Solid,
                            left = TileEdgeType.Line
                        }
                    },
                    {
                        TileType.FourWayDistributor, new TileDirections
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

                var validCellPositions =
                    new Dictionary<TileType, IReadOnlyDictionary<Vector2Int, IEnumerable<RotateStatus>>>();

                foreach (var (tileType, tileDirectionsB)in validTileTypes)
                {
                    var validCellPosition = new Dictionary<Vector2Int, IEnumerable<RotateStatus>>();
                    var canBePut = false;
                    foreach (var (cellPosition, tileDirectionsA) in dictionaryTileDirections)
                    {
                        var validRotateStatuses =
                            new List<RotateStatus>(tileDirectionsB.ValidRotateStatuses(tileDirectionsA));
                        if (validRotateStatuses.Count >= 1)
                        {
                            canBePut = true;
                        }

                        validCellPosition.Add(cellPosition, validRotateStatuses);
                    }

                    if (canBePut)
                    {
                        validCellPositions.Add(tileType, validCellPosition);
                    }
                }

                return validCellPositions;
            }
        }

        public bool IsRadiant
        {
            get
            {
                foreach (var radiantTileModel in _reactiveDictionaryRadiantTileModels.Values)
                {
                    if (radiantTileModel.IsRadiant)
                    {
                        continue;
                    }

                    return false;
                }

                return true;
            }
        }

        public void Clear()
        {
            _reactiveDictionaryTileIds.Clear();
            
            _reactiveDictionaryTileModels.Clear();
            _reactiveDictionaryResettableTileModels.Clear();
            _reactiveDictionaryPowerTileModels.Clear();
            _reactiveDictionaryConductiveTileModels.Clear();
            _reactiveDictionaryBulbOrTerminalTileModels.Clear();
            _reactiveDictionaryRadiantTileModels.Clear();
            
            _hashSetInitialTileCellPositions.Clear();
        }
        public void Initialize(FrameSize frameSize)
        {
            switch (frameSize)
            {
                case FrameSize.Small:
                {
                    _reactivePropertyCellPositionMinX.Value = -4;
                    _reactivePropertyCellPositionMaxX.Value = 4;
                    _reactivePropertyCellPositionMinY.Value = -3;
                    _reactivePropertyCellPositionMaxY.Value = 3;
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(0, 0));
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(-4, -3));
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(4, -3));
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(-4, 3));
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(4, 3));
                    break;
                }
                case FrameSize.Medium:
                {
                    _reactivePropertyCellPositionMinX.Value = -6;
                    _reactivePropertyCellPositionMaxX.Value = 5;
                    _reactivePropertyCellPositionMinY.Value = -5;
                    _reactivePropertyCellPositionMaxY.Value = 5;
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(0, 0));
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(-1, 0));
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(-6, -5));
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(-6, 5));
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(5, -5));
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(5, 5));
                    break;
                }
                case FrameSize.Large:
                {
                    _reactivePropertyCellPositionMinX.Value = -7;
                    _reactivePropertyCellPositionMaxX.Value = 7;
                    _reactivePropertyCellPositionMinY.Value = -6;
                    _reactivePropertyCellPositionMaxY.Value = 6;
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(-1, 0));
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(0, 0));
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(1, 0));
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(-7, -6));
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(7, -6));
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(-7, 0));
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(7, 0));
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(-7, 6));
                    _hashSetInitialTileCellPositions.Add(new Vector2Int(7, 6));
                    break;
                }
                default:
                {
                    _reactivePropertyCellPositionMinX.Value = 0;
                    _reactivePropertyCellPositionMaxX.Value = 0;
                    _reactivePropertyCellPositionMinY.Value = 0;
                    _reactivePropertyCellPositionMaxY.Value = 0;
                    break;
                }
            }
        }

        public void PutTile(Vector2Int cellPosition, int tileId, TileModelBase tileModel)
        {
            _reactiveDictionaryTileIds.Add(cellPosition, tileId);

            _reactiveDictionaryTileModels.Add(cellPosition, tileModel as ITileModel);

            if (tileModel is IResettableTileModel resettableTileModel)
            {
                _reactiveDictionaryResettableTileModels.Add(cellPosition, resettableTileModel);
            }

            if (tileModel is IPowerTileModel powerTileModel)
            {
                _reactiveDictionaryPowerTileModels.Add(cellPosition, powerTileModel);
            }

            if (tileModel is IConductiveTileModel conductiveTileModel)
            {
                _reactiveDictionaryConductiveTileModels.Add(cellPosition, conductiveTileModel);
            }

            if (tileModel is IBulbOrTerminalTileModel bulbOrTerminalTileModel)
            {
                _reactiveDictionaryBulbOrTerminalTileModels.Add(cellPosition, bulbOrTerminalTileModel);
            }

            if (tileModel is IRadiantTileModel radiantTileModel)
            {
                _reactiveDictionaryRadiantTileModels.Add(cellPosition, radiantTileModel);
            }
        }

        public bool TryGetPutTileId(Vector2Int cellPosition, out int tileId)
        {
            if (_reactiveDictionaryTileIds.TryGetValue(cellPosition, out var returnTileId))
            {
                tileId = returnTileId;
                return true;
            }

            tileId = default;
            return false;
        }

        public void Reset()
        {
            foreach (var resettableTileModel in _reactiveDictionaryResettableTileModels.Values)
            {
                resettableTileModel.Reset();
            }

            for (var x = _reactivePropertyCellPositionMinX.Value; x <= _reactivePropertyCellPositionMaxX.Value; x++)
            {
                for (var y = _reactivePropertyCellPositionMinY.Value; y <= _reactivePropertyCellPositionMaxY.Value; y++)
                {
                    var cellPosition = new Vector2Int(x, y);
                    if (_hashSetInitialTileCellPositions.Contains(cellPosition))
                    {
                        continue;
                    }
                    
                    _reactiveDictionaryTileIds.Remove(cellPosition);
                    _reactiveDictionaryTileModels.Remove(cellPosition);
                    _reactiveDictionaryResettableTileModels.Remove(cellPosition);
                    _reactiveDictionaryPowerTileModels.Remove(cellPosition);
                    _reactiveDictionaryConductiveTileModels.Remove(cellPosition);
                    _reactiveDictionaryBulbOrTerminalTileModels.Remove(cellPosition);
                    _reactiveDictionaryRadiantTileModels.Remove(cellPosition);
                }
            }
        }

        public ConductBoardResult Conduct()
        {
            var isCircuitShorted = false;
            var isCircuitClosed = true;
            var scoredTiles = new Dictionary<Vector2Int, ScoredTile>();
            var illuminatedCircuitPaths = new List<CircuitPath>();
            var shortedCircuitPaths = new List<CircuitPath>();

            var conductQueue = new Queue<ConductQueueParams>();
            var conductedDictionary = new Dictionary<Vector2Int, HashSet<LineDirection>>();
            foreach (var (cellPosition, powerTileModel) in _reactiveDictionaryPowerTileModels)
            {
                conductQueue.Clear();
                conductedDictionary.Clear();

                foreach (var output in powerTileModel.PowerOutputs)
                {
                    conductQueue.Enqueue(new ConductQueueParams
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

                while (conductQueue.Count > 0)
                {
                    var akio = conductQueue.Dequeue();
                    if (conductedDictionary.TryGetValue(akio.cellPosition, out var hashSet))
                    {
                        if (hashSet.Contains(akio.lineDirection))
                        {
                            continue;
                        }

                        hashSet.Add(akio.lineDirection);
                    }
                    else
                    {
                        conductedDictionary.Add(akio.cellPosition, new HashSet<LineDirection>
                        {
                            akio.lineDirection
                        });
                    }

                    var nextCellPosition = akio.cellPosition + akio.lineDirection switch
                    {
                        LineDirection.Up => Vector2Int.up,
                        LineDirection.Right => Vector2Int.right,
                        LineDirection.Down => Vector2Int.down,
                        LineDirection.Left => Vector2Int.left,
                        _ => Vector2Int.zero
                    };

                    if (_reactiveDictionaryConductiveTileModels.TryGetValue(nextCellPosition,
                            out var conductiveTileModel))
                    {
                        foreach (var output in conductiveTileModel.Conduct(akio.electricStatus, akio.lineDirection))
                        {
                            var lineDirectionPairs = new List<LineDirectionPair>(akio.lineDirectionPairs)
                            {
                                new()
                                {
                                    lineDirectionInput = akio.lineDirection,
                                    lineDirectionOutput = output.lineDirection
                                }
                            };

                            if (output.isShorted)
                            {
                                isCircuitShorted = true;
                                shortedCircuitPaths.Add(new CircuitPath
                                {
                                    electricStatus = akio.electricStatus,
                                    cellPosition = akio.cellPositionPower,
                                    lineDirectionPairs = lineDirectionPairs
                                });
                            }

                            if (output.score > 0)
                            {
                                if (scoredTiles.TryGetValue(nextCellPosition, out var scoredTile))
                                {
                                    scoredTile.score += output.score;
                                    switch (scoredTile.textEffectMaterialType)
                                    {
                                        case TextEffectMaterialType.ElectricNormal:
                                        {
                                            switch (akio.electricStatus)
                                            {
                                                case ElectricStatus.Normal:
                                                {
                                                    break;
                                                }
                                                default:
                                                {
                                                    scoredTile.textEffectMaterialType =
                                                        TextEffectMaterialType.ElectricMixed;
                                                    break;
                                                }
                                            }

                                            break;
                                        }
                                        case TextEffectMaterialType.ElectricPlus:
                                        {
                                            switch (akio.electricStatus)
                                            {
                                                case ElectricStatus.Plus:
                                                {
                                                    break;
                                                }
                                                default:
                                                {
                                                    scoredTile.textEffectMaterialType =
                                                        TextEffectMaterialType.ElectricMixed;
                                                    break;
                                                }
                                            }

                                            break;
                                        }
                                        case TextEffectMaterialType.ElectricMinus:
                                        {
                                            switch (akio.electricStatus)
                                            {
                                                case ElectricStatus.Minus:
                                                {
                                                    break;
                                                }
                                                default:
                                                {
                                                    scoredTile.textEffectMaterialType =
                                                        TextEffectMaterialType.ElectricMixed;
                                                    break;
                                                }
                                            }

                                            break;
                                        }
                                        case TextEffectMaterialType.ElectricAlternating:
                                        {
                                            switch (akio.electricStatus)
                                            {
                                                case ElectricStatus.Alternating:
                                                {
                                                    break;
                                                }
                                                default:
                                                {
                                                    scoredTile.textEffectMaterialType =
                                                        TextEffectMaterialType.ElectricMixed;
                                                    break;
                                                }
                                            }

                                            break;
                                        }
                                    }

                                    scoredTiles[nextCellPosition] = scoredTile;
                                }
                                else
                                {
                                    scoredTiles.Add(nextCellPosition, new ScoredTile
                                    {
                                        score = output.score,
                                        textEffectMaterialType = akio.electricStatus switch
                                        {
                                            ElectricStatus.Normal => TextEffectMaterialType.ElectricNormal,
                                            ElectricStatus.Plus => TextEffectMaterialType.ElectricPlus,
                                            ElectricStatus.Minus => TextEffectMaterialType.ElectricMinus,
                                            ElectricStatus.Alternating => TextEffectMaterialType.ElectricAlternating,
                                            _ => TextEffectMaterialType.None
                                        }
                                    });
                                }
                            }
                            if (output.terminalType != TerminalType.None)
                            {
                                illuminatedCircuitPaths.Add(new CircuitPath
                                {
                                    electricStatus = akio.electricStatus,
                                    cellPosition = akio.cellPositionPower,
                                    lineDirectionPairs = lineDirectionPairs
                                });
                            }

                            conductQueue.Enqueue(new ConductQueueParams
                            {
                                electricStatus = akio.electricStatus,
                                lineDirectionPower = akio.lineDirectionPower,
                                lineDirection = output.lineDirection,
                                cellPositionPower = akio.cellPositionPower,
                                cellPosition = nextCellPosition,
                                lineDirectionPairs = lineDirectionPairs
                            });
                        }
                    }
                    else
                    {
                        isCircuitClosed = false;
                    }
                }
            }

            return new ConductBoardResult
            {
                isCircuitShorted = isCircuitShorted,
                isCircuitClosed = isCircuitClosed,
                illuminatedCircuitPaths = illuminatedCircuitPaths,
                shortedCircuitPaths = shortedCircuitPaths,
                scoredTiles = scoredTiles
            };
        }

        public TutorialConductResult TutorialConduct(Vector2Int cellPosition, TileModelBase tileModel)
        {
            var isCircuitShorted = false;
            var isCircuitClosed = true;
            var isBulbIlluminated = false;
            
            var powerTileModels = new Dictionary<Vector2Int, IPowerTileModel>();
            var conductiveTileModels = new Dictionary<Vector2Int, IConductiveTileModel>();

            foreach (var (placedCellPosition, placedPowerTileModel) in _reactiveDictionaryPowerTileModels)
            {
                powerTileModels.Add(placedCellPosition, placedPowerTileModel.DuplicatedPowerTileModel);
            }

            foreach (var (placedCellPosition, placedConductiveTileModel)in _reactiveDictionaryConductiveTileModels)
            {
                conductiveTileModels.Add(placedCellPosition, placedConductiveTileModel.DuplicatedConductiveTileModel);
            }

            if (tileModel is IPowerTileModel putPowerTileModel)
            {
                powerTileModels.Add(cellPosition, putPowerTileModel.DuplicatedPowerTileModel);
            }

            if (tileModel is IConductiveTileModel putConductiveTileModel)
            {
                conductiveTileModels.Add(cellPosition, putConductiveTileModel.DuplicatedConductiveTileModel);
            }
            
            var conductQueue = new Queue<ConductQueueParams>();
            var conductedDictionary = new Dictionary<Vector2Int, HashSet<LineDirection>>();

            foreach (var (startCellPosition, startPowerTileModel) in powerTileModels)
            {
                conductQueue.Clear();
                conductedDictionary.Clear();

                foreach (var output in startPowerTileModel.PowerOutputs)
                {
                    conductQueue.Enqueue(new ConductQueueParams
                    {
                        electricStatus = output.electricStatus,
                        lineDirectionPower = output.lineDirection,
                        lineDirection = output.lineDirection,
                        cellPositionPower = startCellPosition,
                        cellPosition = startCellPosition,
                        lineDirectionPairs = new List<LineDirectionPair>
                        {
                            new()
                            {
                                lineDirectionOutput = output.lineDirection
                            }
                        }
                    });
                }

                while (conductQueue.Count > 0)
                {
                    var akio = conductQueue.Dequeue();
                    if (conductedDictionary.TryGetValue(akio.cellPosition, out var hashSet))
                    {
                        if (hashSet.Contains(akio.lineDirection))
                        {
                            continue;
                        }

                        hashSet.Add(akio.lineDirection);
                    }
                    else
                    {
                        conductedDictionary.Add(akio.cellPosition, new HashSet<LineDirection>
                        {
                            akio.lineDirection
                        });
                    }

                    var nextCellPosition = akio.cellPosition + akio.lineDirection switch
                    {
                        LineDirection.Up => Vector2Int.up,
                        LineDirection.Right => Vector2Int.right,
                        LineDirection.Down => Vector2Int.down,
                        LineDirection.Left => Vector2Int.left,
                        _ => Vector2Int.zero
                    };

                    if (conductiveTileModels.TryGetValue(nextCellPosition, out var conductiveTileModel))
                    {
                        foreach (var output in conductiveTileModel.Conduct(akio.electricStatus, akio.lineDirection))
                        {
                            var lineDirectionPairs = new List<LineDirectionPair>(akio.lineDirectionPairs)
                            {
                                new()
                                {
                                    lineDirectionInput = akio.lineDirection,
                                    lineDirectionOutput = output.lineDirection
                                }
                            };

                            if (output.isShorted)
                            {
                                isCircuitShorted = true;
                            }

                            switch (output.terminalType)
                            {
                                case TerminalType.Bulb:
                                {
                                    isBulbIlluminated = true;
                                    break;
                                }
                            }

                            conductQueue.Enqueue(new ConductQueueParams
                            {
                                electricStatus = akio.electricStatus,
                                lineDirectionPower = akio.lineDirectionPower,
                                lineDirection = output.lineDirection,
                                cellPositionPower = akio.cellPositionPower,
                                cellPosition = nextCellPosition,
                                lineDirectionPairs = lineDirectionPairs
                            });
                        }
                    }
                    else
                    {
                        isCircuitClosed = false;
                    }
                }
            }

            if (isCircuitShorted)
            {
                return TutorialConductResult.Shorted;
            }

            if (isCircuitClosed)
            {
                return TutorialConductResult.Closed;
            }

            if (isBulbIlluminated)
            {
                return TutorialConductResult.BulbIlluminated;
            }

            return TutorialConductResult.None;
        }

        public IlluminateBulbOrTerminalResult IlluminateBulbOrTerminal(Vector2Int cellPosition,
            ElectricStatus electricStatus)
        {
            if (_reactiveDictionaryBulbOrTerminalTileModels.TryGetValue(cellPosition, out var bulbOrTerminalTileModel))
            {
                return bulbOrTerminalTileModel.IlluminateBulbOrTerminal(electricStatus);
            }

            return IlluminateBulbOrTerminalResult.None;
        }
    }
}