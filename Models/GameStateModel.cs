using System;
using Enums;
using UniRx;

namespace Models
{
    public class GameStateModel
    {
        private readonly ReactiveProperty<GameStateName> _reactivePropertyGameStateName = new(GameStateName.None);
        public IObservable<GameStateName> OnChangedGameStateName => _reactivePropertyGameStateName;
        public GameStateName GameStateName => _reactivePropertyGameStateName.Value;

        public void SetGameStateName(GameStateName gameStateName)
        {
            _reactivePropertyGameStateName.Value = gameStateName;
        }
    }
}