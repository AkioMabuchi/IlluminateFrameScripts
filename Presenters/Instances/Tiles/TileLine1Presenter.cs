using Parameters.Interfaces;
using UniRx;
using Views.Instances.Tiles;

namespace Presenters.Instances.Tiles
{
    public class TileLine1Presenter: TileBasePresenter
    {
        public TileLine1Presenter(ITileLine1 tileLine1Model, TileLine1 tileLine1)
        {
            tileLine1Model.OnChangedRotateStatus.Subscribe(rotateStatus =>
            {
                tileLine1.SetRotateStatus(rotateStatus);
            }).AddTo(CompositeDisposable);

            tileLine1Model.OnChangedElectricStatusLine.Subscribe(electricStatus =>
            {
                tileLine1.SetElectricStatusLine(electricStatus);
            }).AddTo(CompositeDisposable);
        }
    }
}