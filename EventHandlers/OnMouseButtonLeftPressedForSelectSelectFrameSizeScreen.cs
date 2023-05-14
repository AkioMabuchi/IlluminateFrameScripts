using Processes;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace EventHandlers
{
    public class OnMouseButtonLeftPressedForSelectSelectFrameSizeScreen : ITickable
    {
        private readonly SelectSelectFrameSizeScreenProcess _selectSelectFrameSizeScreenProcess;

        [Inject]
        public OnMouseButtonLeftPressedForSelectSelectFrameSizeScreen(
            SelectSelectFrameSizeScreenProcess selectSelectFrameSizeScreenProcess)
        {
            _selectSelectFrameSizeScreenProcess = selectSelectFrameSizeScreenProcess;
        }
        public void Tick()
        {
            if (Mouse.current == null)
            {
                return;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _selectSelectFrameSizeScreenProcess.DecideProcess();
            }
        }
    }
}