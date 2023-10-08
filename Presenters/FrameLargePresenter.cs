using Models.Instances.Frames;
using UniRx;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class FrameLargePresenter : IInitializable
    {
        private readonly FrameLargeModel _frameLargeModel;
        private readonly FrameLarge _frameLarge;
        
        public FrameLargePresenter(FrameLargeModel frameLargeModel, FrameLarge frameLarge)
        {
            _frameLargeModel = frameLargeModel;
            _frameLarge = frameLarge;
        }

        public void Initialize()
        {
            _frameLargeModel.OnChangedIsIlluminatedBulbTerminalPlusTop.Subscribe(isIlluminated =>
            {
                _frameLarge.IlluminateBulbPlusTop(isIlluminated);
            });
            
            _frameLargeModel.OnChangedIsIlluminatedBulbTerminalPlusBottom.Subscribe(isIlluminated =>
            {
                _frameLarge.IlluminateBulbPlusBottom(isIlluminated);
            });
            
            _frameLargeModel.OnChangedIsIlluminatedBulbTerminalMinusTop.Subscribe(isIlluminated =>
            {
                _frameLarge.IlluminateBulbMinusTop(isIlluminated);
            });
            
            _frameLargeModel.OnChangedIsIlluminatedBulbTerminalMinusBottom.Subscribe(isIlluminated =>
            {
                _frameLarge.IlluminateBulbMinusBottom(isIlluminated);
            });
            
            _frameLargeModel.OnChangedIsIlluminatedBulbTerminalAlternatingLeft.Subscribe(isIlluminated =>
            {
                _frameLarge.IlluminateBulbAlternatingLeft(isIlluminated);
            });
            
            _frameLargeModel.OnChangedIsIlluminatedBulbTerminalAlternatingRight.Subscribe(isIlluminated =>
            {
                _frameLarge.IlluminateBulbAlternatingRight(isIlluminated);
            });

            _frameLargeModel.OnChangedIsIlluminatedBulbIllumination.Subscribe(isIlluminated =>
            {
                _frameLarge.IlluminateBulbIllumination(isIlluminated);
            });
        }
    }
}