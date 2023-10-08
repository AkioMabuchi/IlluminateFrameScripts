using System;
using Enums;
using Enums.ScreenButtonNames;
using Models.ScreenButtons;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;
using Views.Screens;
using Views.Screens.Prior;

namespace Processes.Events.Screens
{
    public class RecordsScreenEventProcess : IInitializable, IDisposable
    {
        private readonly SelectedRecordsScreenButtonModel _selectedRecordsScreenButtonModel;

        private readonly Footer _footer;
        private readonly RecordsScreen _recordsScreen;

        private readonly CompositeDisposable _compositeDisposable = new();

        [Inject]
        public RecordsScreenEventProcess(SelectedRecordsScreenButtonModel selectedRecordsScreenButtonModel,
            Footer footer, RecordsScreen recordsScreen)
        {
            _selectedRecordsScreenButtonModel = selectedRecordsScreenButtonModel;
            _footer = footer;
            _recordsScreen = recordsScreen;
        }

        public void Initialize()
        {
            _recordsScreen.ImageButtonSmall.OnPointerEnter.Subscribe(_ =>
            {
                _selectedRecordsScreenButtonModel.Select(RecordsScreenButtonName.Small);
                _recordsScreen.ImageButtonSmall.ZoomUp();
                _footer.RenderText();
            }).AddTo(_compositeDisposable);
            
            _recordsScreen.ImageButtonMedium.OnPointerEnter.Subscribe(_ =>
            {
                _selectedRecordsScreenButtonModel.Select(RecordsScreenButtonName.Medium);
                _recordsScreen.ImageButtonMedium.ZoomUp();
                _footer.RenderText();
            }).AddTo(_compositeDisposable);
            
            _recordsScreen.ImageButtonLarge.OnPointerEnter.Subscribe(_ =>
            {
                _selectedRecordsScreenButtonModel.Select(RecordsScreenButtonName.Large);
                _recordsScreen.ImageButtonLarge.ZoomUp();
                _footer.RenderText();
            }).AddTo(_compositeDisposable);
            
            _recordsScreen.ImageButtonGlobal.OnPointerEnter.Subscribe(_ =>
            {
                _selectedRecordsScreenButtonModel.Select(RecordsScreenButtonName.Global);
                _recordsScreen.ImageButtonGlobal.ZoomUp();
                _footer.RenderText();
            }).AddTo(_compositeDisposable);
            
            _recordsScreen.ImageButtonFriends.OnPointerEnter.Subscribe(_ =>
            {
                _selectedRecordsScreenButtonModel.Select(RecordsScreenButtonName.Friends);
                _recordsScreen.ImageButtonFriends.ZoomUp();
                _footer.RenderText();
            }).AddTo(_compositeDisposable);

            _recordsScreen.ImageButtonSmall.OnPointerExit.Subscribe(_ =>
            {
                _selectedRecordsScreenButtonModel.Deselect();
                _recordsScreen.ImageButtonSmall.ZoomDown();
                _footer.RenderText();
            }).AddTo(_compositeDisposable);
            
            _recordsScreen.ImageButtonMedium.OnPointerExit.Subscribe(_ =>
            {
                _selectedRecordsScreenButtonModel.Deselect();
                _recordsScreen.ImageButtonMedium.ZoomDown();
                _footer.RenderText();
            }).AddTo(_compositeDisposable);
            
            _recordsScreen.ImageButtonLarge.OnPointerExit.Subscribe(_ =>
            {
                _selectedRecordsScreenButtonModel.Deselect();
                _recordsScreen.ImageButtonLarge.ZoomDown();
                _footer.RenderText();
            }).AddTo(_compositeDisposable);
            
            _recordsScreen.ImageButtonGlobal.OnPointerExit.Subscribe(_ =>
            {
                _selectedRecordsScreenButtonModel.Deselect();
                _recordsScreen.ImageButtonGlobal.ZoomDown();
                _footer.RenderText();
            }).AddTo(_compositeDisposable);

            _recordsScreen.ImageButtonFriends.OnPointerExit.Subscribe(_ =>
            {
                _selectedRecordsScreenButtonModel.Deselect();
                _recordsScreen.ImageButtonFriends.ZoomDown();
                _footer.RenderText();
            }).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}