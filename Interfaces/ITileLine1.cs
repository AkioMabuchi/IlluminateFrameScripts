using System;
using Enums;

namespace Interfaces
{
    public interface ITileLine1 : ITile
    {
        public IObservable<ElectricStatus> OnChangedElectricStatusLine { get; }
    }
}