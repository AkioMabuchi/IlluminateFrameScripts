using System;
using Parameters.Enums;

namespace Parameters.Interfaces
{
    public interface ITilePower1 : ITile
    {
        public IObservable<ElectricStatus> OnChangedElectricStatusLine { get; }
        public IObservable<ElectricStatus> OnChangedElectricStatusPowerSymbol { get; }
    }
}