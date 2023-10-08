using System;
using System.Collections.Generic;
using Enums;
using Interfaces.TileModels;
using Structs;
using UniRx;

namespace Models.Instances.Tiles.Powers
{
    public class MinusPowerTileModel : TileModelBase, IResettableTileModel, ITileModel,
        IConductiveTileModel, IPowerTileModel
    {
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLine = new(ElectricStatus.Minus);

        public IObservable<ElectricStatus> OnChangedElectricStatusLine => _reactivePropertyElectricStatusLine;

        public override TileType TileType => TileType.MinusPower;

        public MinusPowerTileModel()
        {
            
        }

        private MinusPowerTileModel(MinusPowerTileModel minusPowerTileModel)
        {
            _reactivePropertyElectricStatusLine.Value = minusPowerTileModel._reactivePropertyElectricStatusLine.Value;
        }

        public void Reset()
        {
            _reactivePropertyElectricStatusLine.Value = ElectricStatus.Minus;
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

            switch (electricStatus)
            {
                case ElectricStatus.None:
                case ElectricStatus.Minus:
                {
                    return outputs;
                }
            }

            switch (lineDirection)
            {
                case LineDirection.Right:
                {
                    outputs.Add(new ConductOutput
                    {
                        isShorted = true
                    });
                    break;
                }
            }

            return outputs;
        }

        public IConductiveTileModel DuplicatedConductiveTileModel => new MinusPowerTileModel(this);

        public IEnumerable<PowerOutput> PowerOutputs => new List<PowerOutput>
        {
            new()
            {
                electricStatus = ElectricStatus.Minus,
                lineDirection = LineDirection.Left
            },
        };

        public IPowerTileModel DuplicatedPowerTileModel => new MinusPowerTileModel(this);
    }
}