using Enums;
using Models;
using Processes;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnPointerEventsFiredResultScreen: IInitializable
    {
        private readonly SelectResultScreenButtonProcess _selectResultScreenButtonProcess;
        private readonly ResultScreen _resultScreen;

        [Inject]
        public OnPointerEventsFiredResultScreen(SelectResultScreenButtonProcess selectResultScreenButtonProcess,
            ResultScreen resultScreen)
        {
            _selectResultScreenButtonProcess = selectResultScreenButtonProcess;
            _resultScreen = resultScreen;
        }

        public void Initialize()
        {
            _resultScreen.OnPointerEnterRetry.Subscribe(_ =>
            {
                _selectResultScreenButtonProcess.SelectProcess(ResultScreenButtonName.Retry);
            });
            
            _resultScreen.OnPointerEnterTitle.Subscribe(_ =>
            {
                _selectResultScreenButtonProcess.SelectProcess(ResultScreenButtonName.Title);
            });
            
            _resultScreen.OnPointerEnterRecords.Subscribe(_ =>
            {
                _selectResultScreenButtonProcess.SelectProcess(ResultScreenButtonName.Records);
            });
            
            _resultScreen.OnPointerEnterQuit.Subscribe(_ =>
            {
                _selectResultScreenButtonProcess.SelectProcess(ResultScreenButtonName.Quit);
            });

            _resultScreen.OnPointerExitRetry.Subscribe(_ =>
            {
                _selectResultScreenButtonProcess.DeselectProcess(ResultScreenButtonName.Retry);
            });
            
            _resultScreen.OnPointerExitTitle.Subscribe(_ =>
            {
                _selectResultScreenButtonProcess.DeselectProcess(ResultScreenButtonName.Title);
            });
            
            _resultScreen.OnPointerExitRecords.Subscribe(_ =>
            {
                _selectResultScreenButtonProcess.DeselectProcess(ResultScreenButtonName.Records);
            });
            
            _resultScreen.OnPointerExitQuit.Subscribe(_ =>
            {
                _selectResultScreenButtonProcess.DeselectProcess(ResultScreenButtonName.Quit);
            });
        }
    }
}