using System;
using System.Collections.Generic;
using Enums;
using Interfaces.TileModels;
using Structs;
using UniRx;

namespace Models.Instances.Tiles.Dynamics
{
    public class TwinCurvesTileModel : TileModelBase, IResettableTileModel, ICheckableValidTileModel,
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

        public override TileType TileType => TileType.TwinCurves;

        public TwinCurvesTileModel()
        {
            
        }

        private TwinCurvesTileModel(TwinCurvesTileModel twinCurvesTileModel)
        {
            _reactivePropertyRotateStatus.Value = twinCurvesTileModel._reactivePropertyRotateStatus.Value;
            _reactivePropertyElectricStatusLineA.Value =
                twinCurvesTileModel._reactivePropertyElectricStatusLineA.Value;
            _reactivePropertyElectricStatusLineB.Value =
                twinCurvesTileModel._reactivePropertyElectricStatusLineB.Value;
        }

        public void Reset()
        {
            _reactivePropertyElectricStatusLineA.Value = ElectricStatus.None;
            _reactivePropertyElectricStatusLineB.Value = ElectricStatus.None;
        }

        public TileEdgeType GetTileEdgeType(LineDirection lineDirection)
        {
            return TileEdgeType.Line;
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
            const int conductScoreFirst = 8;
            const int conductScoreSecond = 4;

            var outputs = new List<ConductOutput>();
            var score = 0;
            
            switch (_reactivePropertyRotateStatus.Value)
            {
                case RotateStatus.Rotate0:
                {
                    switch (lineDirection)
                    {
                        case LineDirection.Up:
                        {
                            if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                            {
                                if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                                {
                                    score = conductScoreFirst;
                                }
                                else
                                {
                                    score = conductScoreSecond;
                                }
                            }

                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Left
                            });
                            break;
                        }
                        case LineDirection.Right:
                        {
                            if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                            {
                                if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                                {
                                    score = conductScoreFirst;
                                }
                                else
                                {
                                    score = conductScoreSecond;
                                }
                            }

                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Down
                            });
                            break;
                        }
                        case LineDirection.Down:
                        {
                            if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                            {
                                if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                                {
                                    score = conductScoreFirst;
                                }
                                else
                                {
                                    score = conductScoreSecond;
                                }
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Right
                            });
                            break;
                        }
                        case LineDirection.Left:
                        {
                            if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                            {
                                if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                                {
                                    score = conductScoreFirst;
                                }
                                else
                                {
                                    score = conductScoreSecond;
                                }
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Up
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
                            if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                            {
                                if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                                {
                                    score = conductScoreFirst;
                                }
                                else
                                {
                                    score = conductScoreSecond;
                                }
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Right
                            });
                            break;
                        }
                        case LineDirection.Right:
                        {
                            if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                            {
                                if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                                {
                                    score = conductScoreFirst;
                                }
                                else
                                {
                                    score = conductScoreSecond;
                                }
                            }

                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Up
                            });
                            break;
                        }
                        case LineDirection.Down:
                        {
                            if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                            {
                                if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                                {
                                    score = conductScoreFirst;
                                }
                                else
                                {
                                    score = conductScoreSecond;
                                }
                            }

                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Left
                            });
                            break;
                        }
                        case LineDirection.Left:
                        {
                            if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                            {
                                if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                                {
                                    score = conductScoreFirst;
                                }
                                else
                                {
                                    score = conductScoreSecond;
                                }
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
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
                            if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                            {
                                if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                                {
                                    score = conductScoreFirst;
                                }
                                else
                                {
                                    score = conductScoreSecond;
                                }
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Left
                            });
                            break;
                        }
                        case LineDirection.Right:
                        {
                            if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                            {
                                if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                                {
                                    score = conductScoreFirst;
                                }
                                else
                                {
                                    score = conductScoreSecond;
                                }
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Down
                            });
                            break;
                        }
                        case LineDirection.Down:
                        {
                            if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                            {
                                if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                                {
                                    score = conductScoreFirst;
                                }
                                else
                                {
                                    score = conductScoreSecond;
                                }
                            }

                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Right
                            });
                            break;
                        }
                        case LineDirection.Left:
                        {
                            if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                            {
                                if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                                {
                                    score = conductScoreFirst;
                                }
                                else
                                {
                                    score = conductScoreSecond;
                                }
                            }

                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Up
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
                            if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                            {
                                if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                                {
                                    score = conductScoreFirst;
                                }
                                else
                                {
                                    score = conductScoreSecond;
                                }
                            }

                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Right
                            });
                            break;
                        }
                        case LineDirection.Right:
                        {
                            if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                            {
                                if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                                {
                                    score = conductScoreFirst;
                                }
                                else
                                {
                                    score = conductScoreSecond;
                                }
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Up
                            });
                            break;
                        }
                        case LineDirection.Down:
                        {
                            if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                            {
                                if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                                {
                                    score = conductScoreFirst;
                                }
                                else
                                {
                                    score = conductScoreSecond;
                                }
                            }

                            _reactivePropertyElectricStatusLineA.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Left
                            });
                            break;
                        }
                        case LineDirection.Left:
                        {
                            if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                            {
                                if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                                {
                                    score = conductScoreFirst;
                                }
                                else
                                {
                                    score = conductScoreSecond;
                                }
                            }

                            _reactivePropertyElectricStatusLineB.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Down
                            });
                            break;
                        }
                    }

                    break;
                }
            }
            
            return outputs;
        }

        public IConductiveTileModel DuplicatedConductiveTileModel => new TwinCurvesTileModel(this);

        public bool IsRadiant
        {
            get
            {
                switch (_reactivePropertyElectricStatusLineA.Value)
                {
                    case ElectricStatus.None:
                    {
                        return false;
                    }
                }

                switch (_reactivePropertyElectricStatusLineB.Value)
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