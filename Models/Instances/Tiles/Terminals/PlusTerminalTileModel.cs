using System;
using System.Collections.Generic;
using Enums;
using Interfaces.TileModels;
using Structs;
using UniRx;

namespace Models.Instances.Tiles.Terminals
{
    public class PlusTerminalTileModel : TileModelBase, IResettableTileModel, ITileModel,
        IConductiveTileModel, IBulbOrTerminalTileModel, IRadiantTileModel
    {
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLine = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusLine => _reactivePropertyElectricStatusLine;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusTerminalSymbol = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusTerminalSymbol =>
            _reactivePropertyElectricStatusTerminalSymbol;

        public override TileType TileType => TileType.PlusTerminal;

        public PlusTerminalTileModel()
        {
            
        }

        private PlusTerminalTileModel(PlusTerminalTileModel plusTerminalTileModel)
        {
            _reactivePropertyElectricStatusLine.Value =
                plusTerminalTileModel._reactivePropertyElectricStatusLine.Value;
            _reactivePropertyElectricStatusTerminalSymbol.Value =
                plusTerminalTileModel._reactivePropertyElectricStatusTerminalSymbol.Value;
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
                    _reactivePropertyElectricStatusLine.Value = electricStatus;
                    if (_reactivePropertyElectricStatusTerminalSymbol.Value == ElectricStatus.None)
                    {
                        outputs.Add(new ConductOutput
                        {
                            terminalType = TerminalType.Plus
                        });
                    }

                    break;
                }
            }

            return outputs;
        }

        public IConductiveTileModel DuplicatedConductiveTileModel => new PlusTerminalTileModel(this);

        public IlluminateBulbOrTerminalResult IlluminateBulbOrTerminal(ElectricStatus electricStatus)
        {
            _reactivePropertyElectricStatusTerminalSymbol.Value = electricStatus;

            if (electricStatus == ElectricStatus.Plus)
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
                    case ElectricStatus.Plus:
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}