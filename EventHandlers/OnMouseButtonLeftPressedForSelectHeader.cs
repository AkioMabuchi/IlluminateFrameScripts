using Cysharp.Threading.Tasks;
using Processes;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace EventHandlers
{
    public class OnMouseButtonLeftPressedForSelectHeader : ITickable
    {
        private readonly SelectHeaderButtonProcess _selectHeaderButtonProcess;

        [Inject]
        public OnMouseButtonLeftPressedForSelectHeader(SelectHeaderButtonProcess selectHeaderButtonProcess)
        {
            _selectHeaderButtonProcess = selectHeaderButtonProcess;
        }

        public void Tick()
        {
            if (Mouse.current == null)
            {
                return;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _selectHeaderButtonProcess.DecideProcess().Forget();
            }
        }
    }
}