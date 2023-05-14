using System;
using System.Collections.Generic;
using Enums;
using Interfaces;
using Structs;
using UniRx;

namespace Models.Instances.Tiles
{
    public class TileBulbModel : TileBaseModel, ITileBulb
    {
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLine = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusLine => _reactivePropertyElectricStatusLine;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusBulb = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusBulb => _reactivePropertyElectricStatusBulb;
        public BulbElectricStatus GetBulbElectricStatus(ElectricStatus electricStatus)
        {
            return electricStatus switch
            {
                ElectricStatus.Normal => BulbElectricStatus.Illuminated,
                ElectricStatus.Plus => BulbElectricStatus.Illuminated,
                ElectricStatus.Minus => BulbElectricStatus.Illuminated,
                ElectricStatus.Alternating => BulbElectricStatus.Illuminated,
                _ => BulbElectricStatus.None
            };
        }

        public override TileType TileType => TileType.Bulb;

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

            return outputs;
        }

        protected override void IlluminateSub(ElectricStatus electricStatus, LineDirection inputLineDirection,
            LineDirection outputLineDirection)
        {
            switch (inputLineDirection)
            {
                case LineDirection.Left:
                {
                    _reactivePropertyElectricStatusLine.Value = electricStatus;
                    _reactivePropertyElectricStatusBulb.Value = electricStatus;
                    break;
                }
            }
        }
    }
}