using Enums;

namespace Interfaces.TileModels
{
    public interface ICheckableValidTileModel
    {
        public TileType TileType
        {
            get;
        }

        public RotateStatus RotateStatus
        {
            get;
        }
    }
}