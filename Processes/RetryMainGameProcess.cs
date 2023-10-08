using System;
using Cysharp.Threading.Tasks;
using Models;
using UnityEngine;
using VContainer;
using Views;
using Views.Controllers;
using Views.Screens.Prior;

namespace Processes
{
    public class RetryMainGameProcess
    {
        private readonly MainBoardModel _mainBoardModel;

        private readonly NextTileModel _nextTileModel;

        private readonly Desk _desk;

        private readonly Footer _footer;

        private readonly TileRenderer _tileRenderer;
        private readonly TileThrower _tileThrower;

        private readonly ResetMainGameProcess _resetMainGameProcess;
        private readonly BeginMainGameProcess _beginMainGameProcess;

        [Inject]
        public RetryMainGameProcess(MainBoardModel mainBoardModel, NextTileModel nextTileModel, Desk desk,
            Footer footer, TileRenderer tileRenderer, TileThrower tileThrower,
            ResetMainGameProcess resetMainGameProcess, BeginMainGameProcess beginMainGameProcess)
        {
            _mainBoardModel = mainBoardModel;


            _nextTileModel = nextTileModel;

            _desk = desk;

            _footer = footer;

            _tileRenderer = tileRenderer;
            _tileThrower = tileThrower;

            _resetMainGameProcess = resetMainGameProcess;
            _beginMainGameProcess = beginMainGameProcess;
        }

        public async UniTask AsyncRetryMainGame()
        {
            var tileIds = _mainBoardModel.AddedTileIds; // REQUIRED
            
            _tileRenderer.RenderDarkenAllTilesComplete();

            if (_nextTileModel.TryGetTileId(out var nextTileId))
            {
                _tileThrower.ThrowTile(nextTileId, new Vector3(0.0f, 2.0f, -3.0f), new Vector3(1000.0f, 0.0f, 0.0f));
            }

            _resetMainGameProcess.ResetMainGame();
            _desk.ValueDisplayScore.Draw();
            _desk.ValueDisplayTileRestAmount.Draw();

            await UniTask.Delay(TimeSpan.FromSeconds(0.5));

            foreach (var tileId in tileIds)
            {
                _tileThrower.ThrowTile(tileId, new Vector3(0.0f, 2.0f, 3.0f), new Vector3(1000.0f, 0.0f, 0.0f));
                await UniTask.Delay(TimeSpan.FromSeconds(0.05));
            }

            await UniTask.Delay(TimeSpan.FromSeconds(1.5));

            _footer.Show();
            await UniTask.Delay(TimeSpan.FromSeconds(1.5));

            _beginMainGameProcess.BeginMainGame();
        }
    }
}