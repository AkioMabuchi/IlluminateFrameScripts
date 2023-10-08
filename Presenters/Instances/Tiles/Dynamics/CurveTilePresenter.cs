using Models.Instances.Tiles.Dynamics;
using UniRx;
using Views.Instances.Tiles.Dynamics;

namespace Presenters.Instances.Tiles.Dynamics
{
    public class CurveTilePresenter
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        public CurveTilePresenter(CurveTileModel curveTileModel, CurveTile curveTile)
        {
            curveTileModel.OnChangedRotateStatus.Subscribe(rotateStatus =>
            {
                curveTile.SetRotateStatus(rotateStatus);
            }).AddTo(_compositeDisposable);

            curveTileModel.OnChangedElectricStatusLine.Subscribe(electricStatus =>
            {
                curveTile.SetElectricStatusLine(electricStatus);
            }).AddTo(_compositeDisposable);
        }

        public void CompositeDispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}