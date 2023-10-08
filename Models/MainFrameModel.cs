using Interfaces;
using UniRx;

namespace Models
{
    public class MainFrameModel
    {
        private readonly ReactiveProperty<IFrameModel> _reactivePropertyFrameModel = new(null);
        public IFrameModel FrameModel => _reactivePropertyFrameModel.Value;

        public void SetMainFrame(IFrameModel frameModel)
        {
            _reactivePropertyFrameModel.Value = frameModel;
        }

        public void ClearMainFrame()
        {
            _reactivePropertyFrameModel.Value = null;
        }
    }
}