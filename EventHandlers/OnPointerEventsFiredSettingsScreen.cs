using Enums;
using Models;
using Processes;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnPointerEventsFiredSettingsScreen : IInitializable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly CurrentResolutionCodeModel _currentResolutionCodeModel;
        private readonly SettingsScreen _settingsScreen;
        
        private readonly SelectResolutionSettingScreenProcess _selectResolutionSettingScreenProcess;
        
        private readonly ChangeMusicVolumeProcess _changeMusicVolumeProcess;
        private readonly ChangeSoundVolumeProcess _changeSoundVolumeProcess;

        [Inject]
        public OnPointerEventsFiredSettingsScreen(GameStateModel gameStateModel,
            CurrentResolutionCodeModel currentResolutionCodeModel, SettingsScreen settingsScreen,
            SelectResolutionSettingScreenProcess selectResolutionSettingScreenProcess,
            ChangeMusicVolumeProcess changeMusicVolumeProcess, ChangeSoundVolumeProcess changeSoundVolumeProcess)
        {
            _gameStateModel = gameStateModel;
            _currentResolutionCodeModel = currentResolutionCodeModel;
            _settingsScreen = settingsScreen;

            _selectResolutionSettingScreenProcess = selectResolutionSettingScreenProcess;

            _changeMusicVolumeProcess = changeMusicVolumeProcess;
            _changeSoundVolumeProcess = changeSoundVolumeProcess;
        }

        public void Initialize()
        {
            _settingsScreen.OnChangedSliderMusicValue.Subscribe(value =>
            {
                _changeMusicVolumeProcess.ChangeMusicVolume(value);
            });

            _settingsScreen.OnChangedSliderSoundValue.Subscribe(value =>
            {
                _changeSoundVolumeProcess.ChangeSoundVolume(value);
            });

            _settingsScreen.OnPointerUpSliderMusic.Subscribe(musicVolume =>
            {
                _changeMusicVolumeProcess.SetStatMusicVolume(musicVolume);
            });

            _settingsScreen.OnPointerUpSliderSound.Subscribe(soundVolume =>
            {
                _changeSoundVolumeProcess.SetStatSoundVolume(soundVolume);
            });

            _settingsScreen.OnPointerEnterImageButtonResolution.Subscribe(resolutionCode =>
            {
                if (_gameStateModel.GameStateName != GameStateName.Settings ||
                    _currentResolutionCodeModel.CurrentResolutionCode == resolutionCode)
                {
                    return;
                }
                
                _selectResolutionSettingScreenProcess.SelectProcess(resolutionCode);
                
            });

            _settingsScreen.OnPointerExitImageButtonResolution.Subscribe(resolutionCode =>
            {
                _selectResolutionSettingScreenProcess.DeselectProcess(resolutionCode);
            });
        }
    }
}