using Steamworks;
using VContainer;
using Views;

namespace Processes
{
    public class ChangeMusicVolumeProcess
    {
        private readonly MusicPlayer _musicPlayer;
        private readonly SettingsScreen _settingsScreen;

        [Inject]
        public ChangeMusicVolumeProcess(MusicPlayer musicPlayer, SettingsScreen settingsScreen)
        {
            _musicPlayer = musicPlayer;
            _settingsScreen = settingsScreen;
        }

        public void ChangeMusicVolume(float musicVolume)
        {
            _musicPlayer.ChangeVolume(musicVolume);
            _settingsScreen.ChangeSliderValueMusic(musicVolume);
        }

        public void SetStatMusicVolume(float musicVolume)
        {
            SteamUserStats.SetStat("settingsMusicVolume", musicVolume);
            SteamUserStats.StoreStats();
        }
    }
}