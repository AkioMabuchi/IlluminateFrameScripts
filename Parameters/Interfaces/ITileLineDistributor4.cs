using System;
using Parameters.Enums;

namespace Parameters.Interfaces
{
    public interface ITileLineDistributor4 : ITile
    {
        public IObservable<ElectricStatus> OnChangedElectricStatusLineA { get; }
        public IObservable<ElectricStatus> OnChangedElectricStatusLineB { get; }
        public IObservable<ElectricStatus> OnChangedElectricStatusLineC { get; }
        public IObservable<ElectricStatus> OnChangedElectricStatusLineD { get; }
        public IObservable<ElectricStatus> OnChangedElectricStatusCore { get; }
    }
}