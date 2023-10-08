using Models;
using VContainer;
using VContainer.Unity;
using Views.Controllers;

namespace Processes.Updates
{
    public class MoveNextTileUpdateProcess : ITickable
    {
        private readonly NextTileModel _nextTileModel;

        private readonly NextTilePositioner _nextTilePositioner;
        private readonly TileMover _tileMover;

        [Inject]
        public MoveNextTileUpdateProcess(NextTileModel nextTileModel, NextTilePositioner nextTilePositioner,
            TileMover tileMover)
        {
            _nextTileModel = nextTileModel;

            _nextTilePositioner = nextTilePositioner;
            _tileMover = tileMover;
        }

        public void Tick()
        {
            if (_nextTileModel.TryGetTileId(out var nextTileId))
            {
                _tileMover.MoveTile(nextTileId, _nextTilePositioner.CurrentPosition);
            }
        }
    }
}