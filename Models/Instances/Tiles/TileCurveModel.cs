using System;
using System.Collections.Generic;
using Parameters.Enums;
using Parameters.Interfaces;
using Parameters.Structs;
using UniRx;

namespace Models.Instances.Tiles
{
    public class TileCurveModel : TileBaseModel, ITileLine1
    {
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLine = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusLine => _reactivePropertyElectricStatusLine;
        public override TileType TileType => TileType.Curve;

        protected override TileEdgeType GetTileEdgeTypeSub(LineDirection lineDirection)
        {
            switch (lineDirection)
            {
                case LineDirection.Down:
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
                case LineDirection.Down:
                {
                    _reactivePropertyElectricStatusLine.Value = electricStatus;
                    outputs.Add(new ConductOutput
                    {
                        LineDirection = LineDirection.Right
                    });
                    break;
                }
                case LineDirection.Left:
                {
                    _reactivePropertyElectricStatusLine.Value = electricStatus;
                    outputs.Add(new ConductOutput
                    {
                        LineDirection = LineDirection.Up
                    });
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
                case LineDirection.Down:
                case LineDirection.Left:
                {
                    _reactivePropertyElectricStatusLine.Value = electricStatus;

                    break;
                }
            }
        }
    }
}