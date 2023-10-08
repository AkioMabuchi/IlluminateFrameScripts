using System;
using System.Collections.Generic;
using Enums;
using Interfaces.TileModels;
using Structs;
using UniRx;

namespace Models.Instances.Tiles.Powers
{
    public class AlternatingPowerTileModel : TileModelBase, IResettableTileModel, ITileModel,
        IConductiveTileModel, IPowerTileModel
    {
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLineA = new(ElectricStatus.Alternating);

        public IObservable<ElectricStatus> OnChangedElectricStatusLineA => _reactivePropertyElectricStatusLineA;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLineB = new(ElectricStatus.Alternating);

        public IObservable<ElectricStatus> OnChangedElectricStatusLineB => _reactivePropertyElectricStatusLineB;

        public override TileType TileType => TileType.AlternatingPower;

        public AlternatingPowerTileModel()
        {
            
        }

        private AlternatingPowerTileModel(AlternatingPowerTileModel alternatingPowerTileModel)
        {
            _reactivePropertyElectricStatusLineA.Value =
                alternatingPowerTileModel._reactivePropertyElectricStatusLineA.Value;
            _reactivePropertyElectricStatusLineB.Value =
                alternatingPowerTileModel._reactivePropertyElectricStatusLineB.Value;
        }

        public void Reset()
        {
            _reactivePropertyElectricStatusLineA.Value = ElectricStatus.Alternating;
            _reactivePropertyElectricStatusLineB.Value = ElectricStatus.Alternating;
        }

        public TileEdgeType GetTileEdgeType(LineDirection lineDirection)
        {
            switch (lineDirection)
            {
                case LineDirection.Up:
                case LineDirection.Down:
                {
                    return TileEdgeType.Line;
                }
            }

            return TileEdgeType.Solid;
        }

        public IEnumerable<ConductOutput> Conduct(ElectricStatus electricStatus, LineDirection lineDirection)
        {
            var outputs = new List<ConductOutput>();

            switch (electricStatus)
            {
                case ElectricStatus.None:
                case ElectricStatus.Alternating:
                {
                    break;
                }
                default:
                {
                    switch (lineDirection)
                    {
                        case LineDirection.Up:
                        {
                            outputs.Add(new ConductOutput
                            {
                                isShorted = true
                            });
                            break;
                        }
                        case LineDirection.Down:
                        {
                            outputs.Add(new ConductOutput
                            {
                                isShorted = true
                            });
                            break;
                        }
                    }

                    return outputs;
                }
            }

            return outputs;
        }

        public IConductiveTileModel DuplicatedConductiveTileModel => new AlternatingPowerTileModel(this);

        public IEnumerable<PowerOutput> PowerOutputs => new List<PowerOutput>
        {
            new()
            {
                electricStatus = ElectricStatus.Alternating,
                lineDirection = LineDirection.Up
            },
            new()
            {
                electricStatus = ElectricStatus.Alternating,
                lineDirection = LineDirection.Down
            }
        };

        public IPowerTileModel DuplicatedPowerTileModel => new AlternatingPowerTileModel(this);
    }
}