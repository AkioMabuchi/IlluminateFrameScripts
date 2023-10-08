using System;
using Enums;
using Enums.ScreenButtonNames;
using UniRx;

namespace Models.ScreenButtons
{
    public class SelectedTitleScreenButtonModel
    {
        private readonly ReactiveProperty<TitleScreenButtonName> 
            _reactivePropertySelectedTitleScreenButtonName = new(TitleScreenButtonName.None);

        public IObservable<TitleScreenButtonName> OnChangedSelectedTitleScreenButtonName
            => _reactivePropertySelectedTitleScreenButtonName;

        public TitleScreenButtonName SelectedTitleScreenButtonName
            => _reactivePropertySelectedTitleScreenButtonName.Value;

        public void Select(TitleScreenButtonName titleScreenButtonName)
        {
            _reactivePropertySelectedTitleScreenButtonName.Value = titleScreenButtonName;
        }

        public void Deselect()
        {
            _reactivePropertySelectedTitleScreenButtonName.Value = TitleScreenButtonName.None;
        }
    }
}