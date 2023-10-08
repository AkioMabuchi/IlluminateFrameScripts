using System;
using Enums;
using Enums.ScreenButtonNames;
using UniRx;

namespace Models.ScreenButtons
{
    public class SelectedSelectFrameSizeScreenButtonModel
    {
        private readonly ReactiveProperty<SelectFrameSizeScreenButtonName>
            _reactivePropertySelectedSelectFrameSizeScreenButtonName = new(SelectFrameSizeScreenButtonName.None);
        public IObservable<SelectFrameSizeScreenButtonName> OnChangedSelectedSelectFrameSizeScreenButton
            => _reactivePropertySelectedSelectFrameSizeScreenButtonName;
        public SelectFrameSizeScreenButtonName SelectFrameSizeScreenButtonName
            => _reactivePropertySelectedSelectFrameSizeScreenButtonName.Value;

        public void Select(SelectFrameSizeScreenButtonName selectFrameSizeScreenButtonName)
        {
            _reactivePropertySelectedSelectFrameSizeScreenButtonName.Value = selectFrameSizeScreenButtonName;
        }

        public void Deselect()
        {
            _reactivePropertySelectedSelectFrameSizeScreenButtonName.Value = SelectFrameSizeScreenButtonName.None;
        }
    }
}