using System;
using Cysharp.Threading.Tasks;
using VContainer;
using Views;

namespace Processes
{
    public class EndMainGameProcess
    {
        private readonly Footer _footer;
        private readonly ShowUpResultScreenProcess _showUpResultScreenProcess;
        
        [Inject]
        public EndMainGameProcess(Footer footer, ShowUpResultScreenProcess showUpResultScreenProcess)
        {
            _footer = footer;
            _showUpResultScreenProcess = showUpResultScreenProcess;
        }

        public async UniTask EndMainGame()
        {
            _footer.PullDown();
            await UniTask.Delay(TimeSpan.FromSeconds(1.0));
            _showUpResultScreenProcess.ShowUpResultScreen();
        }
    }
}