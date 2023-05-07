using System;
using Parameters.Enums;

namespace Parameters.Interfaces
{
    public interface ITileTerminal : ITile
    {
        public IObservable<ElectricStatus> OnChangedElectricStatusLine { get; }
        public IObservable<ElectricStatus> OnChangedElectricStatusTerminalSymbol { get; }
    }
}