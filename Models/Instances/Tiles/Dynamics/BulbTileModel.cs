using System;
using System.Collections.Generic;
using Enums;
using Interfaces.TileModels;
using Structs;
using UniRx;

namespace Models.Instances.Tiles.Dynamics
{
    public class BulbTileModel : TileModelBase, IResettableTileModel, ICheckableValidTileModel,
        ITileModel, IRotatableTileModel, IConductiveTileModel, IBulbOrTerminalTileModel, IRadiantTileModel
    {

        private readonly ReactiveProperty<RotateStatus> _reactivePropertyRotateStatus = new(RotateStatus.Rotate0);
        public IObservable<RotateStatus> OnChangedRotateStatus => _reactivePropertyRotateStatus;
        public RotateStatus RotateStatus => _reactivePropertyRotateStatus.Value;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLine = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusLine => _reactivePropertyElectricStatusLine;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusBulb = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusBulb => _reactivePropertyElectricStatusBulb;

        public override TileType TileType => TileType.Bulb;

        public BulbTileModel()
        {
            
        }

        private BulbTileModel(BulbTileModel bulbTileModel)
        {
            _reactivePropertyRotateStatus.Value = bulbTileModel._reactivePropertyRotateStatus.Value;
            _reactivePropertyElectricStatusLine.Value = bulbTileModel._reactivePropertyElectricStatusLine.Value;
            _reactivePropertyElectricStatusBulb.Value = bulbTileModel._reactivePropertyElectricStatusBulb.Value;
        }

        public void Reset()
        {
            _reactivePropertyElectricStatusLine.Value = ElectricStatus.None;
            _reactivePropertyElectricStatusBulb.Value = ElectricStatus.None;
        }

        public TileEdgeType GetTileEdgeType(LineDirection lineDirection)
        {
            switch (_reactivePropertyRotateStatus.Value)
            {
                case RotateStatus.Rotate0:
                {
                    switch (lineDirection)
                    {
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
            var outputs = new List<ConductOutput>();
            
            switch (_reactivePropertyRotateStatus.Value)
            {
                case RotateStatus.Rotate0:
                {
                    switch (lineDirection)
                    {
                        case LineDirection.Left:
                        {
                            _reactivePropertyElectricStatusLine.Value = electricStatus;
                            if (_reactivePropertyElectricStatusBulb.Value == ElectricStatus.None)
                            {
                                outputs.Add(new ConductOutput
                                {
                                    terminalType = TerminalType.Bulb
                                });
                            }

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
                            _reactivePropertyElectricStatusLine.Value = electricStatus;
                            if (_reactivePropertyElectricStatusBulb.Value == ElectricStatus.None)
                            {
                                outputs.Add(new ConductOutput
                                {
                                    terminalType = TerminalType.Bulb
                                });
                            }

                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate180:
                {
                    switch (lineDirection)
                    {
                        case LineDirection.Right:
                        {
                            _reactivePropertyElectricStatusLine.Value = electricStatus;
                            if (_reactivePropertyElectricStatusBulb.Value == ElectricStatus.None)
                            {
                                outputs.Add(new ConductOutput
                                {
                                    terminalType = TerminalType.Bulb
                                });
                            }

                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate270:
                {
                    switch (lineDirection)
                    {
                        case LineDirection.Down:
                        {
                            _reactivePropertyElectricStatusLine.Value = electricStatus;
                            if (_reactivePropertyElectricStatusBulb.Value == ElectricStatus.None)
                            {
                                outputs.Add(new ConductOutput
                                {
                                    terminalType = TerminalType.Bulb
                                });
                            }

                            break;
                        }
                    }

                    break;
                }
            }

            return outputs;
        }

        public IConductiveTileModel DuplicatedConductiveTileModel => new BulbTileModel(this);

        public IlluminateBulbOrTerminalResult IlluminateBulbOrTerminal(ElectricStatus electricStatus)
        {
            _reactivePropertyElectricStatusBulb.Value = electricStatus;
            return IlluminateBulbOrTerminalResult.Bulb;
        }

        public bool IsRadiant
        {
            get
            {
                switch (_reactivePropertyElectricStatusBulb.Value)
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