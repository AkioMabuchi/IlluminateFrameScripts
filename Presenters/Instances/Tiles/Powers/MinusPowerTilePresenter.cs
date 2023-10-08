using Models.Instances.Tiles.Powers;
using UniRx;
using Views.Instances.Tiles.Powers;

namespace Presenters.Instances.Tiles.Powers
{
    public class MinusPowerTilePresenter
    {
        private readonly CompositeDisposable _compositeDisposable = new();

        public MinusPowerTilePresenter(MinusPowerTileModel minusPowerTileModel, MinusPowerTile minusPowerTile)
        {
            minusPowerTileModel.OnChangedElectricStatusLine.Subscribe(electricStatus =>
            {
                minusPowerTile.SetElectricStatusLine(electricStatus);
            }).AddTo(_compositeDisposable);
        }

        public void CompositeDispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}