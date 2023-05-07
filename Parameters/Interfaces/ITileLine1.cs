using System;
using Parameters.Enums;

namespace Parameters.Interfaces
{
    public interface ITileLine1 : ITile
    {
        public IObservable<ElectricStatus> OnChangedElectricStatusLine { get; }
    }
}