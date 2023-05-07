using System;
using System.Collections.Generic;
using Parameters.Enums;
using Parameters.Interfaces;
using Parameters.Structs;
using UniRx;

namespace Models.Instances.Tiles
{
    public class TilePowerNormalModel : TileBaseModel, ITilePower2
    {
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLineA = new(ElectricStatus.Normal);

        public IObservable<ElectricStatus> OnChangedElectricStatusLineA => _reactivePropertyElectricStatusLineA;
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLineB = new(ElectricStatus.Normal);

        public IObservable<ElectricStatus> OnChangedElectricStatusLineB => _reactivePropertyElectricStatusLineB;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusPowerSymbol = new(ElectricStatus.Normal);

        public IObservable<ElectricStatus> OnChangedElectricStatusPowerSymbol =>
            _reactivePropertyElectricStatusPowerSymbol;

        public override TileType TileType => TileType.PowerNormal;

        protected override TileEdgeType GetTileEdgeTypeSub(LineDirection lineDirection)
        {
            switch (lineDirection)
            {
                case LineDirection.Right:
                case LineDirection.Left:
                {
                    return TileEdgeType.Line;
                }
            }

            return TileEdgeType.Solid;
        }

        protected override List<ConductOutput> ConductSub(ElectricStatus electricStatus, LineDirection lineDirection)
        {
            var outputs = new List<ConductOutput>();
            
            if (electricStatus is ElectricStatus.None or ElectricStatus.Normal or ElectricStatus.NormalIlluminate)
            {
                return outputs;
            }

            switch (lineDirection)
            {
                case LineDirection.Right:
                {
                    _reactivePropertyElectricStatusLineB.Value = ElectricStatus.Shorted;
                    _reactivePropertyElectricStatusPowerSymbol.Value = ElectricStatus.Shorted;
                    outputs.Add(new ConductOutput
                    {
                        IsShorted = true
                    });
                    break;
                }
                case LineDirection.Left:
                {
                    _reactivePropertyElectricStatusLineA.Value = ElectricStatus.Shorted;
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

        protected override void IlluminateSub(ElectricStatus electricStatus, LineDirection inputLineDirection,
            LineDirection outputLineDirection)
        {
            switch (outputLineDirection)
            {
                case LineDirection.Right:
                {
                    _reactivePropertyElectricStatusLineB.Value = electricStatus;
                    _reactivePropertyElectricStatusPowerSymbol.Value = electricStatus;
                    break;
                }
                case LineDirection.Left:
                {
                    _reactivePropertyElectricStatusLineA.Value = electricStatus;
                    _reactivePropertyElectricStatusPowerSymbol.Value = electricStatus;
                    break;
                }
            }
        }
    }
}