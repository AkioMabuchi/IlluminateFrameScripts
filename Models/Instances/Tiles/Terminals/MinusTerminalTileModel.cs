using System;
using System.Collections.Generic;
using Enums;
using Interfaces.TileModels;
using Structs;
using UniRx;

namespace Models.Instances.Tiles.Terminals
{
    public class MinusTerminalTileModel : TileModelBase, IResettableTileModel, ITileModel,
        IConductiveTileModel, IBulbOrTerminalTileModel, IRadiantTileModel
    {
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLine = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusLine => _reactivePropertyElectricStatusLine;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusTerminalSymbol = new(ElectricStatus.None);

        public IObservable<ElectricStatus> OnChangedElectricStatusTerminalSymbol =>
            _reactivePropertyElectricStatusTerminalSymbol;

        public override TileType TileType => TileType.MinusTerminal;

        public MinusTerminalTileModel()
        {
            
        }

        private MinusTerminalTileModel(MinusTerminalTileModel minusTerminalTileModel)
        {
            _reactivePropertyElectricStatusLine.Value =
                minusTerminalTileModel._reactivePropertyElectricStatusLine.Value;
            _reactivePropertyElectricStatusTerminalSymbol.Value =
                minusTerminalTileModel._reactivePropertyElectricStatusTerminalSymbol.Value;
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
                case LineDirection.Right:
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
                case LineDirection.Right:
                {
                    if (_reactivePropertyElectricStatusTerminalSymbol.Value == ElectricStatus.None)
                    {
                        _reactivePropertyElectricStatusLine.Value = electricStatus;
                        outputs.Add(new ConductOutput
                        {
                            terminalType = TerminalType.Minus
                        });
                    }

                    break;
                }
            }

            return outputs;
        }

        public IConductiveTileModel DuplicatedConductiveTileModel => new MinusTerminalTileModel(this);

        public IlluminateBulbOrTerminalResult IlluminateBulbOrTerminal(ElectricStatus electricStatus)
        {
            _reactivePropertyElectricStatusTerminalSymbol.Value = electricStatus;

            if (electricStatus == ElectricStatus.Minus)
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
                    case ElectricStatus.Minus:
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}