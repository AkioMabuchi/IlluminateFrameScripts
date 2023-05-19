using Enums;
using Models;
using VContainer;
using Views;

namespace Processes
{
    public class SelectRecordsScreenButtonProcess
    {
        private readonly SelectedRecordsScreenButtonModel _selectedRecordsScreenButtonModel;
        private readonly RecordsScreen _recordsScreen;

        [Inject]
        public SelectRecordsScreenButtonProcess(SelectedRecordsScreenButtonModel selectedRecordsScreenButtonModel,
            RecordsScreen recordsScreen)
        {
            _selectedRecordsScreenButtonModel = selectedRecordsScreenButtonModel;
            _recordsScreen = recordsScreen;
        }

        public void SelectProcess(RecordsScreenButtonName recordsScreenButtonName)
        {
            _selectedRecordsScreenButtonModel.Select(recordsScreenButtonName);

            _recordsScreen.ZoomUpButtons(_selectedRecordsScreenButtonModel.SelectedRecordsScreenButtonName);
        }

        public void DeselectProcess(RecordsScreenButtonName recordsScreenButtonName)
        {
            _selectedRecordsScreenButtonModel.Deselect(recordsScreenButtonName);
            
            _recordsScreen.ZoomUpButtons(_selectedRecordsScreenButtonModel.SelectedRecordsScreenButtonName);
        }

        public void DecideProcess()
        {
            switch (_selectedRecordsScreenButtonModel.SelectedRecordsScreenButtonName)
            {
                case RecordsScreenButtonName.Small:
                {
                    break;
                }
                case RecordsScreenButtonName.Medium:
                {
                    break;
                }
                case RecordsScreenButtonName.Large:
                {
                    break;
                }
                case RecordsScreenButtonName.Global:
                {
                    break;
                }
                case RecordsScreenButtonName.Friends:
                {
                    break;
                }
            }
        }
    }
}