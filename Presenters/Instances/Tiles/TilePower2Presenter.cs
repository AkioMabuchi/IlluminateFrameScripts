using Models.Instances.Tiles;
using Parameters.Interfaces;
using UniRx;
using Views.Instances.Tiles;

namespace Presenters.Instances.Tiles
{
    public class TilePower2Presenter: TileBasePresenter
    {
        public TilePower2Presenter(ITilePower2 tilePower2Model, TilePower2 tilePower2)
        {
            tilePower2Model.OnChangedRotateStatus.Subscribe(rotateStatus =>
            {
                tilePower2.SetRotateStatus(rotateStatus);
            }).AddTo(CompositeDisposable);

            tilePower2Model.OnChangedElectricStatusLineA.Subscribe(electricStatus =>
            {
                tilePower2.SetElectricStatusLineA(electricStatus);
            }).AddTo(CompositeDisposable);

            tilePower2Model.OnChangedElectricStatusLineB.Subscribe(electricStatus =>
            {
                tilePower2.SetElectricStatusLineB(electricStatus);
            }).AddTo(CompositeDisposable);

            tilePower2Model.OnChangedElectricStatusPowerSymbol.Subscribe(electricStatus =>
            {
                tilePower2.SetElectricStatusPowerSymbol(electricStatus);
            }).AddTo(CompositeDisposable);
        }
    }
}