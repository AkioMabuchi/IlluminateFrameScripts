using Models.Instances.Tiles;
using Parameters.Interfaces;
using UniRx;
using Views.Instances.Tiles;

namespace Presenters.Instances.Tiles
{
    public class TileBulbPresenter : TileBasePresenter
    {
        public TileBulbPresenter(ITileBulb tileBulbModel, TileBulb tileBulb)
        {
            tileBulbModel.OnChangedRotateStatus.Subscribe(rotateStatus =>
            {
                tileBulb.SetRotateStatus(rotateStatus);
            }).AddTo(CompositeDisposable);

            tileBulbModel.OnChangedElectricStatusLine.Subscribe(electricStatus =>
            {
                tileBulb.SetElectricStatusLine(electricStatus);
            }).AddTo(CompositeDisposable);

            tileBulbModel.OnChangedElectricStatusBulb.Subscribe(electricStatus =>
            {
                tileBulb.SetElectricStatusBulb(electricStatus);
            }).AddTo(CompositeDisposable);
        }
    }
}