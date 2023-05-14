using System;
using System.Collections.Generic;
using Enums;
using Interfaces;
using Structs;
using UniRx;

namespace Models.Instances.Tiles
{
    public class TileTerminalNormalRModel : TileBaseModel, ITileTerminal
    {
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLine = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusLine => _reactivePropertyElectricStatusLine;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusTerminalSymbol = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusTerminalSymbol =>
            _reactivePropertyElectricStatusTerminalSymbol;

        public TerminalElectricStatus GetTerminalElectricStatus(ElectricStatus electricStatus)
        {
            return electricStatus switch
            {
                ElectricStatus.Normal => TerminalElectricStatus.Correct,
                ElectricStatus.Plus => TerminalElectricStatus.Different,
                ElectricStatus.Minus => TerminalElectricStatus.Different,
                ElectricStatus.Alternating => TerminalElectricStatus.Different,
                _ => TerminalElectricStatus.None
            };
        }

        public override TileType TileType => TileType.TerminalNormalR;

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
            switch (lineDirection)
            {
                case LineDirection.Right:
                {
                    if (_reactivePropertyElectricStatusTerminalSymbol.Value == ElectricStatus.None)
                    {
                        _reactivePropertyElectricStatusLine.Value = electricStatus;
                        outputs.Add(new ConductOutput
                        {
                            terminalType = TerminalType.Normal
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
                case LineDirection.Right:
                {
                    _reactivePropertyElectricStatusLine.Value = electricStatus;
                    _reactivePropertyElectricStatusTerminalSymbol.Value = electricStatus;
                    break;
                }
            }
        }
    }
}