using Models;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace EventHandlers
{
    public class OnMouseButtonLeftPressedForSelectResultScreen : ITickable
    {
        private readonly SelectResultScreenButtonProcess _selectResultScreenButtonProcess;

        [Inject]
        public OnMouseButtonLeftPressedForSelectResultScreen(
            SelectResultScreenButtonProcess selectResultScreenButtonProcess)
        {
            _selectResultScreenButtonProcess = selectResultScreenButtonProcess;
        }

        public void Tick()
        {
            if (Mouse.current == null)
            {
                return;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _selectResultScreenButtonProcess.DecideProcess();
            }
        }
    }
}