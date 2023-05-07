using System;
using Parameters.Enums;

namespace Parameters.Interfaces
{
    public interface ITile
    {
        public IObservable<RotateStatus> OnChangedRotateStatus { get; }
    }
}