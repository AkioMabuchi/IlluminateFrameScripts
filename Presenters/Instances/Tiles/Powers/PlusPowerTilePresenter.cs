using Models.Instances.Tiles.Powers;
using UniRx;
using Views.Instances.Tiles.Powers;

namespace Presenters.Instances.Tiles.Powers
{
    public class PlusPowerTilePresenter
    {
        private readonly CompositeDisposable _compositeDisposable = new();

        public PlusPowerTilePresenter(PlusPowerTileModel plusPowerTileModel, PlusPowerTile plusPowerTile)
        {
            plusPowerTileModel.OnChangedElectricStatusLine.Subscribe(electricStatus =>
            {
                plusPowerTile.SetElectricStatusLine(electricStatus);
            }).AddTo(_compositeDisposable);
        }

        public void CompositeDispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}