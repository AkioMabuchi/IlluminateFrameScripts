using System;
using System.Collections.Generic;
using Enums;
using Enums.ScreenButtonNames;
using UniRx;

namespace Models.ScreenButtons
{
    public class SelectedRecordsScreenButtonModel
    {
        private readonly ReactiveProperty<RecordsScreenButtonName>
            _reactivePropertySelectedRecordsScreenButtonName = new(RecordsScreenButtonName.None);

        public IObservable<RecordsScreenButtonName> OnChangedSelectedRecordsScreenButtonName
            => _reactivePropertySelectedRecordsScreenButtonName;

        public RecordsScreenButtonName SelectedRecordsScreenButtonName
            => _reactivePropertySelectedRecordsScreenButtonName.Value;
        
        public void Select(RecordsScreenButtonName recordsScreenButtonName)
        {
            _reactivePropertySelectedRecordsScreenButtonName.Value = recordsScreenButtonName;
        }

        public void Deselect()
        {
            _reactivePropertySelectedRecordsScreenButtonName.Value = RecordsScreenButtonName.None;
        }
    }
}