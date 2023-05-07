using System;
using System.Collections.Generic;
using Parameters.Enums;
using Parameters.Interfaces;
using Parameters.Structs;
using UniRx;

namespace Models.Instances.Tiles
{
    public class TileTwinCurvesModel : TileBaseModel, ITileLine2
    {
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLineA = new(ElectricStatus.None);
        public IObservable<ElectricStatus> OnChangedElectricStatusLineA => _reactivePropertyElectricStatusLineA;
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLineB = new(ElectricStatus.None);
        
        public IObservable<ElectricStatus> OnChangedElectricStatusLineB => _reactivePropertyElectricStatusLineB;
        public override TileType TileType => TileType.TwinCurves;

        protected override TileEdgeType GetTileEdgeTypeSub(LineDirection lineDirection)
        {
            return TileEdgeType.Line;
        }

        protected override List<ConductOutput> ConductSub(ElectricStatus electricStatus, LineDirection lineDirection)
        {
            var outputs = new List<ConductOutput>();
            switch (lineDirection)
            {
                case LineDirection.Up:
                {
                    _reactivePropertyElectricStatusLineB.Value = electricStatus;
                    outputs.Add(new ConductOutput
                    {
                        LineDirection = LineDirection.Left
                    });
                    break;
                }
                case LineDirection.Right:
                {
                    _reactivePropertyElectricStatusLineB.Value = electricStatus;
                    outputs.Add(new ConductOutput
                    {
                        LineDirection = LineDirection.Down
                    });
                    break;
                }
                case LineDirection.Down:
                {
                    _reactivePropertyElectricStatusLineA.Value = electricStatus;
                    outputs.Add(new ConductOutput
                    {
                        LineDirection = LineDirection.Right
                    });
                    break;
                }
                case LineDirection.Left:
                {
                    _reactivePropertyElectricStatusLineA.Value = electricStatus;
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
                case LineDirection.Up:
                case LineDirection.Right:
                {
                    _reactivePropertyElectricStatusLineB.Value = electricStatus;
                    break;
                }
                case LineDirection.Down:
                case LineDirection.Left:
                {
                    _reactivePropertyElectricStatusLineA.Value = electricStatus;
                    break;
                }
            }
        }
    }
}