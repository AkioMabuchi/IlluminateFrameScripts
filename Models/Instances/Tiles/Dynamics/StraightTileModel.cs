using System;
using System.Collections.Generic;
using Enums;
using Interfaces.TileModels;
using Structs;
using UniRx;

namespace Models.Instances.Tiles.Dynamics
{
    public class StraightTileModel : TileModelBase, IResettableTileModel, ICheckableValidTileModel,
        ITileModel, IRotatableTileModel, IConductiveTileModel, IRadiantTileModel
    {
        private readonly ReactiveProperty<RotateStatus> _reactivePropertyRotateStatus = new(RotateStatus.Rotate0);
        public IObservable<RotateStatus> OnChangedRotateStatus => _reactivePropertyRotateStatus;
        public RotateStatus RotateStatus => _reactivePropertyRotateStatus.Value;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLine = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusLine => _reactivePropertyElectricStatusLine;
        public override TileType TileType => TileType.Straight;

        public StraightTileModel()
        {
            
        }

        private StraightTileModel(StraightTileModel straightTileModel)
        {
            _reactivePropertyRotateStatus.Value = straightTileModel._reactivePropertyRotateStatus.Value;
            _reactivePropertyElectricStatusLine.Value = straightTileModel._reactivePropertyElectricStatusLine.Value;
        }
        public void Reset()
        {
            _reactivePropertyElectricStatusLine.Value = ElectricStatus.None;
        }
        
        public TileEdgeType GetTileEdgeType(LineDirection lineDirection)
        {
            switch (_reactivePropertyRotateStatus.Value)
            {
                case RotateStatus.Rotate0:
                case RotateStatus.Rotate180:
                {
                    switch (lineDirection)
                    {
                        case LineDirection.Right:
                        case LineDirection.Left:
                        {
                            return TileEdgeType.Line;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate90:
                case RotateStatus.Rotate270:
                {
                    switch (lineDirection)
                    {
                        case LineDirection.Up:
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
                case RotateStatus.Rotate180:
                {
                    switch (lineDirection)
                    {
                        case LineDirection.Right:
                        {
                            if (_reactivePropertyElectricStatusLine.Value == ElectricStatus.None)
                            {
                                score = conductScore;
                            }

                            _reactivePropertyElectricStatusLine.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Right,
                            });
                            break;
                        }
                        case LineDirection.Left:
                        {
                            if (_reactivePropertyElectricStatusLine.Value == ElectricStatus.None)
                            {
                                score = conductScore;
                            }

                            _reactivePropertyElectricStatusLine.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Left
                            });
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate90:
                case RotateStatus.Rotate270:
                {
                    switch (lineDirection)
                    {
                        case LineDirection.Up:
                        {
                            if (_reactivePropertyElectricStatusLine.Value == ElectricStatus.None)
                            {
                                score = conductScore;
                            }

                            _reactivePropertyElectricStatusLine.Value = electricStatus;
                            outputs.Add(new ConductOutput
                            {
                                score = score,
                                lineDirection = LineDirection.Up
                            });
                            break;
                        }
                        case LineDirection.Down:
                        {
                            if (_reactivePropertyElectricStatusLine.Value == ElectricStatus.None)
                            {
                                score = conductScore;
                            }

                            _reactivePropertyElectricStatusLine.Value = electricStatus;
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

        public IConductiveTileModel DuplicatedConductiveTileModel => new StraightTileModel(this);

        public bool IsRadiant
        {
            get
            {
                switch (_reactivePropertyElectricStatusLine.Value)
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