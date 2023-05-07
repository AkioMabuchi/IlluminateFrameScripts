using System;
using System.Collections.Generic;
using Parameters.Enums;
using Parameters.Interfaces;
using Parameters.Structs;
using UniRx;

namespace Models.Instances.Tiles
{
    public class TilePowerPlusModel : TileBaseModel, ITilePower, ITilePower1
    {
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLine = new(ElectricStatus.Plus);

        public IObservable<ElectricStatus> OnChangedElectricStatusLine => _reactivePropertyElectricStatusLine;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusPowerSymbol = new(ElectricStatus.Plus);

        public IObservable<ElectricStatus> OnChangedElectricStatusPowerSymbol =>
            _reactivePropertyElectricStatusPowerSymbol;

        public override TileType TileType => TileType.PowerPlus;

        protected override TileEdgeType GetTileEdgeTypeSub(LineDirection lineDirection)
        {
            switch (lineDirection)
            {
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
            
            if (electricStatus is ElectricStatus.None or ElectricStatus.Plus or ElectricStatus.PlusIlluminate)
            {
                return outputs;
            }

            switch (lineDirection)
            {
                case LineDirection.Left:
                {
                    _reactivePropertyElectricStatusLine.Value = ElectricStatus.Shorted;
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
                case LineDirection.Right:
                {
                    _reactivePropertyElectricStatusLine.Value = electricStatus;
                    _reactivePropertyElectricStatusPowerSymbol.Value = electricStatus;
                    break;
                }
            }
        }

        public IEnumerable<PowerOutput> PowerOutputs => new List<PowerOutput>
        {
            new()
            {
                ElectricStatus = ElectricStatus.Plus,
                LineDirection = LineDirection.Right
            }
        };
    }
}