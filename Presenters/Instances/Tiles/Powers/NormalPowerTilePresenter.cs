using Models.Instances.Tiles.Powers;
using UniRx;
using Views.Instances.Tiles.Powers;

namespace Presenters.Instances.Tiles.Powers
{
    public class NormalPowerTilePresenter
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        public NormalPowerTilePresenter(NormalPowerTileModel normalPowerTileModel, NormalPowerTile normalPowerTile)
        {
            normalPowerTileModel.OnChangedElectricStatusLineA.Subscribe(electricStatus =>
            {
                normalPowerTile.SetElectricStatusLineA(electricStatus);
            }).AddTo(_compositeDisposable);

            normalPowerTileModel.OnChangedElectricStatusLineB.Subscribe(electricStatus =>
            {
                normalPowerTile.SetElectricStatusLineB(electricStatus);
            }).AddTo(_compositeDisposable);
        }

        public void CompositeDispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}