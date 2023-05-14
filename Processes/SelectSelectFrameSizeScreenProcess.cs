using Cysharp.Threading.Tasks;
using Enums;
using Models;
using VContainer;
using Views;

namespace Processes
{
    public class SelectSelectFrameSizeScreenProcess
    {
        private readonly SelectedSelectFrameSizeScreenButtonModel _selectedSelectFrameSizeScreenButtonModel;
        private readonly SelectFrameSizeScreen _selectFrameSizeScreen;
        private readonly Header _header;
        private readonly Footer _footer;
        private readonly StartMainGameProcess _startMainGameProcess;

        [Inject]
        public SelectSelectFrameSizeScreenProcess(
            SelectedSelectFrameSizeScreenButtonModel selectedSelectFrameSizeScreenButtonModel,
            SelectFrameSizeScreen selectFrameSizeScreen, Header header, Footer footer,
            StartMainGameProcess startMainGameProcess)
        {
            _selectedSelectFrameSizeScreenButtonModel = selectedSelectFrameSizeScreenButtonModel;
            _selectFrameSizeScreen = selectFrameSizeScreen;
            _header = header;
            _footer = footer;
            _startMainGameProcess = startMainGameProcess;
        }

        public void SelectProcess(SelectFrameSizeScreenButtonName selectFrameSizeScreenButtonName)
        {
            _selectedSelectFrameSizeScreenButtonModel.Select(selectFrameSizeScreenButtonName);

            _selectFrameSizeScreen.ZoomUpButtons(_selectedSelectFrameSizeScreenButtonModel
                .SelectFrameSizeScreenButtonName);

            _footer.ChangeFootingText(_selectedSelectFrameSizeScreenButtonModel
                    .SelectFrameSizeScreenButtonName switch
                {
                    SelectFrameSizeScreenButtonName.Small => FooterFootingText.SelectFrameSizeSmall,
                    SelectFrameSizeScreenButtonName.Medium => FooterFootingText.SelectFrameSizeMedium,
                    SelectFrameSizeScreenButtonName.Large => FooterFootingText.SelectFrameSizeLarge,
                    _ => FooterFootingText.None
                });
        }

        public void DeselectProcess(SelectFrameSizeScreenButtonName selectFrameSizeScreenButtonName)
        {
            _selectedSelectFrameSizeScreenButtonModel.Deselect(selectFrameSizeScreenButtonName);

            _selectFrameSizeScreen.ZoomUpButtons(_selectedSelectFrameSizeScreenButtonModel
                .SelectFrameSizeScreenButtonName);
            
            _footer.ChangeFootingText(_selectedSelectFrameSizeScreenButtonModel
                    .SelectFrameSizeScreenButtonName switch
                {
                    SelectFrameSizeScreenButtonName.Small => FooterFootingText.SelectFrameSizeSmall,
                    SelectFrameSizeScreenButtonName.Medium => FooterFootingText.SelectFrameSizeMedium,
                    SelectFrameSizeScreenButtonName.Large => FooterFootingText.SelectFrameSizeLarge,
                    _ => FooterFootingText.None
                });
        }

        public void DecideProcess()
        {
            switch (_selectedSelectFrameSizeScreenButtonModel.SelectFrameSizeScreenButtonName)
            {
                case SelectFrameSizeScreenButtonName.Small:
                {
                    _header.PullUp();
                    _selectFrameSizeScreen.FadeOut();
                    _startMainGameProcess.AsyncStartMainGame(FrameSize.Small).Forget();
                    break;
                }
                case SelectFrameSizeScreenButtonName.Medium:
                {
                    _header.PullUp();
                    _selectFrameSizeScreen.FadeOut();
                    _startMainGameProcess.AsyncStartMainGame(FrameSize.Medium).Forget();
                    break;
                }
                case SelectFrameSizeScreenButtonName.Large:
                {
                    _header.PullUp();
                    _selectFrameSizeScreen.FadeOut();
                    _startMainGameProcess.AsyncStartMainGame(FrameSize.Large).Forget();
                    break;
                }
            }
            
            _selectedSelectFrameSizeScreenButtonModel.Clear();
        }
    }
}