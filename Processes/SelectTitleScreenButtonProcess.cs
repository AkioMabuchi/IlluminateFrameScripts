using Enums;
using Models;
using VContainer;
using Views;

namespace Processes
{
    public class SelectTitleScreenButtonProcess
    {
        private readonly GameStateModel _gameStateModel;
        private readonly SelectedTitleScreenButtonModel _selectedTitleScreenButtonModel;
        private readonly Header _header;
        private readonly Footer _footer;
        private readonly TitleScreen _titleScreen;
        private readonly SelectFrameSizeScreen _selectFrameSizeScreen;
        private readonly SettingsScreen _settingsScreen;
        private readonly RecordsScreen _recordsScreen;
        private readonly QuitGameProcess _quitGameProcess;

        [Inject]
        public SelectTitleScreenButtonProcess(GameStateModel gameStateModel,
            SelectedTitleScreenButtonModel selectedTitleScreenButtonModel,
            Header header, Footer footer, TitleScreen titleScreen, SelectFrameSizeScreen selectFrameSizeScreen,
            SettingsScreen settingsScreen, RecordsScreen recordsScreen, QuitGameProcess quitGameProcess)
        {
            _gameStateModel = gameStateModel;

            _selectedTitleScreenButtonModel = selectedTitleScreenButtonModel;
            _header = header;
            _footer = footer;
            _titleScreen = titleScreen;
            _selectFrameSizeScreen = selectFrameSizeScreen;
            _settingsScreen = settingsScreen;
            _recordsScreen = recordsScreen;
            _quitGameProcess = quitGameProcess;
        }

        public void SelectProcess(TitleScreenButtonName titleScreenButtonName)
        {
            _selectedTitleScreenButtonModel.Select(titleScreenButtonName);

            _titleScreen.ZoomUpButtons(_selectedTitleScreenButtonModel.SelectedTitleScreenButtonName);
        }

        public void DeselectProcess(TitleScreenButtonName titleScreenButtonName)
        {
            _selectedTitleScreenButtonModel.Deselect(titleScreenButtonName);

            _titleScreen.ZoomUpButtons(_selectedTitleScreenButtonModel.SelectedTitleScreenButtonName);
        }

        public void DecideProcess()
        {
            switch (_selectedTitleScreenButtonModel.SelectedTitleScreenButtonName)
            {
                case TitleScreenButtonName.GameStart:
                {
                    _gameStateModel.SetGameStateName(GameStateName.SelectFrameSize);
                    _titleScreen.FadeOut();
                    _selectFrameSizeScreen.FadeIn();
                    _selectFrameSizeScreen.ChangeImageButtonTexts();
                    _header.PullDown();
                    _header.ChangeHeadingText(HeaderHeadingText.SelectFrameSize);
                    _footer.PullUp();
                    _footer.ChangeFootingText(FooterFootingText.None);
                    break;
                }
                case TitleScreenButtonName.Tutorial:
                {
                    break;
                }
                case TitleScreenButtonName.Instruction:
                {
                    break;
                }
                case TitleScreenButtonName.Settings:
                {
                    _gameStateModel.SetGameStateName(GameStateName.Settings);
                    _titleScreen.FadeOut();
                    _settingsScreen.FadeIn();
                    _header.PullDown();
                    _header.ChangeHeadingText(HeaderHeadingText.Settings);
                    _footer.PullUp();
                    _footer.ChangeFootingText(FooterFootingText.None);
                    break;
                }
                case TitleScreenButtonName.Records:
                {
                    _gameStateModel.SetGameStateName(GameStateName.Records);
                    _titleScreen.FadeOut();
                    _recordsScreen.FadeIn();
                    _header.PullDown();
                    _header.ChangeHeadingText(HeaderHeadingText.Records);
                    _footer.PullUp();
                    _footer.ChangeFootingText(FooterFootingText.None);
                    break;
                }
                case TitleScreenButtonName.Achievements:
                {
                    break;
                }
                case TitleScreenButtonName.Credits:
                {
                    break;
                }
                case TitleScreenButtonName.Quit:
                {
                    _quitGameProcess.QuitGame();
                    break;
                }
            }

            _selectedTitleScreenButtonModel.Clear();
        }
    }
}