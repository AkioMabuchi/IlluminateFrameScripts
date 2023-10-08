using System;
using Models.ScreenButtons;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views.Screens;
using Views.Screens.Prior;

namespace Processes.Events.Screens
{
    public class SteamInstructionScreenEventProcess : IInitializable, IDisposable
    {
        private readonly SelectedInstructionScreenButtonModel _selectedInstructionScreenButtonModel;

        private readonly Footer _footer;
        private readonly SteamInstructionScreen _steamInstructionScreen;

        private readonly CompositeDisposable _compositeDisposable = new();

        [Inject]
        public SteamInstructionScreenEventProcess(
            SelectedInstructionScreenButtonModel selectedInstructionScreenButtonModel,
            Footer footer, SteamInstructionScreen steamInstructionScreen)
        {
            _selectedInstructionScreenButtonModel = selectedInstructionScreenButtonModel;
            _footer = footer;
            _steamInstructionScreen = steamInstructionScreen;
        }

        public void Initialize()
        {
            _steamInstructionScreen.OnPointerEnterImageButtonPage.Subscribe(index =>
            {
                _selectedInstructionScreenButtonModel.Select(index);
                _footer.RenderText();
            }).AddTo(_compositeDisposable);

            _steamInstructionScreen.OnPointerExitImageButtonPage.Subscribe(_ =>
            {
                _selectedInstructionScreenButtonModel.Deselect();
                _footer.RenderText();
            }).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}