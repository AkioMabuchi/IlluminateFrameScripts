using System;
using Cysharp.Threading.Tasks;
using Enums;
using Models;
using VContainer;
using Views;

namespace Processes
{
    public class StartMainGameProcess
    {
        private readonly GameStateModel _gameStateModel;
        
        private readonly TileDeckModel _tileDeckModel;
        private readonly TileRestAmountModel _tileRestAmountModel;
        private readonly TilesModel _tilesModel;

        private readonly MainCamera _mainCamera;

        private readonly Footer _footer;

        private readonly DeskFactory _deskFactory;
        

        private readonly TakeTileProcess _takeTileProcess;
        private readonly PrepareNextTileProcess _prepareNextTileProcess;
        
        private readonly UpdateValidCellPositionsProcess _updateValidCellPositionsProcess;

        private readonly ClearMainGameProcess _clearMainGameProcess;
        private readonly InitializeMainGameProcess _initializeMainGameProcess;

        [Inject]
        public StartMainGameProcess(GameStateModel gameStateModel, TileDeckModel tileDeckModel, MainCamera mainCamera,
            DeskFactory deskFactory, Footer footer, TileRestAmountModel tileRestAmountModel, TilesModel tilesModel,
            TakeTileProcess takeTileProcess, PrepareNextTileProcess prepareNextTileProcess,
            UpdateValidCellPositionsProcess updateValidCellPositionsProcess, ClearMainGameProcess clearMainGameProcess,
            InitializeMainGameProcess initializeMainGameProcess)
        {
            _gameStateModel = gameStateModel;

            _tileDeckModel = tileDeckModel;
            _tileRestAmountModel = tileRestAmountModel;
            _tilesModel = tilesModel;

            _mainCamera = mainCamera;
            
            _footer = footer;

            _deskFactory = deskFactory;
            
            _takeTileProcess = takeTileProcess;
            _prepareNextTileProcess = prepareNextTileProcess;

            _clearMainGameProcess = clearMainGameProcess;
            _initializeMainGameProcess = initializeMainGameProcess;
            
            _updateValidCellPositionsProcess = updateValidCellPositionsProcess;
        }

        public async UniTask AsyncStartMainGame(FrameSize frameSize)
        {
            _clearMainGameProcess.ClearMainGame();
            _initializeMainGameProcess.InitializeMainGame(frameSize);

            _mainCamera.LookMainBoard();
            await UniTask.Delay(TimeSpan.FromSeconds(4.0));

            _gameStateModel.SetGameStateName(GameStateName.Main);
            _footer.ChangeFootingText(FooterFootingText.MainGame);
            _updateValidCellPositionsProcess.UpdateValidCellPositions();
            _tileRestAmountModel.DecreaseTileRestAmount();
            _deskFactory.Desk.DisplayTileRestAmount();
            _takeTileProcess.TakeTile(_tilesModel.AddTile(_tileDeckModel.TakeTile()));
            _prepareNextTileProcess.PrepareNextTile(_tilesModel.AddTile(_tileDeckModel.TakeTile()));
        }
    }
}