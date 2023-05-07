using Cysharp.Threading.Tasks;
using Models;
using Parameters.Enums;
using Processes;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnGameStart : IPostStartable
    {
        private readonly StartMainGameProcess _startMainGameProcess;

        [Inject]
        public OnGameStart(StartMainGameProcess startMainGameProcess)
        {
            _startMainGameProcess = startMainGameProcess;
        }

        public void PostStart()
        {
            _startMainGameProcess.AsyncStartMainGame(PanelSize.Large).Forget();
        }
    }
}