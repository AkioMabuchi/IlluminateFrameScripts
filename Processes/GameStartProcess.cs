using System;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Processes
{
    public class GameStartProcess
    {
        private readonly ShowUpTitleScreenProcess _showUpTitleScreenProcess;

        [Inject]
        public GameStartProcess(ShowUpTitleScreenProcess showUpTitleScreenProcess)
        {
            _showUpTitleScreenProcess = showUpTitleScreenProcess;
        }
        public async UniTask AsyncGameStart()
        {
            await UniTask.Delay(TimeSpan.Zero);
            
            _showUpTitleScreenProcess.ShowUpTitleScreen();
        }
    }
}