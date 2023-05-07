using System.Collections.Generic;
using Parameters.Structs;

namespace Parameters.Interfaces
{
    public interface ITilePower
    {
        public IEnumerable<PowerOutput> PowerOutputs { get; }
    }
}