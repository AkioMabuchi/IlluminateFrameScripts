using System;
using System.Collections.Generic;
using Parameters.Enums;
using Parameters.Interfaces;
using Parameters.Structs;
using UniRx;

namespace Models.Instances.Tiles
{
    public class TileTerminalAlternatingLModel : TileBaseModel, ITileTerminal
    {
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLine = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusLine => _reactivePropertyElectricStatusLine;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusTerminalSymbol = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusTerminalSymbol =>
            _reactivePropertyElectricStatusTerminalSymbol;

        public override TileType TileType => TileType.TerminalAlternatingL;

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
                    if (_reactivePropertyElectricStatusTerminalSymbol.Value == ElectricStatus.None)
                    {
                        _reactivePropertyElectricStatusLine.Value = electricStatus;
                        outputs.Add(new ConductOutput
                        {
                            TerminalType = TerminalType.Alternating
                        });
                    }

                    break;
                }
            }

            return outputs;
        }

        protected override void IlluminateSub(ElectricStatus electricStatus, LineDirection inputLineDirection, LineDirection outputLineDirection)
        {
            switch (inputLineDirection)
            {
                case LineDirection.Left:
                {
                    _reactivePropertyElectricStatusLine.Value = electricStatus;
                    _reactivePropertyElectricStatusTerminalSymbol.Value = electricStatus;
                    break;
                }
            }
        }
    }
}