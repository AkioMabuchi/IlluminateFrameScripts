using Models;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class NextTilePresenter : IInitializable
    {
        private readonly NextTileModel _nextTileModel;
        private readonly TileFactory _tileFactory;

        [Inject]
        public NextTilePresenter(NextTileModel nextTileModel, TileFactory tileFactory)
        {
            _nextTileModel = nextTileModel;
            _tileFactory = tileFactory;
        }

        public void Initialize()
        {
            _nextTileModel.OnChangedNextTileId.Subscribe(nextTileId =>
            {
                _tileFactory.SetNextTileId(nextTileId);
            });
        }
    }
}