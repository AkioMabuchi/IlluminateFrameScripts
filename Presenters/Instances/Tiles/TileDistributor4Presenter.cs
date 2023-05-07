using Models.Instances.Tiles;
using Parameters.Interfaces;
using UniRx;
using Views.Instances.Tiles;

namespace Presenters.Instances.Tiles
{
    public class TileDistributor4Presenter : TileBasePresenter
    {
        public TileDistributor4Presenter(ITileLineDistributor4 tileDistributor4Model, TileDistributor4 tileDistributor4)
        {
            tileDistributor4Model.OnChangedRotateStatus.Subscribe(rotateStatus =>
            {
                tileDistributor4.SetRotateStatus(rotateStatus);
            }).AddTo(CompositeDisposable);

            tileDistributor4Model.OnChangedElectricStatusLineA.Subscribe(electricStatus =>
            {
                tileDistributor4.SetElectricStatusLineA(electricStatus);
            }).AddTo(CompositeDisposable);

            tileDistributor4Model.OnChangedElectricStatusLineB.Subscribe(electricStatus =>
            {
                tileDistributor4.SetElectricStatusLineB(electricStatus);
            }).AddTo(CompositeDisposable);

            tileDistributor4Model.OnChangedElectricStatusLineC.Subscribe(electricStatus =>
            {
                tileDistributor4.SetElectricStatusLineC(electricStatus);
            }).AddTo(CompositeDisposable);

            tileDistributor4Model.OnChangedElectricStatusLineD.Subscribe(electricStatus =>
            {
                tileDistributor4.SetElectricStatusLineD(electricStatus);
            }).AddTo(CompositeDisposable);

            tileDistributor4Model.OnChangedElectricStatusCore.Subscribe(electricStatus =>
            {
                tileDistributor4.SetElectricStatusCore(electricStatus);
            }).AddTo(CompositeDisposable);
        }
    }
}