using Cysharp.Threading.Tasks;
using Processes;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace EventHandlers
{
    public class OnEscapeKeyPressedForReturnToTitle : ITickable
    {
        private readonly ReturnToTitleProcess _returnToTitleProcess;
        
        [Inject]
        public OnEscapeKeyPressedForReturnToTitle(ReturnToTitleProcess returnToTitleProcess)
        {
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
                _returnToTitleProcess.ReturnToTitle().Forget();
            }
        }
    }
}