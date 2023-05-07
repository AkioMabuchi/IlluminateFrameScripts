using UniRx;

namespace Presenters.Instances.Tiles
{
    public abstract class TileBasePresenter
    {
        protected readonly CompositeDisposable CompositeDisposable = new();
        public void CompositeDispose()
        {
            CompositeDisposable.Dispose();
        }
    }
}