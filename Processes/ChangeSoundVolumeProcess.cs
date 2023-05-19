using Steamworks;
using VContainer;
using Views;

namespace Processes
{
    public class ChangeSoundVolumeProcess
    {
        private readonly SoundPlayer _soundPlayer;
        private readonly SettingsScreen _settingsScreen;

        [Inject]
        public ChangeSoundVolumeProcess(SoundPlayer soundPlayer, SettingsScreen settingsScreen)
        {
            _soundPlayer = soundPlayer;
            _settingsScreen = settingsScreen;
        }

        public void ChangeSoundVolume(float soundVolume)
        {
            _soundPlayer.ChangeVolume(soundVolume);
            _settingsScreen.ChangeSliderValueSound(soundVolume);
        }

        public void SetStatSoundVolume(float soundVolume)
        {
            SteamUserStats.SetStat("settingsSoundVolume", soundVolume);
            SteamUserStats.StoreStats();
        }
    }
}