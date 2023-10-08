using Models.Instances.Frames;
using UniRx;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class FrameMediumPresenter : IInitializable
    {
        private readonly FrameMediumModel _frameMediumModel;
        private readonly FrameMedium _frameMedium;
        
        public FrameMediumPresenter(FrameMediumModel frameMediumModel, FrameMedium frameMedium)
        {
            _frameMediumModel = frameMediumModel;
            _frameMedium = frameMedium;
        }

        public void Initialize()
        {
            _frameMediumModel.OnChangedIsIlluminatedBulbTerminalPlusTop.Subscribe(isIlluminated =>
            {
                _frameMedium.IlluminateBulbPlusTop(isIlluminated);
            });
            
            _frameMediumModel.OnChangedIsIlluminatedBulbTerminalPlusBottom.Subscribe(isIlluminated =>
            {
                _frameMedium.IlluminateBulbPlusBottom(isIlluminated);
            });
            
            _frameMediumModel.OnChangedIsIlluminatedBulbTerminalMinusTop.Subscribe(isIlluminated =>
            {
                _frameMedium.IlluminateBulbMinusTop(isIlluminated);
            });
            
            _frameMediumModel.OnChangedIsIlluminatedBulbTerminalMinusBottom.Subscribe(isIlluminated =>
            {
                _frameMedium.IlluminateBulbMinusBottom(isIlluminated);
            });

            _frameMediumModel.OnChangedIsIlluminatedBulbIllumination.Subscribe(isIlluminated =>
            {
                _frameMedium.IlluminateBulbIllumination(isIlluminated);
            });
        }
    }
}