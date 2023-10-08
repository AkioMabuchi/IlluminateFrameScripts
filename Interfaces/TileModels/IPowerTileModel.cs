using System.Collections.Generic;
using Structs;

namespace Interfaces.TileModels
{
    public interface IPowerTileModel
    {
        public IEnumerable<PowerOutput> PowerOutputs { get; }

        public IPowerTileModel DuplicatedPowerTileModel
        {
            get;
        }
    }
}