using Interfaces;
using UniRx;
using Views.Instances.Tiles;

namespace Presenters.Instances.Tiles
{
    public class TilePower1Presenter : TileBasePresenter
    {
        public TilePower1Presenter(ITilePower1 tilePowerMinusModel, TilePower1 tilePower1)
        {
            tilePowerMinusModel.OnChangedRotateStatus.Subscribe(rotateStatus =>
            {
                tilePower1.SetRotateStatus(rotateStatus);
            }).AddTo(compositeDisposable);

            tilePowerMinusModel.OnChangedElectricStatusLine.Subscribe(electricStatus =>
            {
                tilePower1.SetElectricStatusLine(electricStatus);
            }).AddTo(compositeDisposable);

            tilePowerMinusModel.OnChangedElectricStatusPowerSymbol.Subscribe(electricStatus =>
            {
                tilePower1.SetElectricStatusPowerSymbol(electricStatus);
            }).AddTo(compositeDisposable);
        }
    }
}