using System;
using System.Collections.Generic;
using Enums;
using Interfaces;
using Structs;
using UniRx;

namespace Models.Instances.Tiles
{
    public class TilePowerMinusModel : TileBaseModel, ITilePower, ITilePower1
    {
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLine = new(ElectricStatus.Minus);

        public IObservable<ElectricStatus> OnChangedElectricStatusLine => _reactivePropertyElectricStatusLine;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusPowerSymbol = new(ElectricStatus.Minus);

        public IObservable<ElectricStatus> OnChangedElectricStatusPowerSymbol =>
            _reactivePropertyElectricStatusPowerSymbol;

        public override TileType TileType => TileType.PowerMinus;

        protected override TileEdgeType GetTileEdgeTypeSub(LineDirection lineDirection)
        {
            switch (lineDirection)
            {
                case LineDirection.Right:
                {
                    return TileEdgeType.Line;
                }
            }

            return TileEdgeType.Solid;
        }

        protected override List<ConductOutput> ConductSub(ElectricStatus electricStatus, LineDirection lineDirection)
        {
            var outputs = new List<ConductOutput>();
            
            if (electricStatus is ElectricStatus.None or ElectricStatus.Minus or ElectricStatus.MinusIlluminate)
            {
                return outputs;
            }

            switch (lineDirection)
            {
                case LineDirection.Right:
                {
                    _reactivePropertyElectricStatusLine.Value = ElectricStatus.Shorted;
                    _reactivePropertyElectricStatusPowerSymbol.Value = ElectricStatus.Shorted;
                    outputs.Add(new ConductOutput
                    {
                        isShorted = true
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
                case LineDirection.Left:
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
                electricStatus = ElectricStatus.Minus,
                lineDirection = LineDirection.Left
            },
        };
    }
}