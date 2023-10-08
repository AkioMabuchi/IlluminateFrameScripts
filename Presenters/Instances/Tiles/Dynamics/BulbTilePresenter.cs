using Models.Instances.Tiles.Dynamics;
using UniRx;
using Views.Instances.Tiles.Dynamics;

namespace Presenters.Instances.Tiles.Dynamics
{
    public class BulbTilePresenter
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        public BulbTilePresenter(BulbTileModel bulbTileModel, BulbTile bulbTile)
        {
            bulbTileModel.OnChangedRotateStatus.Subscribe(rotateStatus =>
            {
                bulbTile.SetRotateStatus(rotateStatus);
            }).AddTo(_compositeDisposable);

            bulbTileModel.OnChangedElectricStatusLine.Subscribe(electricStatus =>
            {
                bulbTile.SetElectricStatusLine(electricStatus);
            }).AddTo(_compositeDisposable);

            bulbTileModel.OnChangedElectricStatusBulb.Subscribe(electricStatus =>
            {
                bulbTile.SetElectricStatusBulb(electricStatus);
            }).AddTo(_compositeDisposable);
        }

        public void CompositeDispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}