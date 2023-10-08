using System;
using System.Collections.Generic;
using Enums;
using Interfaces.TileModels;
using Structs;
using UniRx;

namespace Models.Instances.Tiles.Powers
{
    public class PlusPowerTileModel : TileModelBase, IResettableTileModel, ITileModel,
        IConductiveTileModel, IPowerTileModel
    {
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLine = new(ElectricStatus.Plus);

        public IObservable<ElectricStatus> OnChangedElectricStatusLine => _reactivePropertyElectricStatusLine;

        public override TileType TileType => TileType.PlusPower;

        public PlusPowerTileModel()
        {
            
        }

        private PlusPowerTileModel(PlusPowerTileModel plusPowerTileModel)
        {
            _reactivePropertyElectricStatusLine.Value = plusPowerTileModel._reactivePropertyElectricStatusLine.Value;
        }
        public void Reset()
        {
            _reactivePropertyElectricStatusLine.Value = ElectricStatus.Plus;
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

            switch (electricStatus)
            {
                case ElectricStatus.None:
                case ElectricStatus.Plus:
                {
                    return outputs;
                }
            }

            switch (lineDirection)
            {
                case LineDirection.Left:
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

        public IConductiveTileModel DuplicatedConductiveTileModel => new PlusPowerTileModel(this);

        public IEnumerable<PowerOutput> PowerOutputs => new List<PowerOutput>
        {
            new()
            {
                electricStatus = ElectricStatus.Plus,
                lineDirection = LineDirection.Right
            }
        };

        public IPowerTileModel DuplicatedPowerTileModel => new PlusPowerTileModel(this);
    }
}