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
    public class SelectFrameSizeScreenEventProcess : IInitializable, IDisposable
    {
        private readonly SelectedSelectFrameSizeScreenButtonModel _selectedSelectFrameSizeScreenButtonModel;

        private readonly Footer _footer;
        private readonly SelectFrameSizeScreen _selectFrameSizeScreen;

        private readonly CompositeDisposable _compositeDisposable = new();

        [Inject]
        public SelectFrameSizeScreenEventProcess(
            SelectedSelectFrameSizeScreenButtonModel selectedSelectFrameSizeScreenButtonModel,
            Footer footer, SelectFrameSizeScreen selectFrameSizeScreen)
        {
            _selectedSelectFrameSizeScreenButtonModel = selectedSelectFrameSizeScreenButtonModel;
            _footer = footer;
            _selectFrameSizeScreen = selectFrameSizeScreen;
        }

        public void Initialize()
        {
            _selectFrameSizeScreen.ImageButtonSmall.OnPointerEnter.Subscribe(_ =>
            {
                _selectedSelectFrameSizeScreenButtonModel.Select(SelectFrameSizeScreenButtonName.Small);
                _selectFrameSizeScreen.ImageButtonSmall.ZoomUp();
                _footer.RenderText();
            }).AddTo(_compositeDisposable);
            
            _selectFrameSizeScreen.ImageButtonMedium.OnPointerEnter.Subscribe(_ =>
            {
                _selectedSelectFrameSizeScreenButtonModel.Select(SelectFrameSizeScreenButtonName.Medium);
                _selectFrameSizeScreen.ImageButtonMedium.ZoomUp();
                _footer.RenderText();
            }).AddTo(_compositeDisposable);
            
            _selectFrameSizeScreen.ImageButtonLarge.OnPointerEnter.Subscribe(_ =>
            {
                _selectedSelectFrameSizeScreenButtonModel.Select(SelectFrameSizeScreenButtonName.Large);
                _selectFrameSizeScreen.ImageButtonLarge.ZoomUp();
                _footer.RenderText();
            }).AddTo(_compositeDisposable);

            _selectFrameSizeScreen.ImageButtonSmall.OnPointerExit.Subscribe(_ =>
            {
                _selectedSelectFrameSizeScreenButtonModel.Deselect();
                _selectFrameSizeScreen.ImageButtonSmall.ZoomDown();
                _footer.RenderText();
            }).AddTo(_compositeDisposable);
            
            _selectFrameSizeScreen.ImageButtonMedium.OnPointerExit.Subscribe(_ =>
            {
                _selectedSelectFrameSizeScreenButtonModel.Deselect();
                _selectFrameSizeScreen.ImageButtonMedium.ZoomDown();
                _footer.RenderText();
            }).AddTo(_compositeDisposable);

            _selectFrameSizeScreen.ImageButtonLarge.OnPointerExit.Subscribe(_ =>
            {
                _selectedSelectFrameSizeScreenButtonModel.Deselect();
                _selectFrameSizeScreen.ImageButtonLarge.ZoomDown();
                _footer.RenderText();
            }).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}