using Models.Instances.Tiles.Dynamics;
using UniRx;
using Views.Instances.Tiles.Dynamics;

namespace Presenters.Instances.Tiles.Dynamics
{
    public class StraightTilePresenter
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        public StraightTilePresenter(StraightTileModel straightTileModel, StraightTile straightTile)
        {
            straightTileModel.OnChangedRotateStatus.Subscribe(rotateStatus =>
            {
                straightTile.SetRotateStatus(rotateStatus);
            }).AddTo(_compositeDisposable);

            straightTileModel.OnChangedElectricStatusLine.Subscribe(electricStatus =>
            {
                straightTile.SetElectricStatusLine(electricStatus);
            }).AddTo(_compositeDisposable);
        }

        public void CompositeDispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}