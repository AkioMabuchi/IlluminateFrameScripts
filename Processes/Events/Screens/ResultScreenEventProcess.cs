using System;
using Enums;
using Enums.ScreenButtonNames;
using Models.ScreenButtons;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;
using Views.Screens;

namespace Processes.Events.Screens
{
    public class ResultScreenEventProcess : IInitializable, IDisposable
    {
        private readonly SelectedResultScreenButtonModel _selectedResultScreenButtonModel;

        private readonly SteamResultScreen _steamResultScreen;

        private readonly CompositeDisposable _compositeDisposable = new();

        [Inject]
        public ResultScreenEventProcess(SelectedResultScreenButtonModel selectedResultScreenButtonModel,
            SteamResultScreen steamResultScreen)
        {
            _selectedResultScreenButtonModel = selectedResultScreenButtonModel;
            _steamResultScreen = steamResultScreen;
        }

        public void Initialize()
        {
            _steamResultScreen.ImageButtonRetry.OnPointerEnter.Subscribe(_ =>
            {
                _selectedResultScreenButtonModel.Select(ResultScreenButtonName.Retry);
                _steamResultScreen.ImageButtonRetry.ZoomUp();
            }).AddTo(_compositeDisposable);

            _steamResultScreen.ImageButtonTitle.OnPointerEnter.Subscribe(_ =>
            {
                _selectedResultScreenButtonModel.Select(ResultScreenButtonName.Title);
                _steamResultScreen.ImageButtonTitle.ZoomUp();
            }).AddTo(_compositeDisposable);

            _steamResultScreen.ImageButtonRecords.OnPointerEnter.Subscribe(_ =>
            {
                _selectedResultScreenButtonModel.Select(ResultScreenButtonName.Records);
                _steamResultScreen.ImageButtonRecords.ZoomUp();
            }).AddTo(_compositeDisposable);

            _steamResultScreen.ImageButtonQuit.OnPointerEnter.Subscribe(_ =>
            {
                _selectedResultScreenButtonModel.Select(ResultScreenButtonName.Quit);
                _steamResultScreen.ImageButtonQuit.ZoomUp();
            }).AddTo(_compositeDisposable);

            _steamResultScreen.ImageButtonRetry.OnPointerExit.Subscribe(_ =>
            {
                _selectedResultScreenButtonModel.Deselect();
                _steamResultScreen.ImageButtonRetry.ZoomDown();
            }).AddTo(_compositeDisposable);

            _steamResultScreen.ImageButtonTitle.OnPointerExit.Subscribe(_ =>
            {
                _selectedResultScreenButtonModel.Deselect();
                _steamResultScreen.ImageButtonTitle.ZoomDown();
            }).AddTo(_compositeDisposable);

            _steamResultScreen.ImageButtonRecords.OnPointerExit.Subscribe(_ =>
            {
                _selectedResultScreenButtonModel.Deselect();
                _steamResultScreen.ImageButtonRecords.ZoomDown();
            }).AddTo(_compositeDisposable);

            _steamResultScreen.ImageButtonQuit.OnPointerExit.Subscribe(_ =>
            {
                _selectedResultScreenButtonModel.Deselect();
                _steamResultScreen.ImageButtonQuit.ZoomDown();
            }).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}