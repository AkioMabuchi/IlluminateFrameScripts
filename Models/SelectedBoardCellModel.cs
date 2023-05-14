using UniRx;
using UnityEngine;

namespace Models
{
    public class SelectedBoardCellModel
    {
        private readonly ReactiveProperty<Vector2Int?> _reactivePropertySelectedBoardCell = new(null);
        public Vector2Int? SelectedBoardCell => _reactivePropertySelectedBoardCell.Value;

        public void SetSelectedPanelCell(Vector2Int cellPosition)
        {
            _reactivePropertySelectedBoardCell.Value = cellPosition;
        }

        public void SetSelectedPanelCellNull()
        {
            _reactivePropertySelectedBoardCell.Value = null;
        }
    }
}