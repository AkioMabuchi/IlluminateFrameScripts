using System;
using Cysharp.Threading.Tasks;
using Enums;
using Models;
using VContainer;
using Views;

namespace Processes
{
    public class SelectHeaderButtonProcess
    {
        private readonly GameStateModel _gameStateModel;
        
        private readonly SelectedHeaderButtonModel _selectedHeaderButtonModel;
        private readonly Header _header;
        private readonly Footer _footer;
        private readonly TitleScreen _titleScreen;
        private readonly SelectFrameSizeScreen _selectFrameSizeScreen;
        private readonly InstructionScreen _instructionScreen;
        private readonly SettingsScreen _settingsScreen;
        private readonly RecordsScreen _recordsScreen;
        private readonly AchievementsScreen _achievementsScreen;
        private readonly CreditsScreen _creditsScreen;

        [Inject]
        public SelectHeaderButtonProcess(GameStateModel gameStateModel,
            SelectedHeaderButtonModel selectedHeaderButtonModel, Header header, Footer footer, TitleScreen titleScreen,
            SelectFrameSizeScreen selectFrameSizeScreen, InstructionScreen instructionScreen,
            SettingsScreen settingsScreen, RecordsScreen recordsScreen, AchievementsScreen achievementsScreen,
            CreditsScreen creditsScreen)
        {
            _gameStateModel = gameStateModel;

            _selectedHeaderButtonModel = selectedHeaderButtonModel;
            _header = header;
            _footer = footer;
            _titleScreen = titleScreen;
            _selectFrameSizeScreen = selectFrameSizeScreen;
            _instructionScreen = instructionScreen;
            _settingsScreen = settingsScreen;
            _recordsScreen = recordsScreen;
            _achievementsScreen = achievementsScreen;
            _creditsScreen = creditsScreen;
        }

        public void SelectProcess(HeaderButtonName headerButtonName)
        {
            _selectedHeaderButtonModel.Select(headerButtonName);
            
            _header.ZoomUpButtons(_selectedHeaderButtonModel.SelectedHeaderButtonName);

            _footer.ChangeFootingText(_selectedHeaderButtonModel.SelectedHeaderButtonName switch
            {
                HeaderButtonName.Return => FooterFootingText.ReturnToTitle,
                _ => FooterFootingText.None,
            });
        }

        public void DeselectProcess(HeaderButtonName headerButtonName)
        {
            _selectedHeaderButtonModel.Deselect(headerButtonName);
            
            _header.ZoomUpButtons(_selectedHeaderButtonModel.SelectedHeaderButtonName);
            
            _footer.ChangeFootingText(_selectedHeaderButtonModel.SelectedHeaderButtonName switch
            {
                HeaderButtonName.Return => FooterFootingText.ReturnToTitle,
                _ => FooterFootingText.None,
            });
        }

        public async UniTask DecideProcess()
        {
            switch (_selectedHeaderButtonModel.SelectedHeaderButtonName)
            {
                case HeaderButtonName.Return:
                {
                    switch (_gameStateModel.GameStateName)
                    {
                        case GameStateName.SelectFrameSize:
                        {
                            _gameStateModel.SetGameStateName(GameStateName.None);
                            _header.PullUp();
                            _footer.PullDown();
                            _selectFrameSizeScreen.FadeOut();

                            await UniTask.Delay(TimeSpan.FromSeconds(1.0));

                            _gameStateModel.SetGameStateName(GameStateName.Title);
                            _titleScreen.FadeIn();
                            _titleScreen.ChangeTexts();
                            _titleScreen.ResizeButtons();
                            break;
                        }
                        case GameStateName.Settings:
                        {
                            _gameStateModel.SetGameStateName(GameStateName.None);
                            _header.PullUp();
                            _footer.PullDown();
                            _settingsScreen.FadeOut();
                            
                            await UniTask.Delay(TimeSpan.FromSeconds(1.0));

                            _gameStateModel.SetGameStateName(GameStateName.Title);
                            _titleScreen.FadeIn();
                            _titleScreen.ChangeTexts();
                            _titleScreen.ResizeButtons();
                            
                            break;
                        }
                        case GameStateName.Records:
                        {
                            _gameStateModel.SetGameStateName(GameStateName.None);
                            _header.PullUp();
                            _footer.PullDown();
                            _recordsScreen.FadeOut();

                            await UniTask.Delay(TimeSpan.FromSeconds(1.0));

                            _gameStateModel.SetGameStateName(GameStateName.Title);
                            _titleScreen.FadeIn();
                            _titleScreen.ChangeTexts();
                            _titleScreen.ResizeButtons();
                            
                            break;
                        }
                    }

                    break;
                }
            }
            
            _selectedHeaderButtonModel.Clear();
        }
    }
}