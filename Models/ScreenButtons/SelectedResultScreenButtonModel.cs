using System.Collections.Generic;
using Enums;
using Enums.ScreenButtonNames;
using UniRx;

namespace Models.ScreenButtons
{
    public class SelectedResultScreenButtonModel
    {
        private readonly ReactiveProperty<ResultScreenButtonName>
            _reactivePropertySelectedResultScreenButtonName = new(ResultScreenButtonName.None);

        public ResultScreenButtonName SelectedResultScreenButtonName
            => _reactivePropertySelectedResultScreenButtonName.Value;

        public void Select(ResultScreenButtonName resultScreenButtonName)
        {
            _reactivePropertySelectedResultScreenButtonName.Value = resultScreenButtonName;
        }

        public void Deselect()
        {
            _reactivePropertySelectedResultScreenButtonName.Value = ResultScreenButtonName.None;
        }
    }
}