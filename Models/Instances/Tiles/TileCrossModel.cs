using System;
using System.Collections.Generic;
using Parameters.Enums;
using Parameters.Interfaces;
using Parameters.Structs;
using UniRx;

namespace Models.Instances.Tiles
{
    public class TileCrossModel : TileBaseModel, ITileLine2
    {
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLineA = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusLineA => _reactivePropertyElectricStatusLineA;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLineB = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusLineB => _reactivePropertyElectricStatusLineB;
        public override TileType TileType => TileType.Cross;

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
                        LineDirection = LineDirection.Up
                    });
                    break;
                }
                case LineDirection.Right:
                {
                    _reactivePropertyElectricStatusLineA.Value = electricStatus;
                    outputs.Add(new ConductOutput
                    {
                        LineDirection = LineDirection.Right
                    });
                    break;
                }
                case LineDirection.Down:
                {
                    _reactivePropertyElectricStatusLineB.Value = electricStatus;
                    outputs.Add(new ConductOutput
                    {
                        LineDirection = LineDirection.Down
                    });
                    break;
                }
                case LineDirection.Left:
                {
                    _reactivePropertyElectricStatusLineA.Value = electricStatus;
                    outputs.Add(new ConductOutput
                    {
                        LineDirection = LineDirection.Left
                    });
                    break;
                }
            }
            return outputs;
        }

        protected override void IlluminateSub(ElectricStatus electricStatus, LineDirection inputLineDirection, LineDirection outputLineDirection)
        {
            switch (inputLineDirection)
            {
                case LineDirection.Up:
                case LineDirection.Down:
                {
                    _reactivePropertyElectricStatusLineB.Value = electricStatus;

                    break;
                }
                case LineDirection.Right:
                case LineDirection.Left:
                {
                    _reactivePropertyElectricStatusLineA.Value = electricStatus;

                    break;
                }
            }
        }
    }
}