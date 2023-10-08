using Enums;

namespace Interfaces.TileModels
{
    public interface IBulbOrTerminalTileModel
    {
        public IlluminateBulbOrTerminalResult IlluminateBulbOrTerminal(ElectricStatus electricStatus);
    }
}