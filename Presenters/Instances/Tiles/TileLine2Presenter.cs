using Models.Instances.Tiles;
using UniRx;
using Views.Instances.Tiles;
using Parameters.Interfaces;

namespace Presenters.Instances.Tiles
{
    public class TileLine2Presenter : TileBasePresenter
    {
        public TileLine2Presenter(ITileLine2 tileLine2Model, TileLine2 tileLine2)
        {
            tileLine2Model.OnChangedRotateStatus.Subscribe(rotateStatus =>
            {
                tileLine2.SetRotateStatus(rotateStatus);
            }).AddTo(CompositeDisposable);

            tileLine2Model.OnChangedElectricStatusLineA.Subscribe(electricStatus =>
            {
                tileLine2.SetElectricStatusLineA(electricStatus);
            }).AddTo(CompositeDisposable);

            tileLine2Model.OnChangedElectricStatusLineB.Subscribe(electricStatus =>
            {
                tileLine2.SetElectricStatusLineB(electricStatus);
            }).AddTo(CompositeDisposable);
        }
    }
}