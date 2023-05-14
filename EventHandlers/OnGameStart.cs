using Cysharp.Threading.Tasks;
using Processes;
using VContainer;
using VContainer.Unity;

namespace EventHandlers
{
    public class OnGameStart : IPostStartable
    {
        private readonly GameStartProcess _gameStartProcess;

        [Inject]
        public OnGameStart(GameStartProcess gameStartProcess)
        {
            _gameStartProcess = gameStartProcess;
        }

        public void PostStart()
        {
            _gameStartProcess.AsyncGameStart().Forget();
        }
    }
}