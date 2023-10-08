using Models.Instances.Tiles.Powers;
using UniRx;
using Views.Instances.Tiles.Powers;

namespace Presenters.Instances.Tiles.Powers
{
    public class AlternatingPowerTilePresenter
    {
        private readonly CompositeDisposable _compositeDisposable = new();

        public AlternatingPowerTilePresenter(AlternatingPowerTileModel alternatingPowerTileModel,
            AlternatingPowerTile alternatingPowerTile)
        {
            alternatingPowerTileModel.OnChangedElectricStatusLineA.Subscribe(electricStatus =>
            {
                alternatingPowerTile.SetElectricStatusLineA(electricStatus);
            }).AddTo(_compositeDisposable);

            alternatingPowerTileModel.OnChangedElectricStatusLineB.Subscribe(electricStatus =>
            {
                alternatingPowerTile.SetElectricStatusLineB(electricStatus);
            }).AddTo(_compositeDisposable);
        }

        public void CompositeDispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}