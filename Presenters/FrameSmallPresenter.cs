using Models.Instances.Frames;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class FrameSmallPresenter : IInitializable
    {
        private readonly FrameSmallModel _frameSmallModel;
        private readonly FrameSmall _frameSmall;

        [Inject]
        public FrameSmallPresenter(FrameSmallModel frameSmallModel, FrameSmall frameSmall)
        {
            _frameSmallModel = frameSmallModel;
            _frameSmall = frameSmall;
        }

        public void Initialize()
        {
            _frameSmallModel.OnChangedIsIlluminatedBulbTerminalA.Subscribe(isIlluminated =>
            {
                _frameSmall.IlluminateBulbTerminalA(isIlluminated);
            });

            _frameSmallModel.OnChangedIsIlluminatedBulbTerminalB.Subscribe(isIlluminated =>
            {
                _frameSmall.IlluminateBulbTerminalB(isIlluminated);
            });

            _frameSmallModel.OnChangedIsIlluminatedBulbTerminalC.Subscribe(isIlluminated =>
            {
                _frameSmall.IlluminateBulbTerminalC(isIlluminated);
            });

            _frameSmallModel.OnChangedIsIlluminatedBulbTerminalD.Subscribe(isIlluminated =>
            {
                _frameSmall.IlluminateBulbTerminalD(isIlluminated);
            });

            _frameSmallModel.OnChangedIsIlluminatedBulbIllumination.Subscribe(isIlluminated =>
            {
                _frameSmall.IlluminateBulbsIllumination(isIlluminated);
            });
        }
    }
}