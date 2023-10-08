using Models.Instances.Tiles.Dynamics;
using UniRx;
using Views.Instances.Tiles.Dynamics;

namespace Presenters.Instances.Tiles.Dynamics
{
    public class ThreeWayDistributorTilePresenter
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        public ThreeWayDistributorTilePresenter(
            ThreeWayDistributorTileModel threeWayDistributorTileModel,
            ThreeWayDistributorTile threeWayDistributorTile)
        {
            threeWayDistributorTileModel.OnChangedRotateStatus.Subscribe(rotateStatus =>
            {
                threeWayDistributorTile.SetRotateStatus(rotateStatus);
            }).AddTo(_compositeDisposable);

            threeWayDistributorTileModel.OnChangedElectricStatusLineA.Subscribe(electricStatus =>
            {
                threeWayDistributorTile.SetElectricStatusLineA(electricStatus);
            }).AddTo(_compositeDisposable);

            threeWayDistributorTileModel.OnChangedElectricStatusLineB.Subscribe(electricStatus =>
            {
                threeWayDistributorTile.SetElectricStatusLineB(electricStatus);
            }).AddTo(_compositeDisposable);

            threeWayDistributorTileModel.OnChangedElectricStatusLineC.Subscribe(electricStatus =>
            {
                threeWayDistributorTile.SetElectricStatusLineC(electricStatus);
            }).AddTo(_compositeDisposable);

            threeWayDistributorTileModel.OnChangedElectricStatusCore.Subscribe(electricStatus =>
            {
                threeWayDistributorTile.SetElectricStatusCore(electricStatus);
            }).AddTo(_compositeDisposable);
        }

        public void CompositeDispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}