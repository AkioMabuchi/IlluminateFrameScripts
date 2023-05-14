using System;
using Enums;

namespace Interfaces
{
    public interface ITileTerminal : ITile
    {
        public IObservable<ElectricStatus> OnChangedElectricStatusLine { get; }
        public IObservable<ElectricStatus> OnChangedElectricStatusTerminalSymbol { get; }

        public TerminalElectricStatus GetTerminalElectricStatus(ElectricStatus electricStatus);
    }
}