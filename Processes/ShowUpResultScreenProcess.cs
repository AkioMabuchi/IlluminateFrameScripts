using System;
using Cysharp.Threading.Tasks;
using Enums;
using Models;
using VContainer;
using Views;
using Views.Screens;

namespace Processes
{
    public class ShowUpResultScreenProcess
    {
        private readonly GameStateModel _gameStateModel;
        private readonly BackScreen _backScreen;
        private readonly SteamResultScreen _steamResultScreen;

        [Inject]
        public ShowUpResultScreenProcess(GameStateModel gameStateModel, BackScreen backScreen,
            SteamResultScreen steamResultScreen)
        {
            _gameStateModel = gameStateModel;
            _backScreen = backScreen;
            _steamResultScreen = steamResultScreen;
        }

        public async UniTask ShowUpResultScreen()
        {
            _gameStateModel.SetGameStateName(GameStateName.Result);
            
            _backScreen.FadeIn();
            await UniTask.Delay(TimeSpan.FromSeconds(1.0));
            
            _steamResultScreen.RefreshLineCounts();
            _steamResultScreen.Render();
            _steamResultScreen.ShowUp();
        }
    }
}