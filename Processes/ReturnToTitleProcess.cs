using System;
using Cysharp.Threading.Tasks;
using Enums;
using Models;
using VContainer;
using Views;

namespace Processes
{
    public class ReturnToTitleProcess
    {
        private readonly GameStateModel _gameStateModel;
        private readonly Footer _footer;
        private readonly MainCamera _mainCamera;
        private readonly ShowUpTitleScreenProcess _showUpTitleScreenProcess;
        
        [Inject]
        public ReturnToTitleProcess(GameStateModel gameStateModel, Footer footer, MainCamera mainCamera, ShowUpTitleScreenProcess showUpTitleScreenProcess)
        {
            _gameStateModel = gameStateModel;
            _footer = footer;
            _mainCamera = mainCamera;
            _showUpTitleScreenProcess = showUpTitleScreenProcess;
        }

        public async UniTask ReturnToTitle()
        {
            if (_gameStateModel.GameStateName != GameStateName.Main)
            {
                return;
            }
            
            _gameStateModel.SetGameStateName(GameStateName.None);

            _footer.ChangeFootingText(FooterFootingText.None);
            _footer.PullDown();
            _mainCamera.LookMenu();
            
            await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
            
            _showUpTitleScreenProcess.ShowUpTitleScreen();
        }
    }
}