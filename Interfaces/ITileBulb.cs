using System;
using Enums;

namespace Interfaces
{
    public interface ITileBulb : ITile
    {
        public IObservable<ElectricStatus> OnChangedElectricStatusLine { get; }
        public IObservable<ElectricStatus> OnChangedElectricStatusBulb { get; }

        public BulbElectricStatus GetBulbElectricStatus(ElectricStatus electricStatus);
    }
}