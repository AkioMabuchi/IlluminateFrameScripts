using Models.Instances.Tiles.Dynamics;
using UniRx;
using Views.Instances.Tiles.Dynamics;

namespace Presenters.Instances.Tiles.Dynamics
{
    public class CrossTilePresenter
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        public CrossTilePresenter(CrossTileModel crossTileModel, CrossTile crossTile)
        {
            crossTileModel.OnChangedRotateStatus.Subscribe(rotateStatus =>
            {
                crossTile.SetRotateStatus(rotateStatus);
            }).AddTo(_compositeDisposable);

            crossTileModel.OnChangedElectricStatusLineA.Subscribe(electricStatus =>
            {
                crossTile.SetElectricStatusLineA(electricStatus);
            }).AddTo(_compositeDisposable);

            crossTileModel.OnChangedElectricStatusLineB.Subscribe(electricStatus =>
            {
                crossTile.SetElectricStatusLineB(electricStatus);
            }).AddTo(_compositeDisposable);
        }

        public void CompositeDispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}