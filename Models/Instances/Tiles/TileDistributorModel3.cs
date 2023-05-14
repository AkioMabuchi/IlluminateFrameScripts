using System;
using System.Collections.Generic;
using Enums;
using Interfaces;
using Structs;
using UniRx;

namespace Models.Instances.Tiles
{
    public class TileDistributorModel3 : TileBaseModel, ITileLineDistributor3
    {
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
            _reactivePropertyElectricStatusCore = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusCore => _reactivePropertyElectricStatusCore;
        public override TileType TileType => TileType.Distributor3;

        protected override TileEdgeType GetTileEdgeTypeSub(LineDirection lineDirection)
        {
            switch (lineDirection)
            {
                case LineDirection.Right:
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

            var score = 0;
            switch (lineDirection)
            {
                case LineDirection.Right:
                case LineDirection.Down:
                case LineDirection.Left:
                {
                    if (_reactivePropertyElectricStatusCore.Value == ElectricStatus.None)
                    {
                        score = 10;
                    }
                    _reactivePropertyElectricStatusLineA.Value = electricStatus;
                    _reactivePropertyElectricStatusLineB.Value = electricStatus;
                    _reactivePropertyElectricStatusLineC.Value = electricStatus;

                    _reactivePropertyElectricStatusCore.Value = electricStatus;
                    break;
                }
            }
            
            switch (lineDirection)
            {
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
                        lineDirection = LineDirection.Left
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
                case LineDirection.Right:
                {
                    _reactivePropertyElectricStatusLineC.Value = electricStatus;
                    _reactivePropertyElectricStatusCore.Value = electricStatus;
                    break;
                }
                case LineDirection.Down:
                {
                    _reactivePropertyElectricStatusLineB.Value = electricStatus;
                    _reactivePropertyElectricStatusCore.Value = electricStatus;
                    break;
                }
                case LineDirection.Left:
                {
                    _reactivePropertyElectricStatusLineA.Value = electricStatus;
                    _reactivePropertyElectricStatusCore.Value = electricStatus;
                    break;
                }
            }
            switch (outputLineDirection)
            {
                case LineDirection.Up:
                {
                    _reactivePropertyElectricStatusLineB.Value = electricStatus;
                    break;
                }
                case LineDirection.Right:
                {
                    _reactivePropertyElectricStatusLineA.Value = electricStatus;
                    break;
                }
                case LineDirection.Left:
                {
                    _reactivePropertyElectricStatusLineC.Value = electricStatus;
                    break;
                }
            }
        }
    }
}