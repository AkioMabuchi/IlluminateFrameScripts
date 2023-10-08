using System;
using System.Collections.Generic;
using Enums;
using Interfaces.TileModels;
using Structs;
using UniRx;

namespace Models.Instances.Tiles.Dynamics
{
    public class ThreeWayDistributorTileModel : TileModelBase, IResettableTileModel, ICheckableValidTileModel,
        ITileModel, IRotatableTileModel, IConductiveTileModel, IRadiantTileModel
    {
        private readonly ReactiveProperty<RotateStatus> _reactivePropertyRotateStatus = new(RotateStatus.Rotate0);
        public IObservable<RotateStatus> OnChangedRotateStatus => _reactivePropertyRotateStatus;
        public RotateStatus RotateStatus => _reactivePropertyRotateStatus.Value;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLineA = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusLineA => _reactivePropertyElectricStatusLineA;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLineB = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusLineB => _reactivePropertyElectricStatusLineB;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLineC = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusLineC => _reactivePropertyElectricStatusLineC;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusCore = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusCore => _reactivePropertyElectricStatusCore;

        public override TileType TileType => TileType.ThreeWayDistributor;

        public ThreeWayDistributorTileModel()
        {
            
        }
        
        private ThreeWayDistributorTileModel(ThreeWayDistributorTileModel threeWayDistributorTileModel)
        {
            _reactivePropertyRotateStatus.Value = threeWayDistributorTileModel._reactivePropertyRotateStatus.Value;
            _reactivePropertyElectricStatusLineA.Value =
                threeWayDistributorTileModel._reactivePropertyElectricStatusLineA.Value;
            _reactivePropertyElectricStatusLineB.Value =
                threeWayDistributorTileModel._reactivePropertyElectricStatusLineB.Value;
            _reactivePropertyElectricStatusLineC.Value =
                threeWayDistributorTileModel._reactivePropertyElectricStatusLineC.Value;
            _reactivePropertyElectricStatusCore.Value =
                threeWayDistributorTileModel._reactivePropertyElectricStatusCore.Value;
        }
        public void Reset()
        {
            _reactivePropertyElectricStatusLineA.Value = ElectricStatus.None;
            _reactivePropertyElectricStatusLineB.Value = ElectricStatus.None;
            _reactivePropertyElectricStatusLineC.Value = ElectricStatus.None;
            _reactivePropertyElectricStatusCore.Value = ElectricStatus.None;
        }

        public TileEdgeType GetTileEdgeType(LineDirection lineDirection)
        {
            switch (_reactivePropertyRotateStatus.Value)
            {
                case RotateStatus.Rotate0:
                {
                    switch (lineDirection)
                    {
                        case LineDirection.Right:
                        case LineDirection.Down:
                        case LineDirection.Left:
                        {
                            return TileEdgeType.Line;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate90:
                {
                    switch (lineDirection)
                    {
                        case LineDirection.Down:
                        case LineDirection.Left:
                        case LineDirection.Up:
                        {
                            return TileEdgeType.Line;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate180:
                {
                    switch (lineDirection)
                    {

                        case LineDirection.Left:
                        case LineDirection.Up:
                        case LineDirection.Right:
                        {
                            return TileEdgeType.Line;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate270:
                {
                    switch (lineDirection)
                    {
                        case LineDirection.Up:
                        case LineDirection.Right:
                        case LineDirection.Down:
                        {
                            return TileEdgeType.Line;
                        }
                    }

                    break;
                }
            }

            return TileEdgeType.Solid;
        }

        public void Rotate()
        {
            _reactivePropertyRotateStatus.Value = _reactivePropertyRotateStatus.Value switch
            {
                RotateStatus.Rotate0 => RotateStatus.Rotate90,
                RotateStatus.Rotate90 => RotateStatus.Rotate180,
                RotateStatus.Rotate180 => RotateStatus.Rotate270,
                RotateStatus.Rotate270 => RotateStatus.Rotate0,
                _ => RotateStatus.Rotate0
            };
        }

        public IEnumerable<ConductOutput> Conduct(ElectricStatus electricStatus, LineDirection lineDirection)
        {
            const int conductScore = 10;

            var outputs = new List<ConductOutput>();
            var score = 0;

            switch (_reactivePropertyRotateStatus.Value)
            {
                case RotateStatus.Rotate0:
                {
                    switch (lineDirection)
                    {
                        case LineDirection.Right:
                        {
                            if (_reactivePropertyElectricStatusCore.Value == ElectricStatus.None)
                            {
                                score = conductScore;
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            _reactivePropertyElectricStatusLineC.Value = electricStatus;

                            _reactivePropertyElectricStatusCore.Value = electricStatus;

                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Up
                            });
                            outputs.Add(new ConductOutput
                            {
                                lineDirection = LineDirection.Right
                            });
                            break;
                        }
                        case LineDirection.Down:
                        {
                            if (_reactivePropertyElectricStatusCore.Value == ElectricStatus.None)
                            {
                                score = conductScore;
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            _reactivePropertyElectricStatusLineC.Value = electricStatus;

                            _reactivePropertyElectricStatusCore.Value = electricStatus;

                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Right
                            });
                            outputs.Add(new ConductOutput
                            {
                                lineDirection = LineDirection.Left
                            });
                            break;
                        }
                        case LineDirection.Left:
                        {
                            if (_reactivePropertyElectricStatusCore.Value == ElectricStatus.None)
                            {
                                score = conductScore;
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            _reactivePropertyElectricStatusLineC.Value = electricStatus;

                            _reactivePropertyElectricStatusCore.Value = electricStatus;

                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Up
                            });
                            outputs.Add(new ConductOutput
                            {
                                lineDirection = LineDirection.Left
                            });
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate90:
                {
                    switch (lineDirection)
                    {
                        case LineDirection.Up:
                        {
                            if (_reactivePropertyElectricStatusCore.Value == ElectricStatus.None)
                            {
                                score = conductScore;
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            _reactivePropertyElectricStatusLineC.Value = electricStatus;

                            _reactivePropertyElectricStatusCore.Value = electricStatus;

                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Up
                            });
                            outputs.Add(new ConductOutput
                            {
                                lineDirection = LineDirection.Right
                            });
                            break;
                        }
                        case LineDirection.Down:
                        {
                            if (_reactivePropertyElectricStatusCore.Value == ElectricStatus.None)
                            {
                                score = conductScore;
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            _reactivePropertyElectricStatusLineC.Value = electricStatus;

                            _reactivePropertyElectricStatusCore.Value = electricStatus;

                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Right
                            });
                            outputs.Add(new ConductOutput
                            {
                                lineDirection = LineDirection.Down
                            });
                            break;
                        }
                        case LineDirection.Left:
                        {
                            if (_reactivePropertyElectricStatusCore.Value == ElectricStatus.None)
                            {
                                score = conductScore;
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            _reactivePropertyElectricStatusLineC.Value = electricStatus;

                            _reactivePropertyElectricStatusCore.Value = electricStatus;

                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Up
                            });
                            outputs.Add(new ConductOutput
                            {
                                lineDirection = LineDirection.Down
                            });
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate180:
                {
                    switch (lineDirection)
                    {
                        case LineDirection.Up:
                        {
                            if (_reactivePropertyElectricStatusCore.Value == ElectricStatus.None)
                            {
                                score = conductScore;
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            _reactivePropertyElectricStatusLineC.Value = electricStatus;

                            _reactivePropertyElectricStatusCore.Value = electricStatus;

                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Right
                            });
                            outputs.Add(new ConductOutput
                            {
                                lineDirection = LineDirection.Left
                            });
                            break;
                        }
                        case LineDirection.Right:
                        {
                            if (_reactivePropertyElectricStatusCore.Value == ElectricStatus.None)
                            {
                                score = conductScore;
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            _reactivePropertyElectricStatusLineC.Value = electricStatus;

                            _reactivePropertyElectricStatusCore.Value = electricStatus;

                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Right
                            });
                            outputs.Add(new ConductOutput
                            {
                                lineDirection = LineDirection.Down
                            });
                            break;
                        }
                        case LineDirection.Left:
                        {
                            if (_reactivePropertyElectricStatusCore.Value == ElectricStatus.None)
                            {
                                score = conductScore;
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            _reactivePropertyElectricStatusLineC.Value = electricStatus;

                            _reactivePropertyElectricStatusCore.Value = electricStatus;

                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Down
                            });
                            outputs.Add(new ConductOutput
                            {
                                lineDirection = LineDirection.Left
                            });
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate270:
                {
                    switch (lineDirection)
                    {
                        case LineDirection.Up:
                        {
                            if (_reactivePropertyElectricStatusCore.Value == ElectricStatus.None)
                            {
                                score = conductScore;
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            _reactivePropertyElectricStatusLineC.Value = electricStatus;

                            _reactivePropertyElectricStatusCore.Value = electricStatus;

                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Up
                            });
                            outputs.Add(new ConductOutput
                            {
                                lineDirection = LineDirection.Left
                            });
                            break;
                        }
                        case LineDirection.Right:
                        {
                            if (_reactivePropertyElectricStatusCore.Value == ElectricStatus.None)
                            {
                                score = conductScore;
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            _reactivePropertyElectricStatusLineC.Value = electricStatus;

                            _reactivePropertyElectricStatusCore.Value = electricStatus;

                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Up
                            });
                            outputs.Add(new ConductOutput
                            {
                                lineDirection = LineDirection.Down
                            });
                            break;
                        }
                        case LineDirection.Down:
                        {
                            if (_reactivePropertyElectricStatusCore.Value == ElectricStatus.None)
                            {
                                score = conductScore;
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            _reactivePropertyElectricStatusLineC.Value = electricStatus;

                            _reactivePropertyElectricStatusCore.Value = electricStatus;

                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Down
                            });
                            outputs.Add(new ConductOutput
                            {
                                lineDirection = LineDirection.Left
                            });
                            break;
                        }
                    }

                    break;
                }
            }

            return outputs;
        }

        public IConductiveTileModel DuplicatedConductiveTileModel => new ThreeWayDistributorTileModel(this);

        public bool IsRadiant
        {
            get
            {
                switch (_reactivePropertyElectricStatusCore.Value)
                {
                    case ElectricStatus.None:
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}