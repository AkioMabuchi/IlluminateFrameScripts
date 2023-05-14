using Models;
using Models.Instances.Frames;
using Presenters.Instances.Frames;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class MainFramePresenter : IInitializable
    {
        private readonly MainFrameModel _mainFrameModel;
        private readonly FrameFactory _frameFactory;

        private FrameBasePresenter _framePresenter;
        
        [Inject]
        public MainFramePresenter(MainFrameModel mainFrameModel, FrameFactory frameFactory)
        {
            _mainFrameModel = mainFrameModel;
            _frameFactory = frameFactory;
        }
        
        public void Initialize()
        {
            _framePresenter?.CompositeDispose();
            _framePresenter = null;
            _frameFactory.DestroyFrame();
            
            _mainFrameModel.OnChangedFrameModel.Subscribe(frameModel =>
            {
                switch (frameModel)
                {
                    case FrameSmallModel frameSmallModel:
                    {
                        var frameSmall = _frameFactory.GenerateFrameSmall();
                        _framePresenter = new FrameSmallPresenter(frameSmallModel, frameSmall);
                        break;
                    }
                    case FrameMediumModel frameMediumModel:
                    {
                        var frameMedium = _frameFactory.GenerateFrameMedium();
                        _framePresenter = new FrameMediumPresenter(frameMediumModel, frameMedium);
                        break;
                    }
                    case FrameLargeModel frameLargeModel:
                    {
                        var frameLarge = _frameFactory.GenerateFrameLarge();
                        _framePresenter = new FrameLargePresenter(frameLargeModel, frameLarge);
                        break;
                    }
                }
            });
        }
    }
}