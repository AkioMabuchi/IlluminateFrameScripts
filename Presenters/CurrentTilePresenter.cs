using Models;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class CurrentTilePresenter: IInitializable
    {
        private readonly CurrentTileModel _currentTileModel;
        private readonly TileFactory _tileFactory;

        [Inject]
        public CurrentTilePresenter(CurrentTileModel currentTileModel, TileFactory tileFactory)
        {
            _currentTileModel = currentTileModel;
            _tileFactory = tileFactory;
        }
        
        public void Initialize()
        {
            _currentTileModel.OnChangedCurrentTileId.Subscribe(currentTileId =>
            {
                _tileFactory.SetCurrentTileId(currentTileId);
            });
        }
    }
}