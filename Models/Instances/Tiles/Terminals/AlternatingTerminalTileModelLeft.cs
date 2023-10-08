using System;
using System.Collections.Generic;
using Enums;
using Interfaces.TileModels;
using Structs;
using UniRx;

namespace Models.Instances.Tiles.Terminals
{
    public class AlternatingTerminalTileModelLeft : TileModelBase, IResettableTileModel, ITileModel,
        IConductiveTileModel, IBulbOrTerminalTileModel, IRadiantTileModel
    {
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLine = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusLine => _reactivePropertyElectricStatusLine;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusTerminalSymbol = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusTerminalSymbol =>
            _reactivePropertyElectricStatusTerminalSymbol;

        public override TileType TileType => TileType.AlternatingTerminalLeft;

        public AlternatingTerminalTileModelLeft()
        {
            
        }

        private AlternatingTerminalTileModelLeft(AlternatingTerminalTileModelLeft alternatingTerminalTileModelLeft)
        {
            _reactivePropertyElectricStatusLine.Value =
                alternatingTerminalTileModelLeft._reactivePropertyElectricStatusLine.Value;
            _reactivePropertyElectricStatusTerminalSymbol.Value =
                alternatingTerminalTileModelLeft._reactivePropertyElectricStatusTerminalSymbol.Value;
        }
        public void Reset()
        {
            _reactivePropertyElectricStatusLine.Value = ElectricStatus.None;
            _reactivePropertyElectricStatusTerminalSymbol.Value = ElectricStatus.None;
        }

        public TileEdgeType GetTileEdgeType(LineDirection lineDirection)
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

        public IEnumerable<ConductOutput> Conduct(ElectricStatus electricStatus, LineDirection lineDirection)
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
                            terminalType = TerminalType.Alternating
                        });
                    }

                    break;
                }
            }

            return outputs;
        }

        public IConductiveTileModel DuplicatedConductiveTileModel => new AlternatingTerminalTileModelLeft(this);

        public IlluminateBulbOrTerminalResult IlluminateBulbOrTerminal(ElectricStatus electricStatus)
        {
            _reactivePropertyElectricStatusTerminalSymbol.Value = electricStatus;

            if (electricStatus == ElectricStatus.Alternating)
            {
                return IlluminateBulbOrTerminalResult.TerminalCorrect;
            }

            return IlluminateBulbOrTerminalResult.TerminalIncorrect;
        }

        public bool IsRadiant
        {
            get
            {
                switch (_reactivePropertyElectricStatusTerminalSymbol.Value)
                {
                    case ElectricStatus.Alternating:
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}