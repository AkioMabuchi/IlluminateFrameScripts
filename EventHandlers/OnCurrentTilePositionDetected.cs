using Models;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnCurrentTilePositionDetected : IInitializable
    {
        private readonly CurrentTilePositionDetector _currentTilePositionDetector;

        private readonly TileFactory _tileFactory;

        [Inject]
        public OnCurrentTilePositionDetected(CurrentTilePositionDetector currentTilePositionDetector,
            TileFactory tileFactory)
        {
            _currentTilePositionDetector = currentTilePositionDetector;
            _tileFactory = tileFactory;
        }

        public void Initialize()
        {
            _currentTilePositionDetector.OnDetected.Subscribe(hitPoint =>
            {
                var position = new Vector3(hitPoint.x, 0.1f, hitPoint.z);
                _tileFactory.SetCurrentTilePosition(position);
            });
        }
    }
}