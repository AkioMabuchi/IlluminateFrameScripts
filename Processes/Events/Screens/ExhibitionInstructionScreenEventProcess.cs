using System;
using Models.ScreenButtons;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views.Screens;

namespace Processes.Events.Screens
{
    public class ExhibitionInstructionScreenEventProcess : IInitializable, IDisposable
    {
        private readonly SelectedInstructionScreenButtonModel _selectedInstructionScreenButtonModel;

        private readonly CompositeDisposable _compositeDisposable = new();

        [Inject]
        public ExhibitionInstructionScreenEventProcess(SelectedInstructionScreenButtonModel selectedInstructionScreenButtonModel)
        {
            _selectedInstructionScreenButtonModel = selectedInstructionScreenButtonModel;
        }

        public void Initialize()
        {

        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}