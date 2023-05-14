using System;
using System.Collections.Generic;
using Enums;
using Interfaces;
using Structs;
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
            var score = 0;
            switch (lineDirection)
            {
                case LineDirection.Up:
                {
                    if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                    {
                        if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                        {
                            score = 8;
                        }
                        else
                        {
                            score = 4;
                        }
                    }
                    _reactivePropertyElectricStatusLineB.Value = electricStatus;
                    outputs.Add(new ConductOutput
                    {
                        score = score,
                        lineDirection = LineDirection.Up
                    });
                    break;
                }
                case LineDirection.Right:
                {
                    if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                    {
                        if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                        {
                            score = 8;
                        }
                        else
                        {
                            score = 4;
                        }
                    }
                    _reactivePropertyElectricStatusLineA.Value = electricStatus;
                    outputs.Add(new ConductOutput
                    {
                        score = score,
                        lineDirection = LineDirection.Right
                    });
                    break;
                }
                case LineDirection.Down:
                {
                    if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                    {
                        if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                        {
                            score = 8;
                        }
                        else
                        {
                            score = 4;
                        }
                    }
                    _reactivePropertyElectricStatusLineB.Value = electricStatus;
                    outputs.Add(new ConductOutput
                    {
                        score = score,
                        lineDirection = LineDirection.Down
                    });
                    break;
                }
                case LineDirection.Left:
                {
                    if (_reactivePropertyElectricStatusLineA.Value == ElectricStatus.None)
                    {
                        if (_reactivePropertyElectricStatusLineB.Value == ElectricStatus.None)
                        {
                            score = 8;
                        }
                        else
                        {
                            score = 4;
                        }
                    }
                    _reactivePropertyElectricStatusLineA.Value = electricStatus;
                    outputs.Add(new ConductOutput
                    {
                        score = score,
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