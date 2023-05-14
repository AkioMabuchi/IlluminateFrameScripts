using Models;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class TileRestAmountBoardPresenter : IInitializable
    {
        private readonly TileRestAmountModel _tileRestAmountModel;
        private readonly TileRestAmountBoardFactory _tileRestAmountBoardFactory;

        [Inject]
        public TileRestAmountBoardPresenter(TileRestAmountModel tileRestAmountModel,
            TileRestAmountBoardFactory tileRestAmountBoardFactory)
        {
            _tileRestAmountModel = tileRestAmountModel;
            _tileRestAmountBoardFactory = tileRestAmountBoardFactory;
        }

        public void Initialize()
        {
            _tileRestAmountModel.OnChangedTileRestAmount.Subscribe(tileRestAmount =>
            {
                if (_tileRestAmountBoardFactory.TileRestAmountBoard)
                {
                    _tileRestAmountBoardFactory.TileRestAmountBoard.SetDisplayNumber(tileRestAmount);
                }
            });
        }
    }
}