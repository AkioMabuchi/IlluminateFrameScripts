using System;
using Enums;
using Models;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Views;
using Views.Controllers;

namespace Processes.Events
{
    public class OnCurrentTilePositionDetected : IInitializable, IDisposable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly CurrentTilePositionDetector _currentTilePositionDetector;

        private readonly CurrentTilePositioner _currentTilePositioner;

        private readonly CompositeDisposable _compositeDisposable = new();

        [Inject]
        public OnCurrentTilePositionDetected(GameStateModel gameStateModel,
            CurrentTilePositionDetector currentTilePositionDetector, CurrentTilePositioner currentTilePositioner)
        {
            _gameStateModel = gameStateModel;
            _currentTilePositionDetector = currentTilePositionDetector;

            _currentTilePositioner = currentTilePositioner;
        }

        public void Initialize()
        {
            _currentTilePositionDetector.OnDetected
                .Subscribe(hitPoint =>
                {
                    switch (_gameStateModel.GameStateName)
                    {
                        case GameStateName.Main:
                        {
                            _currentTilePositioner.SetEndPosition(new Vector3(hitPoint.x, 0.1f, hitPoint.z));
                            break;
                        }
                        case GameStateName.Tutorial:
                        {
                            _currentTilePositioner.SetEndPosition(new Vector3(hitPoint.x, 0.1f, hitPoint.z));
                            break;
                        }
                    }
                }).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}