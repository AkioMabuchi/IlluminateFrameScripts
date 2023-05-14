using Models;
using Views;

namespace Processes
{
    public class DecreaseTileRestAmountProcess
    {
        private readonly TileRestAmountModel _tileRestAmountModel;
        private readonly DeskFactory _deskFactory;

        public DecreaseTileRestAmountProcess(TileRestAmountModel tileRestAmountModel, DeskFactory deskFactory)
        {
            _tileRestAmountModel = tileRestAmountModel;
            _deskFactory = deskFactory;
        }

        public void DecreaseTileRestAmount()
        {
            _tileRestAmountModel.DecreaseTileRestAmount();
            _deskFactory.Desk.DisplayTileRestAmount();
        }
    }
}