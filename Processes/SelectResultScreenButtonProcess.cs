using Enums;
using Models;
using VContainer;
using Views;

namespace Processes
{
    public class SelectResultScreenButtonProcess
    {
        private readonly SelectedResultScreenButtonModel _selectedResultScreenButtonModel;
        private readonly ResultScreen _resultScreen;

        [Inject]
        public SelectResultScreenButtonProcess(SelectedResultScreenButtonModel selectedResultScreenButtonModel,
            ResultScreen resultScreen)
        {
            _selectedResultScreenButtonModel = selectedResultScreenButtonModel;
            _resultScreen = resultScreen;
        }

        public void SelectProcess(ResultScreenButtonName resultScreenButtonName)
        {
            _selectedResultScreenButtonModel.Select(resultScreenButtonName);

            _resultScreen.ZoomUpButtons(_selectedResultScreenButtonModel.SelectedResultScreenButtonName);
        }

        public void DeselectProcess(ResultScreenButtonName resultScreenButtonName)
        {
            _selectedResultScreenButtonModel.Deselect(resultScreenButtonName);

            _resultScreen.ZoomUpButtons(_selectedResultScreenButtonModel.SelectedResultScreenButtonName);
        }

        public void DecideProcess()
        {
            switch (_selectedResultScreenButtonModel.SelectedResultScreenButtonName)
            {
                case ResultScreenButtonName.Retry:
                {
                    break;
                }
                case ResultScreenButtonName.Title:
                {
                    break;
                }
                case ResultScreenButtonName.Records:
                {
                    break;
                }
                case ResultScreenButtonName.Quit:
                {
                    break;
                }
            }
            
            _selectedResultScreenButtonModel.Clear();
        }
    }
}