using Models.Instances.Tiles.Dynamics;
using UniRx;
using Views.Instances.Tiles.Dynamics;

namespace Presenters.Instances.Tiles.Dynamics
{
    public class FourWayDistributorPresenter
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        public FourWayDistributorPresenter(FourWayDistributorTileModel fourWayDistributorTileModel,
            FourWayDistributorTile fourWayDistributorTile)
        {
            fourWayDistributorTileModel.OnChangedRotateStatus.Subscribe(rotateStatus =>
            {
                fourWayDistributorTile.SetRotateStatus(rotateStatus);
            }).AddTo(_compositeDisposable);

            fourWayDistributorTileModel.OnChangedElectricStatusLineA.Subscribe(electricStatus =>
            {
                fourWayDistributorTile.SetElectricStatusLineA(electricStatus);
            }).AddTo(_compositeDisposable);

            fourWayDistributorTileModel.OnChangedElectricStatusLineB.Subscribe(electricStatus =>
            {
                fourWayDistributorTile.SetElectricStatusLineB(electricStatus);
            }).AddTo(_compositeDisposable);

            fourWayDistributorTileModel.OnChangedElectricStatusLineC.Subscribe(electricStatus =>
            {
                fourWayDistributorTile.SetElectricStatusLineC(electricStatus);
            }).AddTo(_compositeDisposable);

            fourWayDistributorTileModel.OnChangedElectricStatusLineD.Subscribe(electricStatus =>
            {
                fourWayDistributorTile.SetElectricStatusLineD(electricStatus);
            }).AddTo(_compositeDisposable);

            fourWayDistributorTileModel.OnChangedElectricStatusCore.Subscribe(electricStatus =>
            {
                fourWayDistributorTile.SetElectricStatusCore(electricStatus);
            }).AddTo(_compositeDisposable);
        }

        public void CompositeDispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}