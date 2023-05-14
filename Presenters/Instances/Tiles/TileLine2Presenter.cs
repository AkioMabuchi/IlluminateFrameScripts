using Interfaces;
using UniRx;
using Views.Instances.Tiles;

namespace Presenters.Instances.Tiles
{
    public class TileLine2Presenter : TileBasePresenter
    {
        public TileLine2Presenter(ITileLine2 tileLine2Model, TileLine2 tileLine2)
        {
            tileLine2Model.OnChangedRotateStatus.Subscribe(rotateStatus =>
            {
                tileLine2.SetRotateStatus(rotateStatus);
            }).AddTo(compositeDisposable);

            tileLine2Model.OnChangedElectricStatusLineA.Subscribe(electricStatus =>
            {
                tileLine2.SetElectricStatusLineA(electricStatus);
            }).AddTo(compositeDisposable);

            tileLine2Model.OnChangedElectricStatusLineB.Subscribe(electricStatus =>
            {
                tileLine2.SetElectricStatusLineB(electricStatus);
            }).AddTo(compositeDisposable);
        }
    }
}