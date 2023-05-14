using System;
using Enums;

namespace Interfaces
{
    public interface ITileLineDistributor3 : ITile
    {
        public IObservable<ElectricStatus> OnChangedElectricStatusLineA { get; }
        public IObservable<ElectricStatus> OnChangedElectricStatusLineB { get; }
        public IObservable<ElectricStatus> OnChangedElectricStatusLineC { get; }
        public IObservable<ElectricStatus> OnChangedElectricStatusCore { get; }
    }
}