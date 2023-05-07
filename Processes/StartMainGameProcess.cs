using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Models;
using Parameters.Enums;
using UnityEngine;
using VContainer;
using Views;

namespace Processes
{
    public class StartMainGameProcess
    {
        private readonly MainBoardModel _mainBoardModel;
        private readonly ScoreModel _scoreModel;
        private readonly TileDeckModel _tileDeckModel;
        private readonly TileRestAmountModel _tileRestAmountModel;
        private readonly TilesModel _tilesModel;

        private readonly ScoreBoardFactory _scoreBoardFactory;
        private readonly TileRestAmountBoardFactory _tileRestAmountBoardFactory;
        
        private readonly TakeTileProcess _takeTileProcess;
        private readonly PrepareNextTileProcess _prepareNextTileProcess;

        private readonly PutTileOnThePanelProcess _putTileOnThePanelProcess;
        private readonly UpdateValidCellPositionsProcess _updateValidCellPositionsProcess;

        [Inject]
        public StartMainGameProcess(MainBoardModel mainBoardModel, ScoreModel scoreModel, TileDeckModel tileDeckModel,
            TileRestAmountModel tileRestAmountModel, TilesModel tilesModel, ScoreBoardFactory scoreBoardFactory,
            TileRestAmountBoardFactory tileRestAmountBoardFactory,
            TakeTileProcess takeTileProcess, PrepareNextTileProcess prepareNextTileProcess,
            PutTileOnThePanelProcess putTileOnThePanelProcess,
            UpdateValidCellPositionsProcess updateValidCellPositionsProcess)
        {
            _mainBoardModel = mainBoardModel;
            _scoreModel = scoreModel;
            _tileDeckModel = tileDeckModel;
            _tileRestAmountModel = tileRestAmountModel;
            _tilesModel = tilesModel;

            _scoreBoardFactory = scoreBoardFactory;
            _tileRestAmountBoardFactory = tileRestAmountBoardFactory;
            
            _takeTileProcess = takeTileProcess;
            _prepareNextTileProcess = prepareNextTileProcess;
            _putTileOnThePanelProcess = putTileOnThePanelProcess;
            _updateValidCellPositionsProcess = updateValidCellPositionsProcess;
        }

        public async UniTask AsyncStartMainGame(PanelSize panelSize)
        {
            _mainBoardModel.ClearBoard();
            
            _scoreBoardFactory.GenerateScoreBoard(panelSize);
            _tileRestAmountBoardFactory.GenerateTileRestAmountBoard(panelSize);
            
            _tileDeckModel.ResetTileDeck(panelSize);
            _scoreModel.ResetScore();
            _tileRestAmountModel.ResetTileRestAmount(panelSize);
            
            _tilesModel.ClearTiles();
            
            var initialTiles = new List<(Vector2Int, TileType)>
            {
                (new Vector2Int(-1, 0), TileType.PowerMinus),
                (new Vector2Int(0, 0), TileType.PowerAlternating),
                (new Vector2Int(1, 0), TileType.PowerPlus),
                (new Vector2Int(-7, 6), TileType.TerminalPlus),
                (new Vector2Int(-7, -6), TileType.TerminalPlus),
                (new Vector2Int(-7, 0), TileType.TerminalAlternatingL),
                (new Vector2Int(7, 0), TileType.TerminalAlternatingR),
                (new Vector2Int(7, 6), TileType.TerminalMinus),
                (new Vector2Int(7, -6), TileType.TerminalMinus),
            };

            foreach (var (cellPosition, tileType) in initialTiles)
            {
                var tileId = _tilesModel.AddTile(tileType);
                _putTileOnThePanelProcess.PutTileOnThePanel(cellPosition, tileId);
            }
            
            _updateValidCellPositionsProcess.UpdateValidCellPositions();
            await UniTask.Delay(TimeSpan.Zero);
            
            _scoreBoardFactory.SetScoreBoardDisplayMode(NumberDisplayMode.Show);
            _tileRestAmountBoardFactory.SetTileRestAmountBoardDisplayMode(NumberDisplayMode.Show);
            
            _takeTileProcess.TakeTile(_tilesModel.AddTile(TileType.Straight));
            _prepareNextTileProcess.PrepareNextTile(_tilesModel.AddTile(TileType.Straight));
        }
    }
}