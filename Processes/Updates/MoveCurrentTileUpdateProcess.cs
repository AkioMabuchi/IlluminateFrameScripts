using Enums;
using Models;
using VContainer;
using VContainer.Unity;
using Views.Controllers;

namespace Processes.Updates
{
    public class MoveCurrentTileUpdateProcess: ITickable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly CurrentTileModel _currentTileModel;

        private readonly CurrentTilePositioner _currentTilePositioner;
        private readonly TileMover _tileMover;

        [Inject]
        public MoveCurrentTileUpdateProcess(GameStateModel gameStateModel, CurrentTileModel currentTileModel,
            CurrentTilePositioner currentTilePositioner, TileMover tileMover)
        {
            _gameStateModel = gameStateModel;
            _currentTileModel = currentTileModel;

            _currentTilePositioner = currentTilePositioner;
            _tileMover = tileMover;
        }

        public void Tick()
        {
            switch (_gameStateModel.GameStateName)
            {
                case GameStateName.Main:
                {
                    if (_currentTileModel.TryGetCurrentTileId(out var currentTileId))
                    {
                        _tileMover.MoveTile(currentTileId, _currentTilePositioner.CurrentPosition);
                    }

                    break;
                }
                case GameStateName.Tutorial:
                {
                    if (_currentTileModel.TryGetCurrentTileId(out var currentTileId))
                    {
                        _tileMover.MoveTile(currentTileId,_currentTilePositioner.CurrentPosition);
                    }

                    break;
                }
            }
        }
    }
}