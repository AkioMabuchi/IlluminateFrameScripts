using Interfaces;
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
            }).AddTo(compositeDisposable);

            tileBulbModel.OnChangedElectricStatusLine.Subscribe(electricStatus =>
            {
                tileBulb.SetElectricStatusLine(electricStatus);
            }).AddTo(compositeDisposable);

            tileBulbModel.OnChangedElectricStatusBulb.Subscribe(electricStatus =>
            {
                tileBulb.SetElectricStatusBulb(electricStatus);
            }).AddTo(compositeDisposable);
        }
    }
}