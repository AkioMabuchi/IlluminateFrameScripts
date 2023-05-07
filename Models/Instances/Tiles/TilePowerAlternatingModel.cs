using System;
using System.Collections.Generic;
using Parameters.Enums;
using Parameters.Interfaces;
using Parameters.Structs;
using UniRx;

namespace Models.Instances.Tiles
{
    public class TilePowerAlternatingModel : TileBaseModel, ITilePower, ITilePower2
    {
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLineA = new(ElectricStatus.Alternating);

        public IObservable<ElectricStatus> OnChangedElectricStatusLineA => _reactivePropertyElectricStatusLineA;
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLineB = new(ElectricStatus.Alternating);

        public IObservable<ElectricStatus> OnChangedElectricStatusLineB => _reactivePropertyElectricStatusLineB;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusPowerSymbol = new(ElectricStatus.Alternating);

        public IObservable<ElectricStatus> OnChangedElectricStatusPowerSymbol =>
            _reactivePropertyElectricStatusPowerSymbol;

        public override TileType TileType => TileType.PowerAlternating;

        protected override TileEdgeType GetTileEdgeTypeSub(LineDirection lineDirection)
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

        protected override List<ConductOutput> ConductSub(ElectricStatus electricStatus, LineDirection lineDirection)
        {
            var outputs = new List<ConductOutput>();

            if (electricStatus is ElectricStatus.None or ElectricStatus.Alternating
                or ElectricStatus.AlternatingIlluminate)
            {
                return outputs;
            }

            switch (lineDirection)
            {
                case LineDirection.Up:
                {
                    _reactivePropertyElectricStatusLineA.Value = ElectricStatus.Shorted;
                    _reactivePropertyElectricStatusPowerSymbol.Value = ElectricStatus.Shorted;
                    outputs.Add(new ConductOutput
                    {
                        IsShorted = true
                    });
                    break;
                }
                case LineDirection.Down:
                {
                    _reactivePropertyElectricStatusLineB.Value = ElectricStatus.Shorted;
                    _reactivePropertyElectricStatusPowerSymbol.Value = ElectricStatus.Shorted;
                    outputs.Add(new ConductOutput
                    {
                        IsShorted = true
                    });
                    break;
                }
            }

            return outputs;
        }

        protected override void IlluminateSub(ElectricStatus electricStatus, LineDirection inputLineDirection, LineDirection outputLineDirection)
        {
            switch (outputLineDirection)
            {
                case LineDirection.Up:
                {
                    _reactivePropertyElectricStatusLineB.Value = electricStatus;
                    _reactivePropertyElectricStatusPowerSymbol.Value = electricStatus;
                    break;
                }
                case LineDirection.Down:
                {
                    _reactivePropertyElectricStatusLineA.Value = electricStatus;
                    _reactivePropertyElectricStatusPowerSymbol.Value = electricStatus;
                    break;
                }
            }
        }

        public IEnumerable<PowerOutput> PowerOutputs => new List<PowerOutput>
        {
            new()
            {
                ElectricStatus = ElectricStatus.Alternating,
                LineDirection = LineDirection.Up
            },
            new()
            {
                ElectricStatus = ElectricStatus.Alternating,
                LineDirection = LineDirection.Down
            }
        };
    }
}