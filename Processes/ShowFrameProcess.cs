using Enums;
using VContainer;
using Views;

namespace Processes
{
    public class ShowFrameProcess
    {
        private readonly FrameSmall _frameSmall;
        private readonly FrameMedium _frameMedium;
        private readonly FrameLarge _frameLarge;

        [Inject]
        public ShowFrameProcess(FrameSmall frameSmall, FrameMedium frameMedium, FrameLarge frameLarge)
        {
            _frameSmall = frameSmall;
            _frameMedium = frameMedium;
            _frameLarge = frameLarge;
        }

        public void ShowFrame(FrameSize frameSize)
        {
            switch (frameSize)
            {
                case FrameSize.Small:
                {
                    _frameSmall.Show();
                    _frameMedium.Hide();
                    _frameLarge.Hide();
                    break;
                }
                case FrameSize.Medium:
                {
                    _frameSmall.Hide();
                    _frameMedium.Show();
                    _frameLarge.Hide();
                    break;
                }
                case FrameSize.Large:
                {
                    _frameSmall.Hide();
                    _frameMedium.Hide();
                    _frameLarge.Show();
                    break;
                }
                default:
                {
                    _frameSmall.Hide();
                    _frameMedium.Hide();
                    _frameLarge.Hide();
                    break;
                }
            }
        }
    }
}