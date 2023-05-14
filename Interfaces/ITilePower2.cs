using System;
using Enums;

namespace Interfaces
{
    public interface ITilePower2 : ITile
    {
        public IObservable<ElectricStatus> OnChangedElectricStatusLineA { get; }
        public IObservable<ElectricStatus> OnChangedElectricStatusLineB { get; }
        public IObservable<ElectricStatus> OnChangedElectricStatusPowerSymbol { get; }
    }
}