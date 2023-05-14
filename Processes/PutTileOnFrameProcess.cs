using Models;
using UnityEngine;
using VContainer;
using Views;

namespace Processes
{
    public class PutTileOnFrameProcess
    {
        private readonly MainBoardModel _mainBoardModel;
        
        private readonly TileFactory _tileFactory;
        
        [Inject]
        public PutTileOnFrameProcess(MainBoardModel mainBoardModel, TileFactory tileFactory)
        {
            _mainBoardModel = mainBoardModel;
            _tileFactory = tileFactory;
        }
        
        public void PutTileOnBoard(Vector2Int cellPosition, int tileId)
        {
            _mainBoardModel.PutTile(cellPosition, tileId);
            
            _tileFactory.PutTileOnBoard(tileId, cellPosition);
            _tileFactory.RotateTileImmediate(tileId);
        }
    }
}