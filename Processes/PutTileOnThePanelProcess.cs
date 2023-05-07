using Models;
using Parameters.Classes.Statics;
using UnityEngine;
using VContainer;
using Views;

namespace Processes
{
    public class PutTileOnThePanelProcess
    {
        private readonly MainBoardModel _mainBoardModel;
        
        private readonly TileFactory _tileFactory;
        
        [Inject]
        public PutTileOnThePanelProcess(MainBoardModel mainBoardModel, TileFactory tileFactory)
        {
            _mainBoardModel = mainBoardModel;
            _tileFactory = tileFactory;
        }
        
        public void PutTileOnThePanel(Vector2Int cellPosition, int tileId)
        {
            _mainBoardModel.PutTile(cellPosition, tileId);
            var position = new Vector3(cellPosition.x * Params.TileSize, 0.0f, cellPosition.y * Params.TileSize) +
                           new Vector3(0.0f, 0.01f, 0.0f);
            
            _tileFactory.MoveTile(tileId, position);
            _tileFactory.RotateTileImmediate(tileId);
        }
    }
}