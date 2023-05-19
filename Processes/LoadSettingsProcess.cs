using Cysharp.Threading.Tasks;
using Steamworks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using VContainer;

namespace Processes
{
    public class LoadSettingsProcess
    {
        private readonly ChangeMusicVolumeProcess _changeMusicVolumeProcess;
        private readonly ChangeSoundVolumeProcess _changeSoundVolumeProcess;
        private readonly ChangeResolutionProcess _changeResolutionProcess;

        [Inject]
        public LoadSettingsProcess(ChangeMusicVolumeProcess changeMusicVolumeProcess, ChangeSoundVolumeProcess changeSoundVolumeProcess, ChangeResolutionProcess changeResolutionProcess)
        {
            _changeMusicVolumeProcess = changeMusicVolumeProcess;
            _changeSoundVolumeProcess = changeSoundVolumeProcess;
            _changeResolutionProcess = changeResolutionProcess;
        }
        public async UniTask LoadSettings()
        {
            switch (SteamApps.GetCurrentGameLanguage())
            {
                case "english":
                {
                    LocalizationSettings.SelectedLocale = Locale.CreateLocale("en");
                    break;
                }
                case "japanese":
                {
                    LocalizationSettings.SelectedLocale = Locale.CreateLocale("ja");
                    break;
                }
            }

            await LocalizationSettings.InitializationOperation;

            if (SteamUserStats.RequestCurrentStats())
            {

                if (SteamUserStats.GetStat("settingsMusicVolume", out float musicVolume))
                {
                    _changeMusicVolumeProcess.ChangeMusicVolume(musicVolume);
                }

                if (SteamUserStats.GetStat("settingsSoundVolume", out float soundVolume))
                {
                    _changeSoundVolumeProcess.ChangeSoundVolume(soundVolume);
                }

                if (SteamUserStats.GetStat("settingsResolution", out int resolutionCode))
                {
                    _changeResolutionProcess.ChangeResolution(resolutionCode);
                }
            }
        }
    }
}