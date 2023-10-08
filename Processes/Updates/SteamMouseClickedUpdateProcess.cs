using System.Collections.Generic;
using Classes;
using Classes.Statics;
using Consts;
using Cysharp.Threading.Tasks;
using Enums;
using Enums.ScreenButtonNames;
using Enums.ScreenButtonNames.Prior;
using Interfaces.Processes;
using Models;
using Models.ScreenButtons;
using Models.ScreenButtons.Prior;
using Steamworks;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;
using Views;
using Views.Screens;
using Views.Screens.Prior;

namespace Processes.Updates
{
    public class SteamMouseClickedUpdateProcess : ITickable
    {
        private readonly GameStateModel _gameStateModel;

        private readonly SelectedHeaderButtonModel _selectedHeaderButtonModel;
        private readonly SelectedTitleScreenButtonModel _selectedTitleScreenButtonModel;
        private readonly SelectedSelectFrameSizeScreenButtonModel _selectedSelectFrameSizeScreenButtonModel;
        private readonly SelectedInstructionScreenButtonModel _selectedInstructionScreenButtonModel;
        private readonly SelectedSettingsScreenButtonModel _selectedSettingsScreenButtonModel;
        private readonly SelectedRecordsScreenButtonModel _selectedRecordsScreenButtonModel;
        private readonly SelectedResultScreenButtonModel _selectedResultScreenButtonModel;

        private readonly Header _header;
        private readonly Footer _footer;

        private readonly BackScreen _backScreen;
        private readonly TitleScreen _titleScreen;
        private readonly SelectFrameSizeScreen _selectFrameSizeScreen;
        private readonly SteamInstructionScreen _steamInstructionScreen;
        private readonly SteamSettingsScreen _steamSettingsScreen;
        private readonly RecordsScreen _recordsScreen;
        private readonly SteamAchievementsScreen _steamAchievementsScreen;
        private readonly StatisticsScreen _statisticsScreen;
        private readonly CreditsScreen _creditsScreen;
        private readonly LicensesScreen _licensesScreen;
        
        private readonly SteamResultScreen _steamResultScreen;
        
        private readonly SoundPlayer _soundPlayer;

        private readonly SteamRecordsReceiver _steamRecordsReceiver;

        private readonly StartMainGameProcess _startMainGameProcess;

        private readonly IQuitGameProcess _quitGameProcess;

        private readonly RetryMainGameProcess _retryMainGameProcess;
        private readonly ReturnToTitleProcess _returnToTitleProcess;
        private readonly RotateCurrentTileProcess _rotateCurrentTileProcess;
        private readonly PutCurrentTileProcess _putCurrentTileProcess;
        private readonly TutorialProcess _tutorialProcess;
        
        [Inject]
        public SteamMouseClickedUpdateProcess(GameStateModel gameStateModel,
            SelectedHeaderButtonModel selectedHeaderButtonModel,
            SelectedTitleScreenButtonModel selectedTitleScreenButtonModel,
            SelectedSelectFrameSizeScreenButtonModel selectedSelectFrameSizeScreenButtonModel,
            SelectedInstructionScreenButtonModel selectedInstructionScreenButtonModel,
            SelectedSettingsScreenButtonModel selectedSettingsScreenButtonModel,
            SelectedRecordsScreenButtonModel selectedRecordsScreenButtonModel,
            SelectedResultScreenButtonModel selectedResultScreenButtonModel,
            Header header, Footer footer, BackScreen backScreen, TitleScreen titleScreen,
            SelectFrameSizeScreen selectFrameSizeScreen,
            SteamInstructionScreen steamInstructionScreen, SteamSettingsScreen steamSettingsScreen,
            RecordsScreen recordsScreen,
            SteamAchievementsScreen steamAchievementsScreen, StatisticsScreen statisticsScreen, CreditsScreen creditsScreen,
            LicensesScreen licensesScreen, SteamResultScreen steamResultScreen, SoundPlayer soundPlayer,
            SteamRecordsReceiver steamRecordsReceiver,
            StartMainGameProcess startMainGameProcess,
            IQuitGameProcess quitGameProcess, RetryMainGameProcess retryMainGameProcess,
            ReturnToTitleProcess returnToTitleProcess, RotateCurrentTileProcess rotateCurrentTileProcess,
            PutCurrentTileProcess putCurrentTileProcess, TutorialProcess tutorialProcess)
        {
            _gameStateModel = gameStateModel;

            _selectedHeaderButtonModel = selectedHeaderButtonModel;
            _selectedTitleScreenButtonModel = selectedTitleScreenButtonModel;
            _selectedSelectFrameSizeScreenButtonModel = selectedSelectFrameSizeScreenButtonModel;
            _selectedInstructionScreenButtonModel = selectedInstructionScreenButtonModel;
            _selectedSettingsScreenButtonModel = selectedSettingsScreenButtonModel;
            _selectedRecordsScreenButtonModel = selectedRecordsScreenButtonModel;
            _selectedResultScreenButtonModel = selectedResultScreenButtonModel;

            _header = header;
            _footer = footer;

            _backScreen = backScreen;
            _titleScreen = titleScreen;
            _selectFrameSizeScreen = selectFrameSizeScreen;
            _steamInstructionScreen = steamInstructionScreen;
            _steamSettingsScreen = steamSettingsScreen;
            _recordsScreen = recordsScreen;
            _steamAchievementsScreen = steamAchievementsScreen;
            _statisticsScreen = statisticsScreen;
            _creditsScreen = creditsScreen;
            _licensesScreen = licensesScreen;
            _steamResultScreen = steamResultScreen;

            _soundPlayer = soundPlayer;
            _steamRecordsReceiver = steamRecordsReceiver;

            _startMainGameProcess = startMainGameProcess;
            _quitGameProcess = quitGameProcess;
            _retryMainGameProcess = retryMainGameProcess;
            _returnToTitleProcess = returnToTitleProcess;
            _rotateCurrentTileProcess = rotateCurrentTileProcess;
            _putCurrentTileProcess = putCurrentTileProcess;
            _tutorialProcess = tutorialProcess;
        }

        public void Tick()
        {
            if (Mouse.current == null)
            {
                return;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                switch (_gameStateModel.GameStateName)
                {
                    case GameStateName.Title:
                    {
                        switch (_selectedTitleScreenButtonModel.SelectedTitleScreenButtonName)
                        {
                            case TitleScreenButtonName.GameStart:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.SelectFrameSize);
                                _header.Show();
                                _header.RenderText();
                                _footer.Show();
                                _titleScreen.Hide();
                                _selectFrameSizeScreen.Show();
                                _soundPlayer.PlaySelectSound();

                                break;
                            }
                            case TitleScreenButtonName.Tutorial:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.Tutorial);
                                
                                _titleScreen.Hide();
                                _tutorialProcess.AsyncStartTutorial().Forget();
                                _soundPlayer.PlaySelectSound();
                                break;
                            }
                            case TitleScreenButtonName.Instruction:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.Instruction);
                                _header.Show();
                                _header.RenderText();
                                _footer.Show();
                                _titleScreen.Hide();
                                _steamInstructionScreen.Show();
                                _steamInstructionScreen.ChangePage(0);
                                _soundPlayer.PlaySelectSound();

                                break;
                            }
                            case TitleScreenButtonName.Settings:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.Settings);
                                _header.Show();
                                _header.RenderText();
                                _footer.Show();
                                _titleScreen.Hide();
                                _steamSettingsScreen.Show();
                                _soundPlayer.PlaySelectSound();

                                break;
                            }
                            case TitleScreenButtonName.Records:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.Records);
                                _header.Show();
                                _header.RenderText();
                                _footer.Show();
                                _titleScreen.Hide();
                                _recordsScreen.Show();
                                _recordsScreen.RecordsBoardMessage.RenderLoading();
                                _recordsScreen.EnhancedScrollerRecords.ClearRecords();
                                _recordsScreen.EnhancedScrollerRecords.Render();
                                _steamRecordsReceiver.SetFrameSize(FrameSize.Small);
                                _steamRecordsReceiver.SetLeaderBoardDataRequest(ELeaderboardDataRequest
                                    .k_ELeaderboardDataRequestGlobal);
                                _steamRecordsReceiver.ReceiveRecords();
                                _soundPlayer.PlaySelectSound();
                                _recordsScreen.SetHeaderTextFrameSize(FrameSize.Small);
                                _recordsScreen.SetHeaderTextLeaderboardDataRequest(ELeaderboardDataRequest
                                    .k_ELeaderboardDataRequestGlobal);
                                _recordsScreen.RenderHeaderText();
                                break;
                            }
                            case TitleScreenButtonName.Achievements:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.Achievements);
                                _header.Show();
                                _header.RenderText();
                                _footer.Show();
                                _titleScreen.Hide();
                                
                                _steamAchievementsScreen.EnhancedScrollerAchievements.ClearAchievements();

                                var achievements = new List<CellViewAchievementParamsGroupAchievement>();
            
                                foreach (var (key, value) in Achievements.Dictionary)
                                {
                                    if (SteamUserStats.GetAchievement(value, out var achieved))
                                    {
                                        achievements.Add(new CellViewAchievementParamsGroupAchievement
                                        {
                                            achieved = achieved,
                                            achievement = key,
                                            achievementName =
                                                SteamUserStats.GetAchievementDisplayAttribute(value, "name"),
                                            achievementDetail =
                                                SteamUserStats.GetAchievementDisplayAttribute(value, "desc")
                                        });
                                    }
                                }
            
                                _steamAchievementsScreen.EnhancedScrollerAchievements.SetAchievements(achievements);
                                _steamAchievementsScreen.EnhancedScrollerAchievements.Reload();
                                _steamAchievementsScreen.FadeIn();
                                _soundPlayer.PlaySelectSound();

                                break;
                            }
                            case TitleScreenButtonName.Statistics:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.Statistics);

                                if (SteamUserStats.GetStat(SteamStats.FinishedCountSmallFrame,
                                        out int finishedCountSmallFrame))
                                {
                                    _statisticsScreen.ChangeSmallFrameFinishedCount(finishedCountSmallFrame);
                                }

                                if (SteamUserStats.GetStat(SteamStats.FinishedCountMediumFrame,
                                        out int finishedCountMediumFrame))
                                {
                                    _statisticsScreen.ChangeMediumFrameFinishedCount(finishedCountMediumFrame);
                                }

                                if (SteamUserStats.GetStat(SteamStats.FinishedCountLargeFrame,
                                        out int finishedCountLargeFrame))
                                {
                                    _statisticsScreen.ChangeLargeFrameFinishedCount(finishedCountLargeFrame);
                                }

                                if (SteamUserStats.GetStat(SteamStats.HighScoreSmallFrame,
                                        out int highScoreSmallFrame))
                                {
                                    _statisticsScreen.ChangeSmallFrameHighScore(highScoreSmallFrame);
                                }

                                if (SteamUserStats.GetStat(SteamStats.HighScoreMediumFrame,
                                        out int highScoreMediumFrame))
                                {
                                    _statisticsScreen.ChangeMediumFrameHighScore(highScoreMediumFrame);
                                }

                                if (SteamUserStats.GetStat(SteamStats.HighScoreLargeFrame,
                                        out int highScoreLargeFrame))
                                {
                                    _statisticsScreen.ChangeLargeFrameHighScore(highScoreLargeFrame);
                                }

                                if (SteamUserStats.GetStat(SteamStats.IlluminatedCountSmallFrame,
                                        out int illuminatedCountSmallFrame))
                                {
                                    _statisticsScreen.ChangeSmallFrameIlluminatedCount(illuminatedCountSmallFrame);
                                }

                                if (SteamUserStats.GetStat(SteamStats.IlluminatedCountMediumFrame,
                                        out int illuminatedCountMediumFrame))
                                {
                                    _statisticsScreen.ChangeMediumFrameIlluminatedCount(illuminatedCountMediumFrame);
                                }

                                if (SteamUserStats.GetStat(SteamStats.IlluminatedCountLargeFrame,
                                        out int illuminatedCountLargeFrame))
                                {
                                    _statisticsScreen.ChangeLargeFrameIlluminatedCount(illuminatedCountLargeFrame);
                                }

                                if (SteamUserStats.GetStat(SteamStats.LongestPathSmallFrame,
                                        out int longestPathSmallFrame))
                                {
                                    _statisticsScreen.ChangeSmallFrameLongestPath(longestPathSmallFrame);
                                }

                                if (SteamUserStats.GetStat(SteamStats.LongestPathMediumFrame,
                                        out int longestPathMediumFrame))
                                {
                                    _statisticsScreen.ChangeMediumFrameLongestPath(longestPathMediumFrame);
                                }

                                if (SteamUserStats.GetStat(SteamStats.LongestPathLargeFrame,
                                        out int longestPathLargeFrame))
                                {
                                    _statisticsScreen.ChangeLargeFrameLongestPath(longestPathLargeFrame);
                                }

                                _header.Show();
                                _header.RenderText();
                                _footer.Show();
                                _titleScreen.Hide();
                                _statisticsScreen.Show();
                                _soundPlayer.PlaySelectSound();
                                break;
                            }
                            case TitleScreenButtonName.Credits:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.Credits);
                                _header.Show();
                                _header.RenderText();
                                _footer.Show();
                                _titleScreen.Hide();
                                _creditsScreen.Show();
                                _soundPlayer.PlaySelectSound();
                                break;
                            }
                            case TitleScreenButtonName.Licences:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.Licenses);
                                _header.Show();
                                _header.RenderText();
                                _footer.Show();
                                _titleScreen.Hide();
                                _licensesScreen.Show();
                                _soundPlayer.PlaySelectSound();
                                break;
                            }
                            case TitleScreenButtonName.Quit:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.None);
                                _quitGameProcess.QuitGame();
                                break;
                            }
                        }

                        _selectedTitleScreenButtonModel.Deselect();
                        break;
                    }
                    case GameStateName.SelectFrameSize:
                    {
                        switch (_selectedSelectFrameSizeScreenButtonModel.SelectFrameSizeScreenButtonName)
                        {
                            case SelectFrameSizeScreenButtonName.Small:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.None);
                                _header.Hide();
                                _selectFrameSizeScreen.Hide();
                                _startMainGameProcess.AsyncStartMainGame(FrameSize.Small).Forget();
                                _steamRecordsReceiver.SetFrameSize(FrameSize.Small);
                                _recordsScreen.SetHeaderTextFrameSize(FrameSize.Small);
                                _recordsScreen.RenderHeaderText();
                                
                                _soundPlayer.PlayDecideSound();
                                break;
                            }
                            case SelectFrameSizeScreenButtonName.Medium:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.None);
                                _header.Hide();
                                _selectFrameSizeScreen.Hide();
                                _startMainGameProcess.AsyncStartMainGame(FrameSize.Medium).Forget();
                                _steamRecordsReceiver.SetFrameSize(FrameSize.Medium);
                                _recordsScreen.SetHeaderTextFrameSize(FrameSize.Medium);
                                _recordsScreen.RenderHeaderText();
                                _soundPlayer.PlayDecideSound();
                                break;
                            }
                            case SelectFrameSizeScreenButtonName.Large:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.None);
                                _header.Hide();
                                _selectFrameSizeScreen.Hide();
                                _startMainGameProcess.AsyncStartMainGame(FrameSize.Large).Forget();
                                _steamRecordsReceiver.SetFrameSize(FrameSize.Large);
                                _recordsScreen.SetHeaderTextFrameSize(FrameSize.Large);
                                _recordsScreen.RenderHeaderText();
                                _soundPlayer.PlayDecideSound();
                                break;
                            }
                            default:
                            {
                                switch (_selectedHeaderButtonModel.SelectedHeaderButtonName)
                                {
                                    case HeaderButtonName.Return:
                                    {
                                        _gameStateModel.SetGameStateName(GameStateName.Title);
                                        _header.Hide();
                                        _footer.Hide();
                                        _titleScreen.Show();
                                        _selectFrameSizeScreen.Hide();

                                        break;
                                    }
                                }

                                break;
                            }
                        }

                        break;
                    }
                    case GameStateName.Tutorial:
                    {
                        _tutorialProcess.AsyncPutCurrentTile().Forget();
                        break;
                    }
                    case GameStateName.Instruction:
                    {
                        if (_selectedInstructionScreenButtonModel.TryGetSelectedIndex(out var index))
                        {
                            _steamInstructionScreen.ChangePage(index);
                        }
                        else
                        {
                            switch (_selectedHeaderButtonModel.SelectedHeaderButtonName)
                            {
                                case HeaderButtonName.Return:
                                {
                                    _gameStateModel.SetGameStateName(GameStateName.Title);
                                    _header.Hide();
                                    _footer.Hide();
                                    _titleScreen.Show();
                                    _steamInstructionScreen.Hide();
                                    break;
                                }
                            }
                        }

                        break;
                    }
                    case GameStateName.Settings:
                    {
                        switch (_selectedSettingsScreenButtonModel.SelectedSettingsScreenButtonName)
                        {
                            case SettingsScreenButtonName.Resolution960X540:
                            {
                                _selectedSettingsScreenButtonModel.SetCurrentResolution(ResolutionSize.Size960X540);
                                _steamSettingsScreen.SelectResolutionButtons(ResolutionSize.Size960X540);
                                Screen.SetResolution(960, 540, FullScreenMode.FullScreenWindow);
                                _selectedSettingsScreenButtonModel.Deselect();
                                ES3.Save(SaveKey.Resolution, ResolutionSize.Size960X540);
                                break;
                            }
                            case SettingsScreenButtonName.Resolution1280X720:
                            {
                                _selectedSettingsScreenButtonModel.SetCurrentResolution(ResolutionSize.Size1280X720);
                                _steamSettingsScreen.SelectResolutionButtons(ResolutionSize.Size1280X720);
                                Screen.SetResolution(1280, 720, FullScreenMode.FullScreenWindow);
                                _selectedSettingsScreenButtonModel.Deselect();
                                ES3.Save(SaveKey.Resolution, ResolutionSize.Size1280X720);
                                break;
                            }
                            case SettingsScreenButtonName.Resolution1920X1080:
                            {
                                _selectedSettingsScreenButtonModel.SetCurrentResolution(ResolutionSize.Size1920X1080);
                                _steamSettingsScreen.SelectResolutionButtons(ResolutionSize.Size1920X1080);
                                Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
                                _selectedSettingsScreenButtonModel.Deselect();
                                ES3.Save(SaveKey.Resolution, ResolutionSize.Size1920X1080);
                                break;
                            }
                            case SettingsScreenButtonName.Resolution2560X1440:
                            {
                                _selectedSettingsScreenButtonModel.SetCurrentResolution(ResolutionSize.Size2560X1440);
                                _steamSettingsScreen.SelectResolutionButtons(ResolutionSize.Size2560X1440);
                                Screen.SetResolution(2560, 1440, FullScreenMode.FullScreenWindow);
                                _selectedSettingsScreenButtonModel.Deselect();
                                ES3.Save(SaveKey.Resolution, ResolutionSize.Size2560X1440);
                                break;
                            }
                            case SettingsScreenButtonName.Resolution3840X2160:
                            {
                                _selectedSettingsScreenButtonModel.SetCurrentResolution(ResolutionSize.Size3840X2160);
                                _steamSettingsScreen.SelectResolutionButtons(ResolutionSize.Size3840X2160);
                                Screen.SetResolution(3840, 2160, FullScreenMode.FullScreenWindow);
                                _selectedSettingsScreenButtonModel.Deselect();
                                ES3.Save(SaveKey.Resolution, ResolutionSize.Size3840X2160);
                                break;
                            }
                            case SettingsScreenButtonName.QualityLow:
                            {
                                _selectedSettingsScreenButtonModel.SetCurrentRenderQuality(RenderQuality.Low);
                                _steamSettingsScreen.SelectRenderQualityButtons(RenderQuality.Low);
                                QualitySettings.SetQualityLevel(0);
                                _selectedSettingsScreenButtonModel.Deselect();
                                ES3.Save(SaveKey.RenderQuality, RenderQuality.Low);
                                break;
                            }
                            case SettingsScreenButtonName.QualityMedium:
                            {
                                _selectedSettingsScreenButtonModel.SetCurrentRenderQuality(RenderQuality.Medium);
                                _steamSettingsScreen.SelectRenderQualityButtons(RenderQuality.Medium);
                                QualitySettings.SetQualityLevel(1);
                                _selectedSettingsScreenButtonModel.Deselect();
                                ES3.Save(SaveKey.RenderQuality, RenderQuality.Medium);
                                break;
                            }
                            case SettingsScreenButtonName.QualityHigh:
                            {
                                _selectedSettingsScreenButtonModel.SetCurrentRenderQuality(RenderQuality.High);
                                _steamSettingsScreen.SelectRenderQualityButtons(RenderQuality.High);
                                QualitySettings.SetQualityLevel(2);
                                _selectedSettingsScreenButtonModel.Deselect();
                                ES3.Save(SaveKey.RenderQuality, RenderQuality.High);
                                break;
                            }
                            case SettingsScreenButtonName.QualityVeryHigh:
                            {
                                _selectedSettingsScreenButtonModel.SetCurrentRenderQuality(RenderQuality.VeryHigh);
                                _steamSettingsScreen.SelectRenderQualityButtons(RenderQuality.VeryHigh);
                                QualitySettings.SetQualityLevel(3);
                                _selectedSettingsScreenButtonModel.Deselect();
                                ES3.Save(SaveKey.RenderQuality, RenderQuality.VeryHigh);
                                break;
                            }
                            default:
                            {
                                switch (_selectedHeaderButtonModel.SelectedHeaderButtonName)
                                {
                                    case HeaderButtonName.Return:
                                    {
                                        _gameStateModel.SetGameStateName(GameStateName.Title);
                                        _header.Hide();
                                        _footer.Hide();
                                        _titleScreen.Show();
                                        _steamSettingsScreen.Hide();
                                        break;
                                    }
                                }

                                break;
                            }
                        }

                        break;
                    }
                    case GameStateName.Records:
                    {
                        switch (_selectedRecordsScreenButtonModel.SelectedRecordsScreenButtonName)
                        {
                            case RecordsScreenButtonName.Small:
                            {
                                _recordsScreen.SetHeaderTextFrameSize(FrameSize.Small);
                                _recordsScreen.RenderHeaderText();
                                _recordsScreen.RecordsBoardMessage.RenderLoading();
                                _recordsScreen.EnhancedScrollerRecords.ClearRecords();
                                _recordsScreen.EnhancedScrollerRecords.Render();
                                _steamRecordsReceiver.SetFrameSize(FrameSize.Small);
                                _steamRecordsReceiver.ReceiveRecords();
                                break;
                            }
                            case RecordsScreenButtonName.Medium:
                            {
                                _recordsScreen.SetHeaderTextFrameSize(FrameSize.Medium);
                                _recordsScreen.RenderHeaderText();
                                _recordsScreen.RecordsBoardMessage.RenderLoading();
                                _recordsScreen.EnhancedScrollerRecords.ClearRecords();
                                _recordsScreen.EnhancedScrollerRecords.Render();
                                _steamRecordsReceiver.SetFrameSize(FrameSize.Medium);
                                _steamRecordsReceiver.ReceiveRecords();
                                break;
                            }
                            case RecordsScreenButtonName.Large:
                            {
                                _recordsScreen.SetHeaderTextFrameSize(FrameSize.Large);
                                _recordsScreen.RenderHeaderText();
                                _recordsScreen.RecordsBoardMessage.RenderLoading();
                                _recordsScreen.EnhancedScrollerRecords.ClearRecords();
                                _recordsScreen.EnhancedScrollerRecords.Render();
                                _steamRecordsReceiver.SetFrameSize(FrameSize.Large);
                                _steamRecordsReceiver.ReceiveRecords();
                                break;
                            }
                            case RecordsScreenButtonName.Global:
                            {
                                _recordsScreen.SetHeaderTextLeaderboardDataRequest(ELeaderboardDataRequest
                                    .k_ELeaderboardDataRequestGlobal);
                                _recordsScreen.RenderHeaderText();
                                _recordsScreen.RecordsBoardMessage.RenderLoading();
                                _recordsScreen.EnhancedScrollerRecords.ClearRecords();
                                _recordsScreen.EnhancedScrollerRecords.Render();
                                _steamRecordsReceiver.SetLeaderBoardDataRequest(ELeaderboardDataRequest
                                    .k_ELeaderboardDataRequestGlobal);
                                _steamRecordsReceiver.ReceiveRecords();
                                break;
                            }
                            case RecordsScreenButtonName.Friends:
                            {
                                _recordsScreen.SetHeaderTextLeaderboardDataRequest(ELeaderboardDataRequest
                                    .k_ELeaderboardDataRequestFriends);
                                _recordsScreen.RenderHeaderText();
                                _recordsScreen.RecordsBoardMessage.RenderLoading();
                                _recordsScreen.EnhancedScrollerRecords.ClearRecords();
                                _recordsScreen.EnhancedScrollerRecords.Render();
                                _steamRecordsReceiver.SetLeaderBoardDataRequest(ELeaderboardDataRequest
                                    .k_ELeaderboardDataRequestFriends);
                                _steamRecordsReceiver.ReceiveRecords();
                                break;
                            }
                            default:
                            {
                                switch (_selectedHeaderButtonModel.SelectedHeaderButtonName)
                                {
                                    case HeaderButtonName.Return:
                                    {
                                        _gameStateModel.SetGameStateName(GameStateName.Title);
                                        _header.Hide();
                                        _footer.Hide();
                                        _titleScreen.Show();
                                        _recordsScreen.Hide();
                                        break;
                                    }
                                }

                                break;
                            }
                        }

                        break;
                    }
                    case GameStateName.Achievements:
                    {
                        switch (_selectedHeaderButtonModel.SelectedHeaderButtonName)
                        {
                            case HeaderButtonName.Return:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.Title);
                                _header.Hide();
                                _footer.Hide();
                                _titleScreen.Show();
                                _steamAchievementsScreen.Hide();
                                break;
                            }
                        }

                        break;
                    }
                    case GameStateName.Statistics:
                    {
                        switch (_selectedHeaderButtonModel.SelectedHeaderButtonName)
                        {
                            case HeaderButtonName.Return:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.Title);
                                _header.Hide();
                                _footer.Hide();
                                _titleScreen.Show();
                                _statisticsScreen.Hide();
                                break;
                            }
                        }

                        break;
                    }
                    case GameStateName.Credits:
                    {
                        switch (_selectedHeaderButtonModel.SelectedHeaderButtonName)
                        {
                            case HeaderButtonName.Return:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.Title);
                                _header.Hide();
                                _footer.Hide();
                                _titleScreen.Show();
                                _creditsScreen.Hide();
                                break;
                            }
                        }

                        break;
                    }
                    case GameStateName.Licenses:
                    {
                        switch (_selectedHeaderButtonModel.SelectedHeaderButtonName)
                        {
                            case HeaderButtonName.Return:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.Title);
                                _header.Hide();
                                _footer.Hide();
                                _titleScreen.Show();
                                _licensesScreen.Hide();
                                break;
                            }
                        }

                        break;
                    }
                    case GameStateName.Main:
                    {
                        _putCurrentTileProcess.AsyncPutCurrentTile().Forget();
                        break;
                    }
                    case GameStateName.Result:
                    {
                        switch (_selectedResultScreenButtonModel.SelectedResultScreenButtonName)
                        {
                            case ResultScreenButtonName.Retry:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.None);
                                _backScreen.Hide();
                                _steamResultScreen.Hide();
                                _retryMainGameProcess.AsyncRetryMainGame().Forget();
                                _soundPlayer.PlayDecideSound();
                                break;
                            }
                            case ResultScreenButtonName.Title:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.None);
                                _backScreen.Hide();
                                _steamResultScreen.Hide();
                                _returnToTitleProcess.AsyncReturnToTitle().Forget();
                                _soundPlayer.PlaySelectSound();
                                break;
                            }
                            case ResultScreenButtonName.Records:
                            {
                                _gameStateModel.SetGameStateName(GameStateName.ResultRecords);
                                _header.Show();
                                _header.RenderText();
                                _footer.Show();
                                _steamResultScreen.Hide();
                                _recordsScreen.Show();
                                _recordsScreen.RecordsBoardMessage.RenderLoading();
                                _recordsScreen.EnhancedScrollerRecords.ClearRecords();
                                _recordsScreen.EnhancedScrollerRecords.Render();
                                _steamRecordsReceiver.ReceiveRecords();
                                _soundPlayer.PlaySelectSound();
                                break;
                            }
                            case ResultScreenButtonName.Quit:
                            {
                                _quitGameProcess.QuitGame();
                                break;
                            }
                        }

                        break;
                    }
                    case GameStateName.ResultRecords:
                    {
                        switch (_selectedRecordsScreenButtonModel.SelectedRecordsScreenButtonName)
                        {
                            case RecordsScreenButtonName.Small:
                            {
                                _recordsScreen.SetHeaderTextFrameSize(FrameSize.Small);
                                _recordsScreen.RenderHeaderText();
                                _recordsScreen.RecordsBoardMessage.RenderLoading();
                                _recordsScreen.EnhancedScrollerRecords.ClearRecords();
                                _recordsScreen.EnhancedScrollerRecords.Render();
                                _steamRecordsReceiver.SetFrameSize(FrameSize.Small);
                                _steamRecordsReceiver.ReceiveRecords();
                                break;
                            }
                            case RecordsScreenButtonName.Medium:
                            {
                                _recordsScreen.SetHeaderTextFrameSize(FrameSize.Medium);
                                _recordsScreen.RenderHeaderText();
                                _recordsScreen.RecordsBoardMessage.RenderLoading();
                                _recordsScreen.EnhancedScrollerRecords.ClearRecords();
                                _recordsScreen.EnhancedScrollerRecords.Render();
                                _steamRecordsReceiver.SetFrameSize(FrameSize.Medium);
                                _steamRecordsReceiver.ReceiveRecords();
                                break;
                            }
                            case RecordsScreenButtonName.Large:
                            {
                                _recordsScreen.SetHeaderTextFrameSize(FrameSize.Large);
                                _recordsScreen.RenderHeaderText();
                                _recordsScreen.RecordsBoardMessage.RenderLoading();
                                _recordsScreen.EnhancedScrollerRecords.ClearRecords();
                                _recordsScreen.EnhancedScrollerRecords.Render();
                                _steamRecordsReceiver.SetFrameSize(FrameSize.Large);
                                _steamRecordsReceiver.ReceiveRecords();
                                break;
                            }
                            case RecordsScreenButtonName.Global:
                            {
                                _recordsScreen.SetHeaderTextLeaderboardDataRequest(ELeaderboardDataRequest
                                    .k_ELeaderboardDataRequestGlobal);
                                _recordsScreen.RenderHeaderText();
                                _recordsScreen.RecordsBoardMessage.RenderLoading();
                                _recordsScreen.EnhancedScrollerRecords.ClearRecords();
                                _recordsScreen.EnhancedScrollerRecords.Render();
                                _steamRecordsReceiver.SetLeaderBoardDataRequest(ELeaderboardDataRequest
                                    .k_ELeaderboardDataRequestGlobal);
                                _steamRecordsReceiver.ReceiveRecords();
                                break;
                            }
                            case RecordsScreenButtonName.Friends:
                            {
                                _recordsScreen.SetHeaderTextLeaderboardDataRequest(ELeaderboardDataRequest
                                    .k_ELeaderboardDataRequestFriends);
                                _recordsScreen.RenderHeaderText();
                                _recordsScreen.RecordsBoardMessage.RenderLoading();
                                _recordsScreen.EnhancedScrollerRecords.ClearRecords();
                                _recordsScreen.EnhancedScrollerRecords.Render();
                                _steamRecordsReceiver.SetLeaderBoardDataRequest(ELeaderboardDataRequest
                                    .k_ELeaderboardDataRequestFriends);
                                _steamRecordsReceiver.ReceiveRecords();
                                break;
                            }
                            default:
                            {
                                switch (_selectedHeaderButtonModel.SelectedHeaderButtonName)
                                {
                                    case HeaderButtonName.Return:
                                    {
                                        _gameStateModel.SetGameStateName(GameStateName.Result);
                                        _header.Hide();
                                        _footer.Hide();
                                        _steamResultScreen.Show();
                                        _recordsScreen.Hide();
                                        break;
                                    }
                                }

                                break;
                            }
                        }

                        break;
                    }
                    case GameStateName.Invalid:
                    {
                        _quitGameProcess.QuitGame();
                        break;
                    }
                }
            }

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                switch (_gameStateModel.GameStateName)
                {
                    case GameStateName.Main:
                    {
                        _rotateCurrentTileProcess.RotateCurrentTile();
                        break;
                    }
                    case GameStateName.Tutorial:
                    {
                        _rotateCurrentTileProcess.RotateCurrentTile();
                        break;
                    }
                    case GameStateName.Invalid:
                    {
                        _quitGameProcess.QuitGame();
                        break;
                    }
                }
            }
        }
    }
}