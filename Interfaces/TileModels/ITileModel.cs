using Enums;

namespace Interfaces.TileModels
{
    public interface ITileModel
    {
        public TileEdgeType GetTileEdgeType(LineDirection lineDirection);
    }
}