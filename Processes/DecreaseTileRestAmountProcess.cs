using Models;
using Views;

namespace Processes
{
    public class DecreaseTileRestAmountProcess
    {
        private readonly TileRestAmountModel _tileRestAmountModel;
        private readonly Desk _desk;

        public DecreaseTileRestAmountProcess(TileRestAmountModel tileRestAmountModel, Desk desk)
        {
            _tileRestAmountModel = tileRestAmountModel;
            _desk = desk;
        }

        public void DecreaseTileRestAmount()
        {
            _tileRestAmountModel.DecreaseTileRestAmount();
            _desk.ValueDisplayTileRestAmount.DrawImmediate();
        }
    }
}