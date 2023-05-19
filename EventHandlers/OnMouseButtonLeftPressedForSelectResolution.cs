using Processes;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace EventHandlers
{
    public class OnMouseButtonLeftPressedForSelectResolution : ITickable
    {
        private readonly SelectResolutionSettingScreenProcess _selectResolutionSettingScreenProcess;

        [Inject]
        public OnMouseButtonLeftPressedForSelectResolution(
            SelectResolutionSettingScreenProcess selectResolutionSettingScreenProcess)
        {
            _selectResolutionSettingScreenProcess = selectResolutionSettingScreenProcess;
        }

        public void Tick()
        {
            if (Mouse.current == null)
            {
                return;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _selectResolutionSettingScreenProcess.DecideProcess();
            }
        }
    }
}