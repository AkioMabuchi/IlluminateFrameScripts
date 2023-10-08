using Models.Instances.Tiles.Dynamics;
using UniRx;
using Views.Instances.Tiles.Dynamics;

namespace Presenters.Instances.Tiles.Dynamics
{
    public class TwinCurvesTilePresenter
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        public TwinCurvesTilePresenter(TwinCurvesTileModel twinCurvesTileModel, TwinCurvesTile twinCurvesTile)
        {
            twinCurvesTileModel.OnChangedRotateStatus.Subscribe(rotateStatus =>
            {
                twinCurvesTile.SetRotateStatus(rotateStatus);
            }).AddTo(_compositeDisposable);

            twinCurvesTileModel.OnChangedElectricStatusLineA.Subscribe(electricStatus =>
            {
                twinCurvesTile.SetElectricStatusLineA(electricStatus);
            }).AddTo(_compositeDisposable);

            twinCurvesTileModel.OnChangedElectricStatusLineB.Subscribe(electricStatus =>
            {
                twinCurvesTile.SetElectricStatusLineB(electricStatus);
            }).AddTo(_compositeDisposable);
        }

        public void CompositeDispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}