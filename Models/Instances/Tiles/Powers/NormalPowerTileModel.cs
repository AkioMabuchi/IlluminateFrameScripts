using System;
using System.Collections.Generic;
using Enums;
using Interfaces.TileModels;
using Structs;
using UniRx;

namespace Models.Instances.Tiles.Powers
{
    public class NormalPowerTileModel : TileModelBase, IResettableTileModel, ITileModel,
        IConductiveTileModel, IPowerTileModel
    {
        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLineA = new(ElectricStatus.Normal);

        public IObservable<ElectricStatus> OnChangedElectricStatusLineA => _reactivePropertyElectricStatusLineA;

        private readonly ReactiveProperty<ElectricStatus>
            _reactivePropertyElectricStatusLineB = new(ElectricStatus.Normal);

        public IObservable<ElectricStatus> OnChangedElectricStatusLineB => _reactivePropertyElectricStatusLineB;

        public override TileType TileType => TileType.NormalPower;

        public NormalPowerTileModel()
        {
            
        }

        public NormalPowerTileModel(NormalPowerTileModel normalPowerTileModel)
        {
            _reactivePropertyElectricStatusLineA.Value =
                normalPowerTileModel._reactivePropertyElectricStatusLineA.Value;
            _reactivePropertyElectricStatusLineB.Value =
                normalPowerTileModel._reactivePropertyElectricStatusLineB.Value;
        }

        public void Reset()
        {
            _reactivePropertyElectricStatusLineA.Value = ElectricStatus.Normal;
            _reactivePropertyElectricStatusLineB.Value = ElectricStatus.Normal;
        }

        public TileEdgeType GetTileEdgeType(LineDirection lineDirection)
        {
            switch (lineDirection)
            {
                case LineDirection.Right:
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
                case ElectricStatus.Normal:
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

        public IConductiveTileModel DuplicatedConductiveTileModel => new NormalPowerTileModel(this);

        public IEnumerable<PowerOutput> PowerOutputs => new List<PowerOutput>
        {
            new()
            {
                electricStatus = ElectricStatus.Normal,
                lineDirection = LineDirection.Right
            },
            new()
            {
                electricStatus = ElectricStatus.Normal,
                lineDirection = LineDirection.Left
            }
        };

        public IPowerTileModel DuplicatedPowerTileModel => new NormalPowerTileModel(this);
    }
}