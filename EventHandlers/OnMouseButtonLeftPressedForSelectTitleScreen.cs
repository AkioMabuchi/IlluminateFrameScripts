using Processes;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace EventHandlers
{
    public class OnMouseButtonLeftPressedForSelectTitleScreen : ITickable
    {
        private readonly SelectTitleScreenButtonProcess _selectTitleScreenButtonProcess;

        [Inject]
        public OnMouseButtonLeftPressedForSelectTitleScreen(SelectTitleScreenButtonProcess selectTitleScreenButtonProcess)
        {
            _selectTitleScreenButtonProcess = selectTitleScreenButtonProcess;
        }
        
        public void Tick()
        {
            if (Mouse.current == null)
            {
                return;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _selectTitleScreenButtonProcess.DecideProcess();
            }
        }
    }
}