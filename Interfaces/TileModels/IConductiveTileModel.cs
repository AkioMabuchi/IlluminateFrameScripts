using System.Collections.Generic;
using Enums;
using Structs;

namespace Interfaces.TileModels
{
    public interface IConductiveTileModel
    {
        public IEnumerable<ConductOutput> Conduct(ElectricStatus electricStatus, LineDirection lineDirection);

        public IConductiveTileModel DuplicatedConductiveTileModel
        {
            get;
        }
    }
}