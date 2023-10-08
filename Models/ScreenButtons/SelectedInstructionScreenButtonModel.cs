using UniRx;

namespace Models.ScreenButtons
{
    public class SelectedInstructionScreenButtonModel
    {
        private readonly ReactiveProperty<int?>
            _reactivePropertySelectedInstructionScreenButtonIndex = new(null);

        public void Select(int index)
        {
            _reactivePropertySelectedInstructionScreenButtonIndex.Value = index;
        }

        public void Deselect()
        {
            _reactivePropertySelectedInstructionScreenButtonIndex.Value = null;
        }

        public bool TryGetSelectedIndex(out int index)
        {
            var nullableIndex = _reactivePropertySelectedInstructionScreenButtonIndex.Value;
            if (nullableIndex.HasValue)
            {
                index = nullableIndex.Value;
                return true;
            }

            index = default;
            return false;
        }
    }
}