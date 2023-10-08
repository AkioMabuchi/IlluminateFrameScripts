using System;
using System.Collections.Generic;
using Enums;
using Interfaces.TileModels;
using Structs;
using UniRx;

namespace Models.Instances.Tiles.Dynamics
{
    public class FourWayDistributorTileModel : TileModelBase, IResettableTileModel, ICheckableValidTileModel,
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
            _reactivePropertyElectricStatusLineD = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusLineD => _reactivePropertyElectricStatusLineD;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusCore = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusCore => _reactivePropertyElectricStatusCore;

        public override TileType TileType => TileType.FourWayDistributor;

        public FourWayDistributorTileModel()
        {
            
        }

        private FourWayDistributorTileModel(FourWayDistributorTileModel fourWayDistributorTileModel)
        {
            _reactivePropertyRotateStatus.Value = fourWayDistributorTileModel._reactivePropertyRotateStatus.Value;
            _reactivePropertyElectricStatusLineA.Value =
                fourWayDistributorTileModel._reactivePropertyElectricStatusLineA.Value;
            _reactivePropertyElectricStatusLineB.Value =
                fourWayDistributorTileModel._reactivePropertyElectricStatusLineB.Value;
            _reactivePropertyElectricStatusLineC.Value =
                fourWayDistributorTileModel._reactivePropertyElectricStatusLineC.Value;
            _reactivePropertyElectricStatusLineD.Value =
                fourWayDistributorTileModel._reactivePropertyElectricStatusLineD.Value;
            _reactivePropertyElectricStatusCore.Value =
                fourWayDistributorTileModel._reactivePropertyElectricStatusCore.Value;
        }
        public void Reset()
        {
            _reactivePropertyElectricStatusLineA.Value = ElectricStatus.None;
            _reactivePropertyElectricStatusLineB.Value = ElectricStatus.None;
            _reactivePropertyElectricStatusLineC.Value = ElectricStatus.None;
            _reactivePropertyElectricStatusLineD.Value = ElectricStatus.None;
            _reactivePropertyElectricStatusCore.Value = ElectricStatus.None;
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
            var outputs = new List<ConductOutput>();

            var score = _reactivePropertyElectricStatusCore.Value == ElectricStatus.None ? 10 : 0;

            _reactivePropertyElectricStatusLineA.Value = electricStatus;
            _reactivePropertyElectricStatusLineB.Value = electricStatus;
            _reactivePropertyElectricStatusLineC.Value = electricStatus;
            _reactivePropertyElectricStatusLineD.Value = electricStatus;
            _reactivePropertyElectricStatusCore.Value = electricStatus;

            switch (lineDirection)
            {
                case LineDirection.Up:
                {
                    outputs.Add(new ConductOutput
                    {
                        score = score,
                        lineDirection = LineDirection.Up
                    });
                    outputs.Add(new ConductOutput
                    {
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
                    outputs.Add(new ConductOutput
                    {
                        score = score,
                        lineDirection = LineDirection.Up
                    });
                    outputs.Add(new ConductOutput
                    {
                        lineDirection = LineDirection.Right
                    });
                    outputs.Add(new ConductOutput
                    {
                        lineDirection = LineDirection.Down
                    });
                    break;
                }
                case LineDirection.Down:
                {
                    outputs.Add(new ConductOutput
                    {
                        score = score,
                        lineDirection = LineDirection.Right
                    });
                    outputs.Add(new ConductOutput
                    {
                        lineDirection = LineDirection.Down
                    });
                    outputs.Add(new ConductOutput
                    {
                        lineDirection = LineDirection.Left
                    });
                    break;
                }
                case LineDirection.Left:
                {
                    outputs.Add(new ConductOutput
                    {
                        score = score,
                        lineDirection = LineDirection.Up
                    });
                    outputs.Add(new ConductOutput
                    {
                        lineDirection = LineDirection.Down
                    });
                    outputs.Add(new ConductOutput
                    {
                        lineDirection = LineDirection.Left
                    });
                    break;
                }
            }

            return outputs;
        }

        public IConductiveTileModel DuplicatedConductiveTileModel => new FourWayDistributorTileModel(this);

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