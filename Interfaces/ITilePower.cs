using System.Collections.Generic;
using Structs;

namespace Interfaces
{
    public interface ITilePower
    {
        public IEnumerable<PowerOutput> PowerOutputs { get; }
    }
}