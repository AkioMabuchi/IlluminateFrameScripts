using System;
using Parameters.Enums;

namespace Parameters.Interfaces
{
    public interface ITileBulb : ITile
    {
        public IObservable<ElectricStatus> OnChangedElectricStatusLine { get; }
        public IObservable<ElectricStatus> OnChangedElectricStatusBulb { get; }
    }
}