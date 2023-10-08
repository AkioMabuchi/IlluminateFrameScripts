using Enums;
using Models;
using Models.Instances.Frames;
using VContainer;
using Views;
using Views.Controllers;
using Views.TextEffectFactories;

namespace Processes
{
    public class InitializeMainGameProcess
    {
        private readonly MainFrameModel _mainFrameModel;
        private readonly TileDeckModel _tileDeckModel;
        private readonly TileRestAmountModel _tileRestAmountModel;

        private readonly TileIdModel _tileIdModel;
        
        private readonly FrameSmallModel _frameSmallModel;
        private readonly FrameMediumModel _frameMediumModel;
        private readonly FrameLargeModel _frameLargeModel;

        private readonly MainBoardModel _mainBoardModel;

        private readonly BoardTilePositioner _boardTilePositioner;
        private readonly NextTilePositioner _nextTilePositioner;
        
        private readonly Desk _desk;
        private readonly ConductScoreTextEffectFactory _conductScoreTextEffectFactory;
        private readonly IlluminateScoreTextEffectFactory _illuminateScoreTextEffectFactory;
        private readonly LineCountTextEffectFactory _lineCountTextEffectFactory;
        private readonly LineScoreTextEffectFactory _lineScoreTextEffectFactory;
        private readonly NowhereTextEffectFactory _nowhereTextEffectFactory;
        
        private readonly MainCamera _mainCamera;
        private readonly BoardCellPointer _boardCellPointer;

        private readonly SteamRecordSender _steamRecordSender;

        private readonly ShowFrameProcess _showFrameProcess;
        private readonly GenerateInitialTilesProcess _generateInitialTilesProcess;

        [Inject]
        public InitializeMainGameProcess(MainFrameModel mainFrameModel, TileDeckModel tileDeckModel,
            TileRestAmountModel tileRestAmountModel, FrameSmallModel frameSmallModel, FrameMediumModel frameMediumModel,
            FrameLargeModel frameLargeModel, MainBoardModel mainBoardModel,
            ValidCellPositionsModel validCellPositionsModel,
            BoardTilePositioner boardTilePositioner, NextTilePositioner nextTilePositioner, Desk desk,
            ConductScoreTextEffectFactory conductScoreTextEffectFactory,
            IlluminateScoreTextEffectFactory illuminateScoreTextEffectFactory,
            LineCountTextEffectFactory lineCountTextEffectFactory,
            LineScoreTextEffectFactory lineScoreTextEffectFactory,
            NowhereTextEffectFactory nowhereTextEffectFactory, MainCamera mainCamera, BoardCellPointer boardCellPointer,
            SteamRecordSender steamRecordSender, ShowFrameProcess showFrameProcess,
            GenerateInitialTilesProcess generateInitialTilesProcess)
        {
            _mainFrameModel = mainFrameModel;
            _tileDeckModel = tileDeckModel;
            _tileRestAmountModel = tileRestAmountModel;

            _frameSmallModel = frameSmallModel;
            _frameMediumModel = frameMediumModel;
            _frameLargeModel = frameLargeModel;

            _mainBoardModel = mainBoardModel;

            _boardTilePositioner = boardTilePositioner;
            _nextTilePositioner = nextTilePositioner;

            _desk = desk;

            _conductScoreTextEffectFactory = conductScoreTextEffectFactory;
            _illuminateScoreTextEffectFactory = illuminateScoreTextEffectFactory;
            _lineCountTextEffectFactory = lineCountTextEffectFactory;
            _lineScoreTextEffectFactory = lineScoreTextEffectFactory;
            _nowhereTextEffectFactory = nowhereTextEffectFactory;

            _mainCamera = mainCamera;
            _boardCellPointer = boardCellPointer;

            _steamRecordSender = steamRecordSender;

            _showFrameProcess = showFrameProcess;
            _generateInitialTilesProcess = generateInitialTilesProcess;
        }

        public void InitializeMainGame(FrameSize frameSize)
        {
            _mainFrameModel.SetMainFrame(frameSize switch
            {
                FrameSize.Small => _frameSmallModel,
                FrameSize.Medium => _frameMediumModel,
                FrameSize.Large => _frameLargeModel,
                _ => null
            });
            
            _mainFrameModel.FrameModel.Initialize();
            
            _tileDeckModel.Initialize(frameSize);
            _tileRestAmountModel.Initialize(frameSize);

            _mainBoardModel.Initialize(frameSize);

            _boardTilePositioner.SetFrameSize(frameSize);
            
            _desk.ChangeDesk(frameSize);
            
            _conductScoreTextEffectFactory.Initialize(frameSize);
            _illuminateScoreTextEffectFactory.Initialize(frameSize);
            _lineCountTextEffectFactory.Initialize(frameSize);
            _lineScoreTextEffectFactory.Initialize(frameSize);
            _nowhereTextEffectFactory.Initialize(frameSize);

            _mainCamera.Move(frameSize);
            _boardCellPointer.SetFrameSize(frameSize);
            _nextTilePositioner.SetPositionsByFrameSize(frameSize);

            _steamRecordSender.SetFrameSize(frameSize);

            _desk.ValueDisplayScore.DrawImmediate();
            _desk.ValueDisplayTileRestAmount.DrawImmediate();

            _showFrameProcess.ShowFrame(frameSize);
            _generateInitialTilesProcess.GenerateInitialTiles();
        }
    }
}