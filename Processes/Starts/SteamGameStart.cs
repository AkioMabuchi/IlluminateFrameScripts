using System;
using System.Threading;
using Consts;
using Cysharp.Threading.Tasks;
using Enums;
using Models;
using Models.ScreenButtons;
using Steamworks;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Views;
using Views.Banners;
using Views.Screens;

namespace Processes.Starts
{
    public class SteamGameStartProcess : IAsyncStartable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly SelectedSettingsScreenButtonModel _selectedSettingsScreenButtonModel;

        private readonly SteamModel _steamModel;

        private readonly Localizer _localizer;
        
        private readonly BlackScreen _blackScreen;
        private readonly TitleScreen _titleScreen;
        private readonly SelectFrameSizeScreen _selectFrameSizeScreen;
        private readonly SteamInstructionScreen _steamInstructionScreen;
        private readonly SteamSettingsScreen _steamSettingsScreen;
        private readonly RecordsScreen _recordsScreen;
        private readonly StatisticsScreen _statisticsScreen;
        private readonly SteamResultScreen _steamResultScreen;

        private readonly ClosedBanner _closedBanner;
        private readonly ExterminatedBanner _exterminatedBanner;
        private readonly FinishedBanner _finishedBanner;
        
        private readonly MusicPlayer _musicPlayer;
        private readonly SoundPlayer _soundPlayer;

        [Inject]
        public SteamGameStartProcess(GameStateModel gameStateModel,
            SelectedSettingsScreenButtonModel selectedSettingsScreenButtonModel, SteamModel steamModel,
            Localizer localizer, BlackScreen blackScreen, TitleScreen titleScreen,
            SelectFrameSizeScreen selectFrameSizeScreen, SteamInstructionScreen steamInstructionScreen,
            SteamSettingsScreen steamSettingsScreen, RecordsScreen recordsScreen, StatisticsScreen statisticsScreen,
            SteamResultScreen steamResultScreen, ClosedBanner closedBanner, ExterminatedBanner exterminatedBanner,
            FinishedBanner finishedBanner, MusicPlayer musicPlayer, SoundPlayer soundPlayer)
        {
            _gameStateModel = gameStateModel;
            _selectedSettingsScreenButtonModel = selectedSettingsScreenButtonModel;

            _steamModel = steamModel;

            _localizer = localizer;

            _blackScreen = blackScreen;
            _titleScreen = titleScreen;
            _selectFrameSizeScreen = selectFrameSizeScreen;
            _steamInstructionScreen = steamInstructionScreen;
            _steamSettingsScreen = steamSettingsScreen;
            _recordsScreen = recordsScreen;
            _statisticsScreen = statisticsScreen;
            _steamResultScreen = steamResultScreen;

            _closedBanner = closedBanner;
            _exterminatedBanner = exterminatedBanner;
            _finishedBanner = finishedBanner;

            _musicPlayer = musicPlayer;
            _soundPlayer = soundPlayer;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1), cancellationToken: cancellation);
            if (SteamAPI.Init())
            {
                _steamModel.Initialize();

                // -------------------- Load Settings Process --------------------

                if (ES3.KeyExists(SaveKey.Music))
                {
                    var musicVolume = ES3.Load<float>(SaveKey.Music);
                    _steamSettingsScreen.SliderMusic.value = musicVolume;
                    _musicPlayer.ChangeVolume(musicVolume);
                }

                if (ES3.KeyExists(SaveKey.Sound))
                {
                    var soundVolume = ES3.Load<float>(SaveKey.Sound);
                    _steamSettingsScreen.SliderSound.value = soundVolume;
                    _soundPlayer.ChangeVolume(soundVolume);
                }
                
                var resolutionSize = ResolutionSize.Size1920X1080;
                if (ES3.KeyExists(SaveKey.Resolution))
                {
                    resolutionSize = ES3.Load<ResolutionSize>(SaveKey.Resolution);
                }

                switch (resolutionSize)
                {
                    case ResolutionSize.Size960X540:
                    {
                        _steamSettingsScreen.ImageButtonResolution960X540.ChangeColorCurrent();
                        Screen.SetResolution(960, 540, FullScreenMode.FullScreenWindow);
                        break;
                    }
                    case ResolutionSize.Size1280X720:
                    {
                        _steamSettingsScreen.ImageButtonResolution1280X720.ChangeColorCurrent();
                        Screen.SetResolution(1280, 720, FullScreenMode.FullScreenWindow);
                        break;
                    }
                    case ResolutionSize.Size1920X1080:
                    {
                        _steamSettingsScreen.ImageButtonResolution1920X1080.ChangeColorCurrent();
                        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
                        break;
                    }
                    case ResolutionSize.Size2560X1440:
                    {
                        _steamSettingsScreen.ImageButtonResolution2560X1440.ChangeColorCurrent();
                        Screen.SetResolution(2560, 1440, FullScreenMode.FullScreenWindow);
                        break;
                    }
                    case ResolutionSize.Size3840X2160:
                    {
                        _steamSettingsScreen.ImageButtonResolution3840X2160.ChangeColorCurrent();
                        Screen.SetResolution(3840, 2160, FullScreenMode.FullScreenWindow);
                        break;
                    }
                }

                var renderQuality = RenderQuality.Medium;
                if (ES3.KeyExists(SaveKey.RenderQuality))
                {
                    renderQuality = ES3.Load<RenderQuality>(SaveKey.RenderQuality);
                }

                switch (renderQuality)
                {
                    case RenderQuality.Low:
                    {
                        _steamSettingsScreen.ImageButtonQualityLow.ChangeColorCurrent();
                        QualitySettings.SetQualityLevel(0);
                        break;
                    }
                    case RenderQuality.Medium:
                    {
                        _steamSettingsScreen.ImageButtonQualityMedium.ChangeColorCurrent();
                        QualitySettings.SetQualityLevel(1);
                        break;
                    }
                    case RenderQuality.High:
                    {
                        _steamSettingsScreen.ImageButtonQualityHigh.ChangeColorCurrent();
                        QualitySettings.SetQualityLevel(2);
                        break;
                    }
                    case RenderQuality.VeryHigh:
                    {
                        _steamSettingsScreen.ImageButtonQualityVeryHigh.ChangeColorCurrent();
                        QualitySettings.SetQualityLevel(3);
                        break;
                    }
                }

                _selectedSettingsScreenButtonModel.SetCurrentResolution(resolutionSize);
                _selectedSettingsScreenButtonModel.SetCurrentRenderQuality(renderQuality);

                // -------------------- Localize Process --------------------
                
                switch (SteamApps.GetCurrentGameLanguage())
                {
                    case "english":
                    {
                        _localizer.SetLanguage(LanguageName.English);
                        break;
                    }
                    case "japanese":
                    {
                        _localizer.SetLanguage(LanguageName.Japanese);
                        break;
                    }
                }
                
                _titleScreen.Localize();
                _selectFrameSizeScreen.Localize();
                _steamInstructionScreen.Localize();
                _steamSettingsScreen.Localize();
                _recordsScreen.Localize();
                _statisticsScreen.Localize();
                _steamResultScreen.Localize();
                _closedBanner.Localize();
                _exterminatedBanner.Localize();
                _finishedBanner.Localize();
                
                // -------------------- Start Game Process --------------------
                
                _blackScreen.FadeOut();

                await UniTask.Delay(TimeSpan.FromSeconds(1.0), cancellationToken: cancellation);

                _gameStateModel.SetGameStateName(GameStateName.Title);
                _titleScreen.ShowUp();

                await UniTask.Delay(TimeSpan.FromSeconds(2.0), cancellationToken: cancellation);
                _musicPlayer.PlayMusic();
            }
            else
            {
                _gameStateModel.SetGameStateName(GameStateName.Invalid);
                _blackScreen.ChangeMessage(BlackScreenMessageType.NoSteam);
            }
        }
    }
}