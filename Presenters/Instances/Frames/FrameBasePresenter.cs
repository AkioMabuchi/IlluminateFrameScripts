using UniRx;

namespace Presenters.Instances.Frames
{
    public abstract class FrameBasePresenter
    {
        protected readonly CompositeDisposable compositeDisposable = new();

        public void CompositeDispose()
        {
            compositeDisposable.Dispose();
        }
    }
}