using Cysharp.Threading.Tasks;
using Enums;
using Models;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Processes.Updates
{
    public class EscapeKeyPressedUpdateProcess: ITickable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly ReturnToTitleProcess _returnToTitleProcess;

        [Inject]
        public EscapeKeyPressedUpdateProcess(GameStateModel gameStateModel, ReturnToTitleProcess returnToTitleProcess)
        {
            _gameStateModel = gameStateModel;
            _returnToTitleProcess = returnToTitleProcess;
        }

        public void Tick()
        {
            if (Keyboard.current == null)
            {
                return;
            }

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                switch (_gameStateModel.GameStateName)
                {
                    case GameStateName.Main:
                    {
                        _gameStateModel.SetGameStateName(GameStateName.None);
                        _returnToTitleProcess.AsyncReturnToTitle().Forget();
                        break;
                    }
                }
            }
        }
    }
}