using Enums;
using Models;
using VContainer;
using Views;

namespace Processes
{
    public class InitializeMainGameProcess
    {
        private readonly MainBoardModel _mainBoardModel;
        private readonly MainFrameModel _mainFrameModel;
        private readonly TileDeckModel _tileDeckModel;
        private readonly TileRestAmountModel _tileRestAmountModel;
        private readonly TilesModel _tilesModel;

        private readonly DeskFactory _deskFactory;
        private readonly TextEffectFactory _textEffectFactory;
        private readonly TileFactory _tileFactory;

        private readonly MainCamera _mainCamera;
        private readonly BoardCellPointer _boardCellPointer;

        [Inject]
        public InitializeMainGameProcess(MainBoardModel mainBoardModel, MainFrameModel mainFrameModel,
            TileDeckModel tileDeckModel, TileRestAmountModel tileRestAmountModel, TilesModel tilesModel,
            DeskFactory deskFactory, TextEffectFactory textEffectFactory, TileFactory tileFactory,
            MainCamera mainCamera, BoardCellPointer boardCellPointer)
        {
            _mainBoardModel = mainBoardModel;
            _mainFrameModel = mainFrameModel;
            _tileDeckModel = tileDeckModel;
            _tileRestAmountModel = tileRestAmountModel;
            _tilesModel = tilesModel;

            _deskFactory = deskFactory;
            _textEffectFactory = textEffectFactory;
            _tileFactory = tileFactory;

            _mainCamera = mainCamera;
            _boardCellPointer = boardCellPointer;
        }

        public void InitializeMainGame(FrameSize frameSize)
        {
            _mainFrameModel.SetMainFrame(frameSize);
            _tileDeckModel.ResetTileDeck(frameSize);
            _tileRestAmountModel.ResetTileRestAmount(frameSize);
            
            _deskFactory.GenerateDesk(frameSize);
            _textEffectFactory.SetNextTilePosition(frameSize);
            _textEffectFactory.SetBoardBasePosition(frameSize);
            _tileFactory.SetGeneratePosition(frameSize);
            _tileFactory.SetNextTilePosition(frameSize);
            _tileFactory.SetBoardBasePosition(frameSize);

            _mainCamera.Move(frameSize);
            _boardCellPointer.SetBasePosition(frameSize);
            
            foreach (var (cellPosition, tileType) in _mainFrameModel.Frame.InitialTiles)
            {
                var tileId = _tilesModel.AddTile(tileType);
                _mainBoardModel.PutTile(cellPosition, tileId);
                _tileFactory.PutTileOnBoard(tileId, cellPosition);
                _tileFactory.RotateTileImmediate(tileId);
            }
            
            _deskFactory.Desk.DisplayScore();
            _deskFactory.Desk.DisplayTileRestAmount();
        }
    }
}