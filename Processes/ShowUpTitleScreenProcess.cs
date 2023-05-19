using Enums;
using Models;
using VContainer;
using Views;

namespace Processes
{
    public class ShowUpTitleScreenProcess
    {
        private readonly GameStateModel _gameStateModel;
        private readonly TitleScreen _titleScreen;

        [Inject]
        public ShowUpTitleScreenProcess(GameStateModel gameStateModel, TitleScreen titleScreen)
        {
            _gameStateModel = gameStateModel;
            _titleScreen = titleScreen;
        }

        public void ShowUpTitleScreen()
        {
            _gameStateModel.SetGameStateName(GameStateName.Title);
            _titleScreen.ChangeTexts();
            _titleScreen.ResizeButtons();
            _titleScreen.ShowUp();
        }
        
    }
}