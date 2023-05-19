using Enums;
using Processes;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnPointerEventsFiredRecordsScreen : IInitializable
    {
        private readonly SelectRecordsScreenButtonProcess _selectRecordsScreenButtonProcess;
        private readonly RecordsScreen _recordsScreen;

        [Inject]
        public OnPointerEventsFiredRecordsScreen(SelectRecordsScreenButtonProcess selectRecordsScreenButtonProcess,
            RecordsScreen recordsScreen)
        {
            _selectRecordsScreenButtonProcess = selectRecordsScreenButtonProcess;
            _recordsScreen = recordsScreen;
        }

        public void Initialize()
        {
            _recordsScreen.OnPointerEnterSmall.Subscribe(_ =>
            {
                _selectRecordsScreenButtonProcess.SelectProcess(RecordsScreenButtonName.Small);
            });
            
            _recordsScreen.OnPointerEnterMedium.Subscribe(_ =>
            {
                _selectRecordsScreenButtonProcess.SelectProcess(RecordsScreenButtonName.Medium);
            });
            
            _recordsScreen.OnPointerEnterLarge.Subscribe(_ =>
            {
                _selectRecordsScreenButtonProcess.SelectProcess(RecordsScreenButtonName.Large);
            });
            
            _recordsScreen.OnPointerEnterGlobal.Subscribe(_ =>
            {
                _selectRecordsScreenButtonProcess.SelectProcess(RecordsScreenButtonName.Global);
            });
            
            _recordsScreen.OnPointerEnterFriends.Subscribe(_ =>
            {
                _selectRecordsScreenButtonProcess.SelectProcess(RecordsScreenButtonName.Friends);
            });

            _recordsScreen.OnPointerExitSmall.Subscribe(_ =>
            {
                _selectRecordsScreenButtonProcess.DeselectProcess(RecordsScreenButtonName.Small);
            });
            
            _recordsScreen.OnPointerExitMedium.Subscribe(_ =>
            {
                _selectRecordsScreenButtonProcess.DeselectProcess(RecordsScreenButtonName.Medium);
            });
            
            _recordsScreen.OnPointerExitLarge.Subscribe(_ =>
            {
                _selectRecordsScreenButtonProcess.DeselectProcess(RecordsScreenButtonName.Large);
            });
            
            _recordsScreen.OnPointerExitGlobal.Subscribe(_ =>
            {
                _selectRecordsScreenButtonProcess.DeselectProcess(RecordsScreenButtonName.Global);
            });
            
            _recordsScreen.OnPointerExitFriends.Subscribe(_ =>
            {
                _selectRecordsScreenButtonProcess.DeselectProcess(RecordsScreenButtonName.Friends);
            });
        }
    }
}