using System;
using Enums;

namespace Interfaces
{
    public interface ITile
    {
        public IObservable<RotateStatus> OnChangedRotateStatus { get; }
    }
}