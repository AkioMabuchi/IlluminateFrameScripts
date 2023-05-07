using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Models;
using Parameters.Enums;
using VContainer;

namespace Processes
{
    public class MainPanelBoardProcess
    {
        private readonly TilesModel _tilesModel;
        private readonly NextTileModel _nextTileModel;

        private readonly ConductMainBoardProcess _conductMainBoardProcess;
        private readonly TakeTileProcess _takeTileProcess;
        private readonly PrepareNextTileProcess _prepareNextTileProcess;
        private readonly UpdateValidCellPositionsProcess _updateValidCellPositionsProcess;
        private readonly IlluminateBoardProcess _illuminateBoardProcess;

        [Inject]
        public MainPanelBoardProcess(TilesModel tilesModel, NextTileModel nextTileModel,
            ConductMainBoardProcess conductMainBoardProcess, TakeTileProcess takeTileProcess,
            PrepareNextTileProcess prepareNextTileProcess, IlluminateBoardProcess illuminateBoardProcess,
            UpdateValidCellPositionsProcess updateValidCellPositionsProcess)
        {
            _nextTileModel = nextTileModel;
            _tilesModel = tilesModel;
            _conductMainBoardProcess = conductMainBoardProcess;
            _takeTileProcess = takeTileProcess;
            _illuminateBoardProcess = illuminateBoardProcess;
            _prepareNextTileProcess = prepareNextTileProcess;
            _updateValidCellPositionsProcess = updateValidCellPositionsProcess;
        }

        public async UniTask AsyncMainPanelBoardProcess()
        {
            var boardConductionResult = _conductMainBoardProcess.ConductMainBoard();

            await _illuminateBoardProcess.IlluminateBoard(boardConductionResult.IlluminatePaths);

            _updateValidCellPositionsProcess.UpdateValidCellPositions();

            if (_nextTileModel.NextTileId.HasValue)
            {
                var nextTileId = _nextTileModel.NextTileId.Value;
                _takeTileProcess.TakeTile(nextTileId);
            }

            var tiles = new List<TileType>()
            {
                TileType.Straight,
                TileType.Straight,
                TileType.Straight,
                TileType.Straight,
                TileType.Curve,
                TileType.Curve,
                TileType.Curve,
                TileType.Curve,
                TileType.Curve,
                TileType.TwinCurves,
                TileType.TwinCurves,
                TileType.Cross,
                TileType.Cross,
                TileType.Distributor3,
                TileType.Distributor3,
                TileType.Distributor4,
                TileType.Bulb,
            };
            
            var tileType = tiles[UnityEngine.Random.Range(0, tiles.Count)];
            _prepareNextTileProcess.PrepareNextTile(_tilesModel.AddTile(tileType));
        }
    }
}