using System;
using Cysharp.Threading.Tasks;
using Enums;
using VContainer;
using Views;
using Views.Controllers;
using Views.Screens.Prior;

namespace Processes
{
    public class StartMainGameProcess
    {
        private readonly MainCamera _mainCamera;
        private readonly MusicPlayer _musicPlayer;
        
        private readonly Desk _desk;
        private readonly TileRenderer _tileRenderer;
        
        private readonly InitializeMainGameProcess _initializeMainGameProcess;
        private readonly ResetMainGameProcess _resetMainGameProcess;
        private readonly BeginMainGameProcess _beginMainGameProcess;
        private readonly ClearMainGameProcess _clearMainGameProcess;

        [Inject]
        public StartMainGameProcess(MainCamera mainCamera, MusicPlayer musicPlayer, Desk desk,
            TileRenderer tileRenderer, InitializeMainGameProcess initializeMainGameProcess,
            ResetMainGameProcess resetMainGameProcess, BeginMainGameProcess beginMainGameProcess,
            ClearMainGameProcess clearMainGameProcess)
        {
            _mainCamera = mainCamera;
            _musicPlayer = musicPlayer;

            _desk = desk;
            _tileRenderer = tileRenderer;

            _initializeMainGameProcess = initializeMainGameProcess;
            _resetMainGameProcess = resetMainGameProcess;
            _beginMainGameProcess = beginMainGameProcess;
            _clearMainGameProcess = clearMainGameProcess;
        }

        public async UniTask AsyncStartMainGame(FrameSize frameSize)
        {
            _clearMainGameProcess.ClearMainGame();
            _initializeMainGameProcess.InitializeMainGame(frameSize);
            _resetMainGameProcess.ResetMainGame();

            _desk.ValueDisplayScore.DrawImmediate();
            _desk.ValueDisplayTileRestAmount.DrawImmediate();
            
            _tileRenderer.RenderAllTiles();
            
            _mainCamera.LookMainBoard();
            _musicPlayer.MuteMain(false);

            await UniTask.Delay(TimeSpan.FromSeconds(4.0));
            
            _beginMainGameProcess.BeginMainGame();
        }
    }
}