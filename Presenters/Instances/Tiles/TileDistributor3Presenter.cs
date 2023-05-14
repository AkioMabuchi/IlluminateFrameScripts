using Interfaces;
using UniRx;
using Views.Instances.Tiles;

namespace Presenters.Instances.Tiles
{
    public class TileDistributor3Presenter : TileBasePresenter
    {
        public TileDistributor3Presenter(ITileLineDistributor3 tileDistributor3Model, TileDistributor3 tileDistributor3)
        {
            tileDistributor3Model.OnChangedRotateStatus.Subscribe(rotateStatus =>
            {
                tileDistributor3.SetRotateStatus(rotateStatus);
            }).AddTo(compositeDisposable);

            tileDistributor3Model.OnChangedElectricStatusLineA.Subscribe(electricStatus =>
            {
                tileDistributor3.SetElectricStatusLineA(electricStatus);
            }).AddTo(compositeDisposable);

            tileDistributor3Model.OnChangedElectricStatusLineB.Subscribe(electricStatus =>
            {
                tileDistributor3.SetElectricStatusLineB(electricStatus);
            }).AddTo(compositeDisposable);

            tileDistributor3Model.OnChangedElectricStatusLineC.Subscribe(electricStatus =>
            {
                tileDistributor3.SetElectricStatusLineC(electricStatus);
            }).AddTo(compositeDisposable);
            
            tileDistributor3Model.OnChangedElectricStatusCore.Subscribe(electricStatus =>
            {
                tileDistributor3.SetElectricStatusCore(electricStatus);
            }).AddTo(compositeDisposable);
        }
    }
}