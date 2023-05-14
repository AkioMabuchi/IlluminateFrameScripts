using Enums;
using Processes;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnPointerEventsFiredSelectFrameSizeScreen : IInitializable
    {
        private readonly SelectSelectFrameSizeScreenProcess _selectSelectFrameSizeScreenProcess;
        private readonly SelectFrameSizeScreen _selectFrameSizeScreen;

        [Inject]
        public OnPointerEventsFiredSelectFrameSizeScreen(
            SelectSelectFrameSizeScreenProcess selectSelectFrameSizeScreenProcess,
            SelectFrameSizeScreen selectFrameSizeScreen)
        {
            _selectSelectFrameSizeScreenProcess = selectSelectFrameSizeScreenProcess;
            _selectFrameSizeScreen = selectFrameSizeScreen;
        }

        public void Initialize()
        {
            _selectFrameSizeScreen.OnPointerEnterImageButtonSmall.Subscribe(_ =>
            {
                _selectSelectFrameSizeScreenProcess.SelectProcess(SelectFrameSizeScreenButtonName.Small);
            });
            
            _selectFrameSizeScreen.OnPointerEnterImageButtonMedium.Subscribe(_ =>
            {
                _selectSelectFrameSizeScreenProcess.SelectProcess(SelectFrameSizeScreenButtonName.Medium);
            });
            
            _selectFrameSizeScreen.OnPointerEnterImageButtonLarge.Subscribe(_ =>
            {
                _selectSelectFrameSizeScreenProcess.SelectProcess(SelectFrameSizeScreenButtonName.Large);
            });

            _selectFrameSizeScreen.OnPointerExitImageButtonSmall.Subscribe(_ =>
            {
                _selectSelectFrameSizeScreenProcess.DeselectProcess(SelectFrameSizeScreenButtonName.Small);
            });
            
            _selectFrameSizeScreen.OnPointerExitImageButtonMedium.Subscribe(_ =>
            {
                _selectSelectFrameSizeScreenProcess.DeselectProcess(SelectFrameSizeScreenButtonName.Medium);
            });
            
            _selectFrameSizeScreen.OnPointerExitImageButtonLarge.Subscribe(_ =>
            {
                _selectSelectFrameSizeScreenProcess.DeselectProcess(SelectFrameSizeScreenButtonName.Large);
            });
        }
    }
}