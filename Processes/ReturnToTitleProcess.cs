using System;
using Cysharp.Threading.Tasks;
using Enums;
using Models;
using VContainer;
using Views;
using Views.Screens;
using Views.Screens.Prior;

namespace Processes
{
    public class ReturnToTitleProcess
    {
        private readonly GameStateModel _gameStateModel;
        private readonly Footer _footer;
        private readonly TitleScreen _titleScreen;
        private readonly MainCamera _mainCamera;

        [Inject]
        public ReturnToTitleProcess(GameStateModel gameStateModel, Footer footer, TitleScreen titleScreen,
            MainCamera mainCamera)
        {
            _gameStateModel = gameStateModel;
            _footer = footer;
            _titleScreen = titleScreen;
            _mainCamera = mainCamera;
        }

        public async UniTask AsyncReturnToTitle()
        {
            _footer.RenderText();
            _footer.Hide();
            _mainCamera.LookMenu();
            
            await UniTask.Delay(TimeSpan.FromSeconds(2.0));
            
            _gameStateModel.SetGameStateName(GameStateName.Title);
            _titleScreen.Show();
        }
    }
}