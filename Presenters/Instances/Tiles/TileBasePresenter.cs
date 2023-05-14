using UniRx;

namespace Presenters.Instances.Tiles
{
    public abstract class TileBasePresenter
    {
        protected readonly CompositeDisposable compositeDisposable = new();
        public void CompositeDispose()
        {
            compositeDisposable.Dispose();
        }
    }
}