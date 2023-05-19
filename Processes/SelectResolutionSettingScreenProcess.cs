using Models;
using UnityEngine;
using VContainer;
using Views;

namespace Processes
{
    public class SelectResolutionSettingScreenProcess
    {
        private readonly SelectedResolutionButtonModel _selectedResolutionButtonModel;

        private readonly SettingsScreen _settingsScreen;
        
        private readonly ChangeResolutionProcess _changeResolutionProcess;
        
        [Inject]
        public SelectResolutionSettingScreenProcess(SelectedResolutionButtonModel selectedResolutionButtonModel,
            SettingsScreen settingsScreen, ChangeResolutionProcess changeResolutionProcess)
        {
            _selectedResolutionButtonModel = selectedResolutionButtonModel;

            _settingsScreen = settingsScreen;
            
            _changeResolutionProcess = changeResolutionProcess;
        }

        public void SelectProcess(int resolutionCode)
        {
            _selectedResolutionButtonModel.Select(resolutionCode);
            
            _settingsScreen.ChangeImageButtonResolutionColors(_selectedResolutionButtonModel
                .SelectedResolutionButtonCode);
        }

        public void DeselectProcess(int resolutionCode)
        {
            _selectedResolutionButtonModel.Deselect(resolutionCode);
            
            _settingsScreen.ChangeImageButtonResolutionColors(_selectedResolutionButtonModel
                .SelectedResolutionButtonCode);
        }

        public void DecideProcess()
        {
            var nullableResolutionCode = _selectedResolutionButtonModel.SelectedResolutionButtonCode;
            if (nullableResolutionCode.HasValue)
            {
                var resolutionCode = nullableResolutionCode.Value;
                _changeResolutionProcess.ChangeResolution(resolutionCode);
            }

            _selectedResolutionButtonModel.Clear();

            _settingsScreen.ChangeImageButtonResolutionColors(null);
        }
    }
}