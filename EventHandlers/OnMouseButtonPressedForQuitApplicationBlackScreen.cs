using Enums;
using Models;
using Processes;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace EventHandlers
{
    public class OnMouseButtonPressedForQuitApplicationBlackScreen : ITickable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly QuitGameProcess _quitGameProcess;

        [Inject]
        public OnMouseButtonPressedForQuitApplicationBlackScreen(GameStateModel gameStateModel,
            QuitGameProcess quitGameProcess)
        {
            _gameStateModel = gameStateModel;
            _quitGameProcess = quitGameProcess;
        }
        public void Tick()
        {
            if (Mouse.current == null)
            {
                return;
            }

            if (_gameStateModel.GameStateName != GameStateName.Invalid)
            {
                return;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame)
            {
                _quitGameProcess.QuitGame();
            }
        }
    }
}