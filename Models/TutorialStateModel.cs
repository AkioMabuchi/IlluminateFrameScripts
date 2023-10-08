using System;
using Enums;
using UniRx;

namespace Models
{
    public class TutorialStateModel
    {
        private readonly ReactiveProperty<TutorialStateName>
            _reactivePropertyTutorialStateName = new(TutorialStateName.None);

        public IObservable<TutorialStateName> OnChangedTutorialStateName => _reactivePropertyTutorialStateName;
        public TutorialStateName TutorialStateName => _reactivePropertyTutorialStateName.Value;
        private readonly ReactiveProperty<bool> _reactivePropertyCanPut = new(false);
        public bool CanPut => _reactivePropertyCanPut.Value;

        public void ChangeTutorialState(TutorialStateName tutorialStateName)
        {
            _reactivePropertyTutorialStateName.Value = tutorialStateName;
        }

        public void SetCanPut(bool canPut)
        {
            _reactivePropertyCanPut.Value = canPut;
        }
    }
}