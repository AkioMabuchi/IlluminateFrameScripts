using System;
using Enums;

namespace Interfaces
{
    public interface ITilePower1 : ITile
    {
        public IObservable<ElectricStatus> OnChangedElectricStatusLine { get; }
        public IObservable<ElectricStatus> OnChangedElectricStatusPowerSymbol { get; }
    }
}