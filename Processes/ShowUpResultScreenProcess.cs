using Enums;
using Models;
using VContainer;
using Views;

namespace Processes
{
    public class ShowUpResultScreenProcess
    {
        private readonly GameStateModel _gameStateModel;
        private readonly ResultScreen _resultScreen;

        [Inject]
        public ShowUpResultScreenProcess(GameStateModel gameStateModel, ResultScreen resultScreen)
        {
            _gameStateModel = gameStateModel;
            _resultScreen = resultScreen;
        }

        public void ShowUpResultScreen()
        {
            _gameStateModel.SetGameStateName(GameStateName.Result);
            _resultScreen.ChangeTexts();
            _resultScreen.ShowUp();
        }
    }
}