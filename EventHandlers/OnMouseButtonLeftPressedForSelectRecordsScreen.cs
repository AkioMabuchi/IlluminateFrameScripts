using Processes;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace EventHandlers
{
    public class OnMouseButtonLeftPressedForSelectRecordsScreen : ITickable
    {
        private readonly SelectRecordsScreenButtonProcess _selectRecordsScreenButtonProcess;

        [Inject]
        public OnMouseButtonLeftPressedForSelectRecordsScreen(
            SelectRecordsScreenButtonProcess selectRecordsScreenButtonProcess)
        {
            _selectRecordsScreenButtonProcess = selectRecordsScreenButtonProcess;
        }

        public void Tick()
        {
            if (Mouse.current == null)
            {
                return;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _selectRecordsScreenButtonProcess.DecideProcess();
            }
        }
    }
}