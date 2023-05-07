using System;
using Parameters.Enums;

namespace Parameters.Interfaces
{
    public interface ITileLine2 : ITile
    {
        public IObservable<ElectricStatus> OnChangedElectricStatusLineA { get; }
        public IObservable<ElectricStatus> OnChangedElectricStatusLineB { get; }
    }
}