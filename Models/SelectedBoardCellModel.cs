using System;
using UniRx;
using UnityEngine;

namespace Models
{
    public class SelectedBoardCellModel
    {
        private readonly ReactiveProperty<Vector2Int?> _reactivePropertySelectedBoardCell = new(null);
        public IObservable<Vector2Int?> OnChangedSelectedBoardCell => _reactivePropertySelectedBoardCell;

        public void SetSelectedPanelCell(Vector2Int cellPosition)
        {
            _reactivePropertySelectedBoardCell.Value = cellPosition;
        }

        public void Reset()
        {
            _reactivePropertySelectedBoardCell.Value = null;
        }

        public bool TryGetSelectedPanelCell(out Vector2Int cellPosition)
        {
            if (_reactivePropertySelectedBoardCell.Value.HasValue)
            {
                cellPosition = _reactivePropertySelectedBoardCell.Value.Value;
                return true;
            }

            cellPosition = default;
            return false;
        }
    }
}