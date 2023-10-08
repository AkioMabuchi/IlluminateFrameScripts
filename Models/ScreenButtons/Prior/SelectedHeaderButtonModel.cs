using System;
using System.Collections.Generic;
using Enums;
using Enums.ScreenButtonNames.Prior;
using UniRx;

namespace Models.ScreenButtons.Prior
{
    public class SelectedHeaderButtonModel
    {
        private readonly ReactiveProperty<HeaderButtonName>
            _reactivePropertySelectedHeaderButtonName = new(HeaderButtonName.None);

        public IObservable<HeaderButtonName> OnChangedSelectedHeaderButtonName
            => _reactivePropertySelectedHeaderButtonName;

        public HeaderButtonName SelectedHeaderButtonName =>
            _reactivePropertySelectedHeaderButtonName.Value;

        public void Select(HeaderButtonName headerButtonName)
        {
            _reactivePropertySelectedHeaderButtonName.Value = headerButtonName;
        }

        public void Deselect()
        {
            _reactivePropertySelectedHeaderButtonName.Value = HeaderButtonName.None;
        }
    }
}